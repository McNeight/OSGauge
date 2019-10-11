// <copyright file="Program.cs" company="Neil McNeight">
// Copyright © 2008 fatmantwo.
// Copyright © 2019 Neil McNeight. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for
// full license information.
// </copyright>

using System;
using System.Windows.Forms;

namespace OSGaugeTest
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using (var frm = new Form1())
            {
                Application.Run(frm);
            }
        }
    }
}
