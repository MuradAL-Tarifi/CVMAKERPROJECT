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
    
    public partial class CompanyProject
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
        public Nullable<int> CompanyId { get; set; }
    
        public virtual Company Company { get; set; }
    }
}