using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TolabPortal.DataAccess.Models;
using TolabPortal.ViewModels;

namespace TolabPortal.Views.Shared.Components.StudentTreeView
{
    public class StudentTreeViewViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(List<Section> vm)
        {
            return View("Default", vm);
        }
    }

     
}
