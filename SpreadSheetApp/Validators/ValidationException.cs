using System;


namespace SpreadSheetApp.Validators
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
