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
    
    public partial class dPackedStocks
    {
        public int Id { get; set; }
        public int PackQuantity { get; set; }
        public Nullable<double> dCost { get; set; }
    
        public virtual Mix Mix { get; set; }
        public virtual Package Package { get; set; }
    }
}
