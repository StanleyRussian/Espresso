//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class CoffeeSale
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CoffeeSale()
        {
            this.Sale_Details = new HashSet<CoffeeSale_Details>();
        }
    
        public int Id { get; set; }
        public System.DateTime Date { get; set; }
        public System.DateTime PaymentDate { get; set; }
        public bool Paid { get; set; }
        public double Sum { get; set; }
        public int TransactionId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CoffeeSale_Details> Sale_Details { get; set; }
        public virtual Recipient Recipient { get; set; }
        public virtual Account Account { get; set; }
    }
}
