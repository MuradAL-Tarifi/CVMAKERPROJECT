using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MYCVMAKERPROJECT.Models
{
    [MetadataType(typeof(PersonalServicesMetaData))]
    public partial class PersonalService
    {
    }
    public class PersonalServicesMetaData
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string JobTitle { get; set; }
        public Nullable<int> PersonalId { get; set; }

        public virtual Personal Personal { get; set; }
    }
}