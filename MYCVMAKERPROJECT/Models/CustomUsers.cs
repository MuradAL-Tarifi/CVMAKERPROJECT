using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MYCVMAKERPROJECT.Models
{
    [MetadataType(typeof(UsersMetaData))]
    public partial class Users
    {
    }
    public class UsersMetaData
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string UserEmail { get; set; }
        public int UserState { get; set; }
        public string Description { get; set; }
        public string InstagramLink { get; set; }
        public string GetHupLink { get; set; }
        public string LinkedInLink { get; set; }
        public string FacebookLink { get; set; }
        public string Address { get; set; }
        public int PhoneNumber { get; set; }
    }

}