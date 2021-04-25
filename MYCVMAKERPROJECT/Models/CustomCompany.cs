using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MYCVMAKERPROJECT.Models
{
    [MetadataType(typeof(CompanyMetaData))]

    public partial class Company
    {
    }
    public class CompanyMetaData
    {
        public int Id { get; set; }
        public string C_Name { get; set; }
        public string C_JobTitle { get; set; }
        public string C_Type { get; set; }
        public string C_logo { get; set; }
        public Nullable<System.Guid> UsersId { get; set; }
    }
}