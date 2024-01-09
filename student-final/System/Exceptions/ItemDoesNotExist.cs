namespace student_final.System.Exceptions;

public class ItemDoesNotExist : Exception
{
    public ItemDoesNotExist(string? message) : base(message)
    {
    }
}