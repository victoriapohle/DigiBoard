//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DigiboardDisplay
{
    using System;
    using System.Collections.Generic;
    
    public partial class Event
    {
        public int eventID { get; set; }
        public string eventTitle { get; set; }
        public string eventDescription { get; set; }
        public string eventLocation { get; set; }
        public Nullable<bool> isDeleted { get; set; }
        public Nullable<System.DateTime> eventDateTime { get; set; }
        public Nullable<int> createdByUserID { get; set; }
    }
}
