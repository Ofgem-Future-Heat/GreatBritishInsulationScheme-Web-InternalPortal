using System;
using System.Collections.Generic;
using System.Text;

namespace Ofgem.GBI.InternalPortal.Service.Models.Links
{
    public class GbisHome : Link
    {
        public GbisHome(string href, string @class = "") : base(href, @class: @class)
        {
        }

        public override string Render()
        {
            return $"<div class=\"{Class}\">Great British Insulation Scheme</div>";
        }
    }
}