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
    
    public partial class Roasting
    {
        public int Id { get; set; }
        public System.DateTime Date { get; set; }
        public double InitialAmount { get; set; }
        public double RoastedAmount { get; set; }
        public double ShrinkagePercent { get; set; }
    
        public virtual CoffeeSort CoffeeSort { get; set; }
    }
}
