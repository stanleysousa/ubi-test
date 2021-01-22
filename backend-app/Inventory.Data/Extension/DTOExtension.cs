using Inventory.Common.Model;
using Inventory.DataAccess.DTO;

namespace Inventory.DataAccess.Extension
{
    public static class DTOExtensions
    {
        public static ValidationResult Validate(this ItemDTO item)
        {
            var result = new ValidationResult();

            if(item?.Name.Length > 45)
            {
                result.IsValid = false;
                result.Errors.Add($"{item.Name} is not a valid NAME. Maximum length is 45 characters.");
            }

            if (item?.Game.Length > 45)
            {
                result.IsValid = false;
                result.Errors.Add($"{item.Game} is not a valid GAME. Maximum length is 45 characters.");
            }

            return result;
        }

        public static ValidationResult Validate(this PropertyDTO property)
        {
            var result = new ValidationResult();

            if (property?.Name.Length > 45)
            {
                result.IsValid = false;
                result.Errors.Add($"{property.Name} is not a valid NAME. Maximum length is 45 characters.");
            }

            if (property?.Value.Length > 45)
            {
                result.IsValid = false;
                result.Errors.Add($"{property.Value} is not a valid VALUE. Maximum length is 45 characters.");
            }

            return result;
        }

        public static ValidationResult Validate(this UserDTO user)
        {
            var result = new ValidationResult();

            if (user?.Username.Length > 45)
            {
                result.IsValid = false;
                result.Errors.Add($"{user.Username} is not a valid USERNAME. Maximum length is 45 characters.");
            }

            return result;
        }
    }
}
