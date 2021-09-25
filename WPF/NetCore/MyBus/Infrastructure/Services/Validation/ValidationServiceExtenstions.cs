using System.Collections;

namespace MyBus.Infrastructure.Services.Validation
{
    public static class ValidationServiceExtenstions
    {
        public static IEnumerable IsNullOrWhiteSpace(this Base.ValidationService validationService, string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                yield return "Поле не может быть пустым";
        }
    }
}
