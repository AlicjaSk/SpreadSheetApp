using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadSheetApp
{
    class ValidationException: Exception
    {
        public ValidationException()
        {

        }
        public ValidationException(string message):
            base(message)
        {

        }
    }
}
