﻿namespace Ofgem.GBI.InternalPortal.Service.Models.Links
{
    public class CrownCopyright : Link
    {
        public CrownCopyright(string href, string @class = "") : base(href, @class: @class)
        {
        }

        public override string Render()
        {
            return $"<a href = \"{Href}\" class=\"{Class}\">© Crown copyright</a>";
        }
    }
}
