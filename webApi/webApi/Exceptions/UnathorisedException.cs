using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webApi.Exceptions
{
    public class UnathorisedException:Exception
    {
        public UnathorisedException(string message) : base(message) { }
    }
}
