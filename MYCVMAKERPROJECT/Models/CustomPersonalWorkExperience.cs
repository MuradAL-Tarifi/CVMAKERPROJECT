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
        public string City { get; set; }
        public string Employeer { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Description { get; set; }
        public string JobTitle { get; set; }
        public Nullable<int> PersonalId { get; set; }

        public virtual Personal Personal { get; set; }
    }
}