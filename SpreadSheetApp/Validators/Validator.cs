using System.Linq;


namespace SpreadSheetApp.Validators
{
    class Validator
    {
        private string _line;
        public string ErrorMessage { get; set; }
        private const string Caution = "Please use appropriate format!";

        private string GetFullErrorMessage()
        {
            return ErrorMessage + " " + Caution;
        }

        public void Validate(string line)
        {
            _line = line;
            if (IsNull() ||
                IsEmpty() ||
                !DoesItContainSeparator())
            {
                throw new ValidationException(GetFullErrorMessage());
            }
        }
        
        private bool IsEmpty()
        {
            ErrorMessage = "You provided empty input!";
            return (_line == "") ? true : false;
        }

        private bool IsNull()
        {
            ErrorMessage = "You provided null input!";
            return (_line == null) ? true : false;
        }

        private bool DoesItContainSeparator()
        {
            if (_line.Contains(Globals.EndOfSheet)) return true;
            ErrorMessage = "You provided input without any separators!";
            return (_line.Contains(Globals.InputSeparator)) ? true : false;
        }

    }
}
