//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DigiboardEditor
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        public int UserID { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public Nullable<int> roleID { get; set; }
        public Nullable<bool> isDeleted { get; set; }
        public string password { get; set; }
    
        public virtual UserRole UserRole { get; set; }
    }
}
