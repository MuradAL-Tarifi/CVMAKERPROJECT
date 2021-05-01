using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MYCVMAKERPROJECT.Models
{
    [MetadataType(typeof(PersonalMetaData))]
    public partial class Personal
    {
    }
    public class PersonalMetaData
    {
        public int Id { get; set; }
        public string P_FirstName { get; set; }
        public string P_LastName { get; set; }
        public string P_JobTitle { get; set; }
        public string P_FileCV { get; set; }
        public string P_Image { get; set; }
        public string P_Gender { get; set; }
        public Nullable<int> UsersId { get; set; }

    }
}