﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Web.API
{
    public class PresentationAssemblyReference
    {
        internal static readonly Assembly Assembly = typeof(PresentationAssemblyReference).Assembly;
    }
}

