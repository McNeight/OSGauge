// <copyright file="RoundFancy.cs" company="Neil McNeight">
// Copyright © 2008 fatmantwo.
// Copyright © 2019 Neil McNeight. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for
// full license information.
// </copyright>

using System;
using System.ComponentModel;

namespace OSGauge
{
    /// <summary>
    /// Represents a <see cref="RoundFancy"/> <see cref='System.Windows.Forms.Panel'/> control.
    /// </summary>
    public partial class RoundFancy : Round
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoundFancy"/> class.
        /// </summary>
        public RoundFancy()
        : base()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RoundFancy"/> class.
        /// </summary>
        /// <param name="container">The <see cref="IContainer"/> to add 'this' to.</param>
        public RoundFancy(IContainer container)
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
