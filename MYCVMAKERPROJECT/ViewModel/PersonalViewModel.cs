using MYCVMAKERPROJECT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MYCVMAKERPROJECT.ViewModel
{
    public class PersonalViewModel
    {
        public User User { get; set; }
        public Personal Personal { get; set; }
        public List<Nofitication> Nofitication { get; set; }
        public List<PersonalWorkExperience> PersonalWorkExperience { get; set; }
        public List<PersonalProject> PersonalProject { get; set; }
        public List<PersonalService> PersonalService { get; set; }
        public List<PersonalSkill> PersonalSkill { get; set; }
        public List<Education> Education { get; set; }
    }
}