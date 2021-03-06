﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Data
{
    /// <summary>
    /// Data fielf function control
    /// </summary>
    [Flags]
    public enum FunctionControl
    {
        /// <summary>
        /// Full control
        /// </summary>
        Default = 0,
        /// <summary>
        /// Allow select
        /// </summary>
        Read = 1,
        /// <summary>
        /// Allow insert
        /// </summary>
        Create = 2,
        /// <summary>
        /// Allow update
        /// </summary>
        Update = 4,
        /// <summary>
        /// Full control
        /// </summary>
        Full = Read | Create | Update
    }
}
