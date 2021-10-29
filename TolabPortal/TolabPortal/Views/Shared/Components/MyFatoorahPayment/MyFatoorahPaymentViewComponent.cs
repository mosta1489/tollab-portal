using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TolabPortal.DataAccess.Models.Payment;

namespace TolabPortal.Views.Shared.Components.MyFatoorahPayment
{
    public class MyFatoorahPaymentViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(PayVm payViewmodel)
        {
            return View("Default", payViewmodel);
        }
    }

     
}
