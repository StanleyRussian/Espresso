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
    
    public partial class dRoastedStock
    {
        public int Id { get; set; }
        public double Quantity { get; set; }
    
        public virtual CoffeeSort CoffeeSort { get; set; }
    }
}
