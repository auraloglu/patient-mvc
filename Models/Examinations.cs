//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVC_Final.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Examinations
    {
        public int ID { get; set; }
        public int InspectionID { get; set; }
        public string Examination { get; set; }
        public System.DateTime RegisterDate { get; set; }
        public bool Active { get; set; }
    
        public virtual Inspections Inspections { get; set; }
    }
}
