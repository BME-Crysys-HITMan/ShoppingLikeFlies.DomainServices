using ShoppingLikeFiles.DataAccessLogic.Entities;

namespace ShoppingLikeFiles.DataAccessLogic.Repository;

public interface ICaffRepository : IGenericRepository<Caff>
{
    Task AddCommentAsync(int id, Comment comment);
}
