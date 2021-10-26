using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TolabPortal.DataAccess.Models.Payment;
using TolabPortal.DataAccess.Services.Payment;
using TolabPortal.ViewModels;

namespace TolabPortal.Controllers
{
    [AllowAnonymous]
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
                return View(new PaymentViewModel() {
                    InvoiceValue=amount,
                    PaymentMethods=response.Data.PaymentMethods,
                    CustomerMobile= "01267086929",
                    MobileCountryCode= "+2",
                    CustomerName= "46695",
                    CustomerReference ="CourseIdOrTrackId",
                    TransactionType=(int)TransactionType.Course,
                    ReturnUrl=""
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
        [Route("~/ErrorPayment")]
        public IActionResult ErrorPayment()
        {
            return View();
        }



        [Route("~/CompletePayment")]
        public async Task<IActionResult> CompletePayment(string paymentId)
        {
            if (ModelState.IsValid)
            {
                var response = await _paymentService.LogTransaction(new GetPaymentStatusRequest { 
                Key=paymentId,
                KeyType= "paymentId"
                }).ConfigureAwait(false);
                if (response.IsSuccess)
                {
                    if (response.Data.InvoiceStatus.ToLower()=="paid")
                    {
                        
                        // split userDefined parameter to transactionType and return URl
                        //subscribe to course or track or Live
                        // redirect to paymnet Url
                        
                    }


                }

               

            }
            return View("ErrorPayment");

        }



         
    }
}
