﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace FluentDAO.Atrributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class IgnoreAttribute:Attribute
    {
    }
}
