// <copyright file="IRound.cs" company="Neil McNeight">
// Copyright © 2008 fatmantwo.
// Copyright © 2019 Neil McNeight. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for
// full license information.
// </copyright>

namespace OSGauge
{
    /// <summary>
    /// Used to abstract access to classes that represent a round gauge.
    /// </summary>
    public interface IRound
    {
        /// <summary>
        /// Gets or sets the lower limit of values of the displayed range.
        /// </summary>
        int Minimum
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the upper limit of values of the displayed range.
        /// </summary>
        int Maximum
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a numeric value that represents the current position of the needle
        /// on the displayed gauge.
        /// </summary>
        int Value
        {
            get;
            set;
        }
    }
}
