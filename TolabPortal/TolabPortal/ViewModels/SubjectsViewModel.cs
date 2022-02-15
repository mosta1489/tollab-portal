using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tolab.Common;
using TolabPortal.DataAccess.Models;

namespace TolabPortal.ViewModels
{
    public class SubjectsViewModel
    {
        public List<StudentHomeCourse> StudentHomeCourses { get; set; }
        public List<Section> Sections { get; set; }
    }
}