namespace student_final.System.Exceptions;

public class ItemAlreadyExists : Exception
{
    public ItemAlreadyExists(string? message) : base(message)
    {
    }
}