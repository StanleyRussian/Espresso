//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Espresso.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class PackagePurchase
    {
        public int Id { get; set; }
        public System.DateTime Date { get; set; }
        public int PackQuantity { get; set; }
        public double Price { get; set; }
    
        public virtual Package Package { get; set; }
        public virtual Account Account { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
