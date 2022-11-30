namespace ShoppingLikeFiles.DomainServices.Exceptions;


public class InvalidCaffException : Exception
{
    public InvalidCaffException() : base("CAFF file invalid!") { }

    public InvalidCaffException(Exception? innerException) : base("CAFF file invalid!", innerException) { }
}

