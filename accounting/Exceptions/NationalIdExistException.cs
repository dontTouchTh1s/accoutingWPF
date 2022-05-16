using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accounting.Exceptions
{
    class NationalIdExistException : Exception
    {
        public NationalIdExistException() { }
        public NationalIdExistException(string nationalId) : base(string.Format("People with national id {0} currently exist", nationalId))
        { }
    }
}
