using System;
using System.Collections.Generic;
using System.Text;

namespace AuditLogDB
{
    public class Instance
    {
        public virtual int? ID { get; set; }
        public virtual string VariantInstanceID { get; set; }
        public virtual string GeneName { get; set; }
        public virtual string XmlString { get; set; }
        public virtual string EncryptedHashCode { get; set; }
        public virtual DateTime RecordedDate { get; set; }
        public virtual int UploadID { get; set; }
    }
}
