using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MYCVMAKERPROJECT.Models
{
    [MetadataType(typeof(PersonalWorkExperienceMetaData))]
    public partial class PersonalWorkExperience
    {
    }
    public class PersonalWorkExperienceMetaData
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string JobTitle { get; set; }
        public Nullable<System.Guid> CompanyId { get; set; }

        public virtual Company Company { get; set; }
    }
}