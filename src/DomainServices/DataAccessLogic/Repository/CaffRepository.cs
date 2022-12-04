using ShoppingLikeFiles.DataAccessLogic.Context;
using ShoppingLikeFiles.DataAccessLogic.Entities;

namespace ShoppingLikeFiles.DataAccessLogic.Repository;

internal class CaffRepository : GenericRepository<Caff>, ICaffRepository
{
    public CaffRepository(ShoppingLikeFliesDbContext dbContext) : base(dbContext)
    {
    }

    public async Task AddCommentAsync(int id, Comment comment)
    {
        var e = await GetAsync(id);
        e.Comments.Add(comment);
        await base.SaveChangesAsync();
    }
}
