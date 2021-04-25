using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MYCVMAKERPROJECT.Models
{
    [MetadataType(typeof(PersonalProjectMetaData))]
    public partial class PersonalProject
    {
    }
    public class PersonalProjectMetaData
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string EndDate { get; set; }
        public string ClientName { get; set; }
        public string Tools { get; set; }
        public string DomainName { get; set; }
        public string InterfaceImage { get; set; }
        public string SubImage { get; set; }
        public Nullable<int> PersonalId { get; set; }

        public virtual Personal Personal { get; set; }
    }
}