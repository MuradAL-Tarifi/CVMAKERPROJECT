//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MYCVMAKERPROJECT.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PersonalSkill
    {
        public int Id { get; set; }
        public int Level { get; set; }
        public string Skill { get; set; }
        public Nullable<int> PersonalId { get; set; }
    
        public virtual Personal Personal { get; set; }
    }
}
