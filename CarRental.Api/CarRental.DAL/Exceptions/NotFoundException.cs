using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.DAL.Exceptions
{
    public class NotFoundException: Exception
    {
        public NotFoundException(string message): base(message)
        {}
    }
}
