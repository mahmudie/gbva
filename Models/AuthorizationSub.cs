using System;
using System.Collections.Generic;

namespace rmc.Models
{
    public partial class AuthorizationSub
    {
        public int AuthorizationSubId { get; set; }
        public int? AuthorizationId { get; set; }
        public int? AgencyTypeId { get; set; }
        public string AgencyName { get; set; }
        public string Comment { get; set; }

        public virtual AgencyType AgencyType { get; set; }
        public virtual Authorization Authorization { get; set; }
    }
}
