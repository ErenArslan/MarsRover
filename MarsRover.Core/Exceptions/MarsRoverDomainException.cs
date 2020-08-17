using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover.Core.Exceptions
{
  public  class MarsRoverDomainException : Exception
    {
        public MarsRoverDomainException()
        { }

        public MarsRoverDomainException(string message)
            : base(message)
        { }

        public MarsRoverDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
