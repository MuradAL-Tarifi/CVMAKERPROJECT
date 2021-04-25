using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MYCVMAKERPROJECT.Models
{
    [MetadataType(typeof(AdminMetaData))]
    public partial class Admin
    {
    }
    public class AdminMetaData
    {
        public int Id { get; set; }
        public string Ad_Name { get; set; }
        public int UserPageViews { get; set; }
        public int PersonalRegistration { get; set; }
        public int EmployersRegistration { get; set; }
        public int DailyViews { get; set; }
        public int TotalViews { get; set; }
        public int TotalJobAlerts { get; set; }
        public Nullable<int> UsersId { get; set; }

        public virtual User User { get; set; }
    }
}