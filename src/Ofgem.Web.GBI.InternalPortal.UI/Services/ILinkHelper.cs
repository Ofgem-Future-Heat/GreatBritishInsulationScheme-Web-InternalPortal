﻿using System;
using Ofgem.GBI.InternalPortal.Service.Models;

namespace Ofgem.GBI.InternalPortal.Service.Services
{
    public interface ILinkHelper
    {
        string RenderListItemLink<T>(bool isSelected = false, string @class = "") where T : Link;
        string RenderLink<T>(Func<string>? before = null, Func<string>? after = null, bool isSelected = false) where T : Link;
    }
}
