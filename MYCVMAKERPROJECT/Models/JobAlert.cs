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
    
    public partial class JobAlert
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public JobAlert()
        {
            this.Nofitications = new HashSet<Nofitication>();
        }
    
        public int Id { get; set; }
        public string J_Description { get; set; }
        public string J_Title { get; set; }
        public string J_location { get; set; }
        public string J_CompanyIndustry { get; set; }
        public string J_CompanyType { get; set; }
        public string J_JobRole { get; set; }
        public string ImplementType { get; set; }
        public decimal MonthlySalaryRange { get; set; }
        public int NumberOfVacancies { get; set; }
        public string Careerlevel { get; set; }
        public int YearsOfExperience { get; set; }
        public string Address { get; set; }
        public string Degree { get; set; }
        public bool IsActive { get; set; }
        public Nullable<int> CompanyId { get; set; }
    
        public virtual Company Company { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Nofitication> Nofitications { get; set; }
    }
}
