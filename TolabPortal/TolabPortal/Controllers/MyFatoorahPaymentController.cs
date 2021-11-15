using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Tolab.Common;
using TolabPortal.DataAccess.Models.Payment;
using TolabPortal.DataAccess.Services;
using TolabPortal.DataAccess.Services.Payment;
using TolabPortal.ViewModels;

namespace TolabPortal.Controllers
{
    public class MyFatoorahPaymentController : Controller
    {
        private readonly IMyFatoorahPaymentService _paymentService;
        private readonly ISubscribeService _subscribeService;
        private readonly ISessionManager _sessionManager;
        private readonly ILogger<MyFatoorahPaymentController> _logger;
        private readonly ApplicationConfig _config;

        public MyFatoorahPaymentController(IMyFatoorahPaymentService paymentService, ISubscribeService subscribeService,
            ISessionManager sessionManager, ILogger<MyFatoorahPaymentController> logger, IOptions<ApplicationConfig> config)
        {
            _paymentService = paymentService;
            this._subscribeService = subscribeService;
            this._sessionManager = sessionManager;
            this._logger = logger;
            this._config = config.Value;
        }

        // [Route("~/InitiatePayment/{amount}")]
        [HttpPost]
        public async Task<IActionResult> InitiatePayment(PayVm payViewmodel)
        {
            if (payViewmodel.InvoiceAmount == 0)
                return RedirectToAction("CompleteFreePayment", new { TransactionType = payViewmodel.TransactionType, tranactionId = payViewmodel.TransactionId,
                    redirectUrl= $"{_config.CallBackPayemntRoot}{payViewmodel.ReturnRoute}"
                });
            var response = await _paymentService.InitiatePayment(new InitiatePaymentRequest
            {
                InvoiceAmount = payViewmodel.InvoiceAmount,
                CurrencyIso = "kwd"
            }).ConfigureAwait(false);
            if (response.IsSuccess)
                return View(new PaymentViewModel()
                {
                    InvoiceValue = payViewmodel.InvoiceAmount,
                    PaymentMethods = response.Data.PaymentMethods,
                    CustomerName = _sessionManager.UserId ?? "",
                    CustomerReference = payViewmodel.TransactionId,
                    TransactionType = payViewmodel.TransactionType,
                    ReturnUrl = $"{_config.CallBackPayemntRoot}{payViewmodel.ReturnRoute}"
                });
            return View("ErrorPayment");
        }

        [HttpPost("executePayment")]
        public async Task<IActionResult> ExecutePayment(PaymentViewModel paymentVm)
        {
            if (ModelState.IsValid)
            {


                var response = await _paymentService.ExecutePayment(new ExecutePaymentRequest()
                {
                    PaymentMethodId = paymentVm.PaymentMethodId,
                    InvoiceValue = paymentVm.InvoiceValue == 0 ? 1 : paymentVm.InvoiceValue,
                    CustomerReference = paymentVm.CustomerReference,
                    CustomerName = _sessionManager.UserId ?? "",
                    UserDefinedField = $"{paymentVm.TransactionType},{paymentVm.ReturnUrl},{_sessionManager.IdentityId}",
                    CallBackUrl = $"{_config.CallBackPayemntRoot}/CompletePayment",
                    ErrorUrl = $"{_config.CallBackPayemntRoot}/ErrorPayment",
                    Language = "AR",
                    CustomerMobile= _sessionManager.CountryCode+_sessionManager.Phone,
                    CustomerEmail=_sessionManager.Email,
                    ExpiryDate = DateTime.Now.AddYears(1),
                 
                }).ConfigureAwait(false);
                if (response.IsSuccess)
                {
                    _ = await _subscribeService.InsertInvoiceLog(response.Data.InvoiceId);
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
                var message = await _paymentService.LogTransaction(new GetPaymentStatusRequest
                {
                    Key = paymentId,
                    KeyType = "paymentId"
                }).ConfigureAwait(false);
                var response = JsonConvert.DeserializeObject<GenericResponse<GetPaymentStatusResponse>>(message);
                if (response.IsSuccess)
                {
                    if (response.Data.InvoiceStatus.ToLower() == "paid")
                    {
                      
                        var computedFiled = response.Data.UserDefinedField.Split(",", StringSplitOptions.RemoveEmptyEntries);
                        switch (computedFiled[0])
                        {
                            case string transaction when int.Parse(transaction) == (int)TransactionType.Course:
                                await _subscribeService.SubscribeCourse(message, !string.IsNullOrEmpty(response.Data.CustomerReference) ? long.Parse(response.Data.CustomerReference) : 0);
                                break;

                            case string transaction when int.Parse(transaction) == (int)TransactionType.Live:
                                await _subscribeService.SubscribeLive(message, !string.IsNullOrEmpty(response.Data.CustomerReference) ? long.Parse(response.Data.CustomerReference) : 0);
                                break;

                            case string transaction when int.Parse(transaction) == (int)TransactionType.Track:
                                await _subscribeService.SubscribeTrack(!string.IsNullOrEmpty(response.Data.CustomerReference) ? long.Parse(response.Data.CustomerReference) : 0);
                                break;
                        }

                        if (!string.IsNullOrEmpty(computedFiled[1]))
                        {
                            _ = await _subscribeService.UpdateInvoiceLog(response.Data.InvoiceId);
                            return Redirect(computedFiled[1]);

                        }
                        return RedirectToAction("Index", "Home");
                    }
                }

                return View("ErrorPayment");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Complete payment :{ex.Message} ||StackTrace : {ex.StackTrace} ");
                return View("ErrorPayment");
            }
        }

        [Route("~/CompleteFreePayment")]
        public async Task<IActionResult> CompleteFreePayment(string transactionType, string tranactionId, string redirectUrl)
        {
            try
            {


                switch (transactionType)
                {
                    case string transaction when int.Parse(transaction) == (int)TransactionType.Course:
                        await _subscribeService.SubscribeCourse("", !string.IsNullOrEmpty(tranactionId) ? long.Parse(tranactionId) : 0);
                        break;

                    case string transaction when int.Parse(transaction) == (int)TransactionType.Live:
                        await _subscribeService.SubscribeLive("", !string.IsNullOrEmpty(tranactionId) ? long.Parse(tranactionId) : 0);
                        break;

                    case string transaction when int.Parse(transaction) == (int)TransactionType.Track:
                        await _subscribeService.SubscribeTrack(!string.IsNullOrEmpty(tranactionId) ? long.Parse(tranactionId) : 0);
                        break;
                }

                if (!string.IsNullOrEmpty(redirectUrl))
                    return Redirect(redirectUrl);
                return RedirectToAction("Index", "Home");


                return View("ErrorPayment");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Complete payment :{ex.Message} ||StackTrace : {ex.StackTrace} ");
                return View("ErrorPayment");
            }
        }

        
    }
}