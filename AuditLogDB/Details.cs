using System;
using System.Collections.Generic;
using System.Text;

namespace AuditLogDB
{
    /// <summary>
    /// Use to check if an existing variant has been updated or deleted
    /// </summary>
    public class Details
    {
        public virtual int? ID { get; set; }
        public virtual string CheckSum { get; set; }
        public virtual string Status { get; set; }
        public virtual Instance Instance { get; set; }
        public virtual int TransactionID { get; set; } 
    }
}
