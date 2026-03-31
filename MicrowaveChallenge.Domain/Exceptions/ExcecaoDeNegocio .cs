using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrowaveChallenge.Domain.Exceptions
{
    public class ExcecaoDeNegocio : Exception
    {
        public ExcecaoDeNegocio(string message) : base(message)
        {
        }
    }
}
