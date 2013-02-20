using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace V1.ViewModel
{
    public class IssueViewModel
    {
        public string IssueId { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public Nullable<bool> IssueVisibility { get; set; }
        public Nullable<bool> CreatorVisibility { get; set; }
        public Nullable<byte> Category_ID { get; set; }
        public string CreatorID { get; set; }
        public string Comment_ID { get; set; }
        public System.DateTime Creation_date { get; set; }
        public string PIN { get; set; }
    
    }
}