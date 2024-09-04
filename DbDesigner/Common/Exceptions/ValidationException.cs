using Common.Dtos;

namespace Common.Exceptions;

public class ValidationException : Exception
{
    public List<ValidationDto> ValidationResult;
    
    public ValidationException(string fieldName, string? message)
    {
        var validation = Create(fieldName, message);
        ValidationResult = new List<ValidationDto> { validation };
    }

    private ValidationDto Create(string fieldName, string? message)
    {
        var validation = new ValidationDto
        {
            FieldName = fieldName,
            Message = message ?? "Uncaught exception"
        };
        return validation;
    }
}