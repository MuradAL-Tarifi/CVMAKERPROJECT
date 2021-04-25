using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MYCVMAKERPROJECT.Models
{
    [MetadataType(typeof(EducationMetaData))]
    public partial class Education
    {
    }
    public class EducationMetaData
    {
        public int Id { get; set; }
        public string Degree { get; set; }
        public string Description { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public Nullable<int> PersonalId { get; set; }

        public virtual Personal Personal { get; set; }
    }
}