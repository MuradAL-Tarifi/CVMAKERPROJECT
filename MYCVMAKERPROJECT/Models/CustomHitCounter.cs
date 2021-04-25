using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MYCVMAKERPROJECT.Models
{
    [MetadataType(typeof(HitCounterMetaData))]
    public partial class HitCounter
    {
    }
    public class HitCounterMetaData
    {
        public int Id { get; set; }
        public string IPAddress { get; set; }
        public System.DateTime CreateDate { get; set; }
        public Nullable<int> UsersId { get; set; }

        public virtual User User { get; set; }
    }
}