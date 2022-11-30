namespace ShoppingLikeFiles.DomainServices.Service;

/// <summary>
/// This class represents a payment transaction.
/// </summary>
public interface IPaymentService
{
    /// <summary>
    /// Imitates a caff purchase.
    /// </summary>
    /// <param name="userId">Id of user who is buying the product</param>
    /// <param name="price">Price that the user is paying</param>
    /// <param name="caffId">Id of the caff file</param>
    /// <returns>Returns ture if user is able to purchase the item with the given money. Otherwise return false.</returns>
    Task<bool> BuyItemAsync(Guid userId, int price, Guid caffId);
}
