using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MYCVMAKERPROJECT.Models
{
    [MetadataType(typeof(JobAlertMetaData))]
    public partial class JobAlert
    {
    }
    public class JobAlertMetaData
    {
        public int Id { get; set; }
        public string J_Description { get; set; }
        public string J_location { get; set; }
        public string J_CompanyIndustry { get; set; }
        public string J_CompanyType { get; set; }
        public string J_JobRole { get; set; }
        public string ImplementType { get; set; }
        public decimal MonthlySalaryRange { get; set; }
        public int NumberOfVacancies { get; set; }
        public string Careerlevel { get; set; }
        public int YearsOfExperience { get; set; }
        public string Address { get; set; }
        public string Degree { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.Guid> CompanyId { get; set; }

        public virtual Company Company { get; set; }
    }
}