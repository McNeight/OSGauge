using System;
using System.Collections.Generic;
using System.Text;

namespace OSGaugesLib
{
    public interface OSG_IRound
    {
        

        int Minimum
        {
            get;
            set;
        }

        int Maximum
        {
            get;
            set;
        }

        int Value
        {
            get;
            set;
        }



    }
}
