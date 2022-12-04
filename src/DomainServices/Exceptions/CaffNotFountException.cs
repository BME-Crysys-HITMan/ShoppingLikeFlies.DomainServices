namespace ShoppingLikeFiles.DomainServices.Exceptions;

public class CaffNotFountException : Exception
{
    public CaffNotFountException() : base("CAFF not found!") { }
    public CaffNotFountException(int id) : base($"CAFF with id {id} not found!") { }
}
