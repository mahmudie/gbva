using System;
using System.Collections.Generic;

namespace rmc.Models
{
    public partial class AgencyType
    {
        public AgencyType()
        {
            AuthorizationSub = new HashSet<AuthorizationSub>();
        }

        public int AgencyTypeId { get; set; }
        public string AgencyType1 { get; set; }

        public virtual ICollection<AuthorizationSub> AuthorizationSub { get; set; }
    }
}
