namespace student_final.System.Exceptions;

public class ItemsDoNotExist : Exception
{
    public ItemsDoNotExist(string? message) : base(message)
    {
    }
}