using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceX.Infrastructure.Exceptions
{
    public class IntegrationException : BaseException
    {
        public IntegrationException(string message) : base(ErrorCodes.IntegrationError, message)
        {
        }
    }
}
