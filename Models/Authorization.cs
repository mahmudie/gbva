using System;
using System.Collections.Generic;

namespace rmc.Models
{
    public partial class Authorization
    {
        public Authorization()
        {
            AuthorizationSub = new HashSet<AuthorizationSub>();
        }

        public int AuthorizationId { get; set; }
        public Guid? GbvCaseId { get; set; }
        public bool Islessthan18 { get; set; }
        public bool IsSigned { get; set; }


        public virtual ICollection<AuthorizationSub> AuthorizationSub { get; set; }
        public virtual GbvCase GbvCase { get; set; }
    }
}
