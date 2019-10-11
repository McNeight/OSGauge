﻿// <copyright file="DoubleBuffered.cs" company="Neil McNeight">
// Copyright © 2008 fatmantwo.
// Copyright © 2019 Neil McNeight. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for
// full license information.
// </copyright>

using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace OSGauge
{
    /// <summary>
    /// Represents a <see cref="DoubleBuffered"/> <see cref='Panel'/> control.
    /// </summary>
    public partial class DoubleBuffered : Panel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DoubleBuffered"/> class.
        /// </summary>
        public DoubleBuffered()
        : base()
        {
            this.InitializeComponent();
            this.SetStyle(
            ControlStyles.UserPaint |
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.DoubleBuffer, true);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DoubleBuffered"/> class.
        /// </summary>
        /// <param name="container">The <see cref="IContainer"/> to add 'this' to.</param>
        public DoubleBuffered(IContainer container)
            : this()
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            container.Add(this);
        }
    }
}
