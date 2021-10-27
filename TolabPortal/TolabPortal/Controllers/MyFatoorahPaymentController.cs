using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TolabPortal.DataAccess.Models.Payment;
using TolabPortal.DataAccess.Services;
using TolabPortal.DataAccess.Services.Payment;
using TolabPortal.ViewModels;

namespace TolabPortal.Controllers
{
  
    public class MyFatoorahPaymentController : Controller
    {
        private readonly IMyFatoorahPaymentService _paymentService;
        private readonly ISubscribeService subscribeService;
        private readonly ILogger<MyFatoorahPaymentController> logger;
        private readonly IConfiguration config;

        public MyFatoorahPaymentController(IMyFatoorahPaymentService paymentService, ISubscribeService subscribeService, ILogger<MyFatoorahPaymentController> logger,IConfiguration config)
        {
            _paymentService = paymentService;
            this.subscribeService = subscribeService;
            this.logger = logger;
            this.config = config;
        }


         
        [Route("~/InitiatePayment/{amount}")]
        public async Task<IActionResult> InitiatePayment(decimal amount)
        {

            var response = await _paymentService.InitiatePayment(new InitiatePaymentRequest
            {
                InvoiceAmount = amount,
                CurrencyIso = "kwd"

            }).ConfigureAwait(false); 
            if (response.IsSuccess)
                return View(new PaymentViewModel() {
                    InvoiceValue=amount,
                    PaymentMethods=response.Data.PaymentMethods,
                    CustomerMobile= "01267086929",
                    MobileCountryCode= "+2",
                    CustomerName= "46695",
                    CustomerReference = "10110",
                    TransactionType=(int)TransactionType.Course,
                    ReturnUrl= "2,https://f5e9-46-153-75-72.ngrok.io/Subjects/Track/Course?courseId=10110"
                });
            return View("ErrorPayment");
        }

 
        [HttpPost("executePayment")]
        public async Task<IActionResult> ExecutePayment(PaymentViewModel paymentVm)
        {
            if (ModelState.IsValid)
            {
                var response = await _paymentService.ExecutePayment(new ExecutePaymentRequest() {
                    PaymentMethodId = paymentVm.PaymentMethodId,
                    InvoiceValue = paymentVm.InvoiceValue,
                    CustomerReference = paymentVm.CustomerReference,
                    MobileCountryCode = paymentVm.MobileCountryCode,
                    UserDefinedField = $"{paymentVm.TransactionType},{paymentVm.ReturnUrl}",
                    CallBackUrl = $"{config["CallBackPayemntRoot"]}/CompletePayment",
                    ErrorUrl = $"{config["CallBackPayemntRoot"]}/ErrorPayment",
                    Language = "AR",
                    ExpiryDate = DateTime.Now.AddYears(1)
                }).ConfigureAwait(false);
                if (response.IsSuccess)
                {
                    var url = response.Data.PaymentURL;
                    var invoiceId = response.Data.InvoiceId;
                    return Redirect(response.Data.PaymentURL);
                }
                
            }
            return View("ErrorPayment");


        }
        [AllowAnonymous]
        [Route("~/ErrorPayment")]
        public IActionResult ErrorPayment()
        {
            return View();
        }



        [Route("~/CompletePayment")]
        public async Task<IActionResult> CompletePayment(string paymentId)
        {

            try
            {
                var response = await _paymentService.LogTransaction(new GetPaymentStatusRequest
                {
                    Key = paymentId,
                    KeyType = "paymentId"
                }).ConfigureAwait(false);
                if (response.IsSuccess)
                {
                    if (response.Data.InvoiceStatus.ToLower() == "paid")
                    {
                        var computedFiled = response.Data.UserDefinedField.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        switch (computedFiled[0])
                        {
                            case string transaction when int.Parse(transaction) == (int)TransactionType.Course:
                                await subscribeService.SubscribeCourse(!string.IsNullOrEmpty(response.Data.CustomerReference) ? long.Parse(response.Data.CustomerReference) : 0);
                                break;
                            case string transaction when int.Parse(transaction) == (int)TransactionType.Live:
                                await subscribeService.SubscribeLive(!string.IsNullOrEmpty(response.Data.CustomerReference) ? long.Parse(response.Data.CustomerReference) : 0);
                                break;
                            case string transaction when int.Parse(transaction) == (int)TransactionType.Track:
                                await subscribeService.SubscribeTrack(!string.IsNullOrEmpty(response.Data.CustomerReference) ? long.Parse(response.Data.CustomerReference) : 0);
                                break;
                        }

                        if (!string.IsNullOrEmpty(computedFiled[1]))
                            return Redirect(computedFiled[1]);
                        return RedirectToAction("Index", "Home");

                    }


                }

                return View("ErrorPayment");

            }
            catch (Exception ex)
            {
                logger.LogError($"Complete payment :{ex.Message} ||StackTrace : {ex.StackTrace} ");
                return View("ErrorPayment");
            }

        }



         
    }
}
