using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace OSGaugesLib
{
    public partial class OSG_Round_Fancy : OSG_Round
    {
        public OSG_Round_Fancy()
        {
            InitializeComponent();
        }

        public OSG_Round_Fancy(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }


    }
}
