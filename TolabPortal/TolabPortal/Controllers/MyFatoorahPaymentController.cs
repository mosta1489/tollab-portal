using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TolabPortal.DataAccess.Models.Payment;
using TolabPortal.DataAccess.Services.Payment;
using TolabPortal.ViewModels;

namespace TolabPortal.Controllers
{
    
    public class MyFatoorahPaymentController : Controller
    {
        private readonly IMyFatoorahPaymentService _paymentService;
        private readonly IConfiguration config;

        public MyFatoorahPaymentController(IMyFatoorahPaymentService paymentService, IConfiguration config)
        {
            _paymentService = paymentService;
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
                return View(new PaymentViewModel() {InvoiceValue=amount,PaymentMethods=response.Data.PaymentMethods,CallBackUrl= "https://tollab.azurewebsites.net/", ErrorUrl= "https://tollab.azurewebsites.net/" });
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
                    CallBackUrl = paymentVm.CallBackUrl,
                    ErrorUrl = paymentVm.ErrorUrl,
                    Language = "AR",
                    ExpiryDate = DateTime.Now.AddYears(1),


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



        [Route("~/InitiatePayment/{amount}")]
        public async Task<IActionResult> CompletePayment(GetPaymentStatusRequest getPaymentStatusRequest)
        {
            if (ModelState.IsValid)
            {
                var response = await _paymentService.GetPaymentStatus(getPaymentStatusRequest).ConfigureAwait(false);
                return View("GetPaymentStatusResult", response);

            }
            return View("GetPaymentStatus", getPaymentStatusRequest);
        }



        //[HttpGet("sendPayment")]
        //public IActionResult SendPayment()
        //{
        //    var model = new SendPaymentRequest
        //    {
        //        //required fields
        //        CustomerName = "Customer Name",
        //        NotificationOption = "LNK",
        //        InvoiceValue = 100,
        //        //optional fields 
        //        DisplayCurrencyIso = "KWD",
        //        MobileCountryCode = "965",
        //        CustomerMobile = "12345678",
        //        CustomerEmail = "email@example.com",
        //        CallBackUrl = "https://example.com/callback",
        //        ErrorUrl = "https://example.com/error",
        //        Language = "En",
        //        CustomerReference = "",
        //        CustomerCivilId = "",
        //        UserDefinedField = "",
        //        ExpiryDate = DateTime.Now.AddYears(1),
        //    };

        //    return View("SendPayment", model);
        //}
        //[HttpPost("sendPayment")]
        //public async Task<IActionResult> PostSendPayment(SendPaymentRequest sendPaymentRequest)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (string.IsNullOrEmpty(sendPaymentRequest.Language))
        //            sendPaymentRequest.Language = "en";
        //        var response = await _paymentService.SendPayment(new SendPaymentRequest()
        //        {

        //        }).ConfigureAwait(false);
        //        return View("SendPaymentResult", response);

        //    }
        //    return View("SendPayment", sendPaymentRequest);
        //}
    }
}
