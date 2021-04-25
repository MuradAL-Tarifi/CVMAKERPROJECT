using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MYCVMAKERPROJECT.Models
{
    [MetadataType(typeof(NofiticationMetaData))]
    public partial class Nofitication
    {
    }
    public class NofiticationMetaData
    {
        public int Id { get; set; }
        public Nullable<int> PersonalId { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public bool IsReaded { get; set; }
        public bool Submitted { get; set; }

        public virtual Company Company { get; set; }
        public virtual Personal Personal { get; set; }
    }
}