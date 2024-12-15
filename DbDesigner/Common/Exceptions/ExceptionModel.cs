using Common.Dtos;

namespace Common.Exceptions;

public class ExceptionModel : Exception
{
    public ExceptionModel(string message) : base(message)
    {
        Model = new ExceptionDto { Message = message };
    }
    
    public ExceptionDto? Model { get; set; }
}