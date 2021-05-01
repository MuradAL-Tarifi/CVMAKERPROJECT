using MYCVMAKERPROJECT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MYCVMAKERPROJECT.ViewModel
{
    public class CompanyViewModel
    {
        public User User { get; set; }
        public Company Company { get; set; }
        public List<Nofitication> Nofitication { get; set; }
        public List<CompanWorkExperience> CompanWorkExperience { get; set; }
        public List<CompanyProject> CompanyProject { get; set; }
        public List<CompanyService> CompanyServices { get; set; }
        public List<CompanySkill> CompanySkills { get; set; }
    }
}