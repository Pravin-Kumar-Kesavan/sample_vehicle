using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trimble_Transportation.Models
{
    public class Transportation
    {
         static Transportation transport=null;
        public static Transportation getInstance()
        {
            if (transport == null)
            {
                transport = new Transportation();
                return transport;
            }
            else
            {
                return transport;
            }
        }
        
    }
}
