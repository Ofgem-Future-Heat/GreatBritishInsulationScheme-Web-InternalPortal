﻿namespace Ofgem.GBI.InternalPortal.Service.Models.Links
{
    public class BuiltBy : Link
    {
        public BuiltBy(string href, string @class = "") : base(href, @class: @class)
        {        
        }

        public override string Render()
        {
            return $"<a href = \"{Href}\" target=\"_blank\" class=\"{Class}\">Office of Gas and Electricity Markets</a>";
        }
    }
}
