using System;
using System.Collections.Generic;
using System.Text;

namespace ADO.Data.Exceptions
{
    public class DomainException:Exception
    {
        public DomainException(string message):base(message) { }
    }
}
