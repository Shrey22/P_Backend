//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FinalProject.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class T_Subject
    {
        public T_Subject()
        {
            this.T_Question = new HashSet<T_Question>();
            this.T_Result = new HashSet<T_Result>();
            this.T_Sample_Paper = new HashSet<T_Sample_Paper>();
        }
    
        public int SubId { get; set; }
        public string SubName { get; set; }
    
        public virtual ICollection<T_Question> T_Question { get; set; }
        public virtual ICollection<T_Result> T_Result { get; set; }
        public virtual ICollection<T_Sample_Paper> T_Sample_Paper { get; set; }
    }
}
