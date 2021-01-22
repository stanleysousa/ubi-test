using System.Collections.Generic;

namespace Inventory.Common.Model
{
    public class ValidationResult
    {
        public bool IsValid { get; set; }

        public List<string> Errors { get; private set; }

        public ValidationResult()
        {
            IsValid = true;
            Errors = new List<string>();
        }
    }
}
