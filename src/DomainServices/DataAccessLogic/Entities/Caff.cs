using Microsoft.EntityFrameworkCore;

namespace ShoppingLikeFiles.DataAccessLogic.Entities
{
    public class Caff : EntityBase<Caff>
    {
        public Caff()
        {
            Tags = new HashSet<CaffToTag>();
            Comments = new HashSet<Comment>();
        }
        public string FilePath { get; set; } = string.Empty;
        public DateTime CreationDateTime { get; set; }
        public string Creator { get; set; } = string.Empty;
        public ICollection<CaffToTag> Tags { get; set; }
        public string ThumbnailPath { get; set; } = string.Empty;
        public ICollection<Caption> Captions { get; set; }
        public ICollection<Comment> Comments { get; set; }

        public override void IncludeAll(IQueryable<Caff> queryable)
        {
            queryable.Include(x => x.Tags).ThenInclude(x => x.CaffTag);
            queryable.Include(x => x.Comments);
            queryable.Include(x => x.Captions);
        }
    }
}
