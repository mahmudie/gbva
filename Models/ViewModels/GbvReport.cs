using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rmc.Models.ViewModels
{
    public class GbvReport
    {
        public Consent Consent{get;set;}
        public Registration Registration{get;set;}
        public IntakeInfo IntakeInfo{get;set;}
        public Authorization Authorization{get;set;}

    }
}
