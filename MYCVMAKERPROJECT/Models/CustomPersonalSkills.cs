using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MYCVMAKERPROJECT.Models
{
    [MetadataType(typeof(PersonalSkillsMetaData))]
    public partial class PersonalSkill
    {

    }
    public class PersonalSkillsMetaData
    {
        public int Id { get; set; }
        public int Level { get; set; }
        public string Skill { get; set; }
        public Nullable<int> PersonalId { get; set; }

        public virtual Personal Personal { get; set; }
    }
}