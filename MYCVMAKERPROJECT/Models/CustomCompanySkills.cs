using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MYCVMAKERPROJECT.Models
{
    [MetadataType(typeof(CompanySkillsMetaData))]
    public partial class CompanySkills
    {
    }
    public class CompanySkillsMetaData
    {
        public int Id { get; set; }
        public string Skill { get; set; }
        public Nullable<System.Guid> CompanyId { get; set; }

        public virtual Company Company { get; set; }
    }
}