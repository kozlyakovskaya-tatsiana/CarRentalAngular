using System;

namespace CarRental.DAL.Exceptions
{
    public class NotFoundException: Exception
    {
        public NotFoundException(string message): base(message)
        {}
    }
}
