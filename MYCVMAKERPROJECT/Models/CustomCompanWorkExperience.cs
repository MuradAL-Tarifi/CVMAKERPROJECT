using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MYCVMAKERPROJECT.Models
{
    [MetadataType(typeof(CompanWorkExperienceMetaData))]

    public partial class CompanWorkExperience
    {
    }
    public class CompanWorkExperienceMetaData
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string JobTitle { get; set; }
        public Nullable<int> CompanyId { get; set; }

        public virtual Company Company { get; set; }
    }
}