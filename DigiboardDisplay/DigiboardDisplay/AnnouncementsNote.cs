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
    
    public partial class AnnouncementsNote
    {
        public int noteID { get; set; }
        public string announcementHeader { get; set; }
        public string announcementBody { get; set; }
        public Nullable<int> createdBy { get; set; }
        public Nullable<bool> isDeleted { get; set; }
        public Nullable<System.DateTime> displayStartDate { get; set; }
        public Nullable<System.DateTime> displayEndDate { get; set; }
        public Nullable<int> createdByUserID { get; set; }
    }
}
