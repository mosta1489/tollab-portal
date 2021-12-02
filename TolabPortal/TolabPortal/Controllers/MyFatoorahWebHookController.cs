using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Tolab.Common;
using TolabPortal.DataAccess.Models;
using TolabPortal.DataAccess.Models.Payment;
using TolabPortal.DataAccess.Services;
using TolabPortal.DataAccess.Services.Payment;
using TolabPortal.ViewModels;
using TolabPortal.ViewModels.WebHookModels;

namespace TolabPortal.Controllers
{
    public class MyFatoorahWebHookController : Controller
    {
        private readonly IMyFatoorahPaymentService _paymentService;
        private readonly ISubscribeService _subscribeService;
        private readonly IAccountService accountService;

        public MyFatoorahWebHookController(IMyFatoorahPaymentService paymentService, ISubscribeService subscribeService, IAccountService accountService)
        {
            _paymentService = paymentService;
            _subscribeService = subscribeService;
            this.accountService = accountService;
        }

        [AllowAnonymous]

        [Route("~/MyFatoorahWebHookPost")]
        public async Task<IActionResult> MyFatoorahWebHookPost()
        {
            try
            {
                var json = new StreamReader(Request.Body).ReadToEndAsync().Result;
                Request.Headers.TryGetValue("MyFatoorah-Signature", out var signatureHeader);
                string secretKey = "", headerSignature = "";
                bool isValidSignature = true;
                if (!string.IsNullOrWhiteSpace(signatureHeader)) //Check If Enabled Secret Key and If The header has request
                {
                    isValidSignature = false;
                    headerSignature = signatureHeader.ToString();
                    secretKey = "CB2+r2b6p1Pq8QGR5ni0seF2666FPLCRbRBmLz/RBDTanYkVmwhFQ/UiB4WfnXmRNt2aPxdPo9xMSYk5s+blHQ=="; //From Your Portal.
                }
                var model = JsonConvert.DeserializeObject<GenericWebhookModel<object>>(json);
                if (model != null)
                {

                    var transactionModel = JsonConvert.DeserializeObject<GenericWebhookModel<WebhookTransactionStatus>>(json);
                    if (!isValidSignature)
                    {
                        isValidSignature = CheckMyFatoorahSignature(transactionModel, secretKey, headerSignature);
                        if (!isValidSignature) return BadRequest("Invalid Signature");
                        if (!string.IsNullOrEmpty(transactionModel.Data.PaymentId))
                        {
                            var message = await _paymentService.LogTransaction(new GetPaymentStatusRequest
                            {
                                Key = transactionModel.Data.PaymentId,
                                KeyType = "paymentId"
                            }).ConfigureAwait(false);
                            var response = JsonConvert.DeserializeObject<GenericResponse<GetPaymentStatusResponse>>(message);
                            if (response.IsSuccess)
                            {
                                if (response.Data.InvoiceStatus.ToLower() != "canceled")
                                {
                                    if (!string.IsNullOrEmpty(response.Data.CustomerName))
                                    {
                                        var studentDataResponse = await accountService.GetStudentById(response.Data.CustomerName);
                                        if (studentDataResponse.IsSuccessStatusCode)
                                        {
                                            var student = await CommonUtilities.GetResponseModelFromJson<StudentResponse>(studentDataResponse);
                                            var computedFiled = response.Data.UserDefinedField.Split(",", StringSplitOptions.RemoveEmptyEntries);
                                            switch (computedFiled[0])
                                            {
                                                case string transaction when int.Parse(transaction) == (int)TransactionType.Course:
                                                    await _subscribeService.SubscribeCourse(message, !string.IsNullOrEmpty(response.Data.CustomerReference) ? long.Parse(response.Data.CustomerReference) : 0, "", student.Student.IdentityId, true);
                                                    break;

                                                case string transaction when int.Parse(transaction) == (int)TransactionType.Live:
                                                    await _subscribeService.SubscribeLive(message, !string.IsNullOrEmpty(response.Data.CustomerReference) ? long.Parse(response.Data.CustomerReference) : 0, "", student.Student.IdentityId, true);
                                                    break;

                                                case string transaction when int.Parse(transaction) == (int)TransactionType.Track:
                                                    await _subscribeService.SubscribeTrack(!string.IsNullOrEmpty(response.Data.CustomerReference) ? long.Parse(response.Data.CustomerReference) : 0, "", student.Student.IdentityId, true);
                                                    break;
                                            }
                                        }

                                    }


                                }
                            }
                        }
                    }



                }
                else
                {
                    return BadRequest("model Is null");
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Route("~/fix-failed-invoices")]
        public async Task<string> FixFailedInvoices()
        {
            await _subscribeService.FixFailedInvoices();
            return "Done";
        }

        public bool CheckMyFatoorahSignature<T>(GenericWebhookModel<T> model, string secretKey, string headerSignature)
        {
            //***Generate The Signature*** :
            //1- Order all properties alphabetic
            //2-Encrypt the data with the secret key
            //3-Compare the signature
            var properties = typeof(T).GetProperties().Select(p => p.Name).OrderBy(p => p).ToList();
            var type = model.Data.GetType();
            var parameters = new List<ItemTxt>();
            for (int i = 0; i < properties.Count; i++)
            {
                var value = type.GetProperty(properties[i]).GetValue(model.Data);
                value = value == null ? "" : value.ToString();
                parameters.Add(new ItemTxt { Text = properties[i], Value = value.ToString() });
            }
            var signature = Sign(parameters, secretKey);
            return signature == headerSignature;
        }
        private static string Sign(List<ItemTxt> paramsArray, string secretKey)
        {
            var dataToSign = paramsArray.Select(p => p.Text + "=" + p.Value).ToList();
            var data = string.Join(",", dataToSign);
            var encoding = new UTF8Encoding();
            var keyByte = encoding.GetBytes(secretKey);
            var hmacsha256 = new HMACSHA256(keyByte);
            var messageBytes = encoding.GetBytes(data);
            return Convert.ToBase64String(hmacsha256.ComputeHash(messageBytes));

        }
    }


}
