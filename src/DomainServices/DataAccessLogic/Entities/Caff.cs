using Microsoft.EntityFrameworkCore;

namespace ShoppingLikeFiles.DataAccessLogic.Entities
{
    public class Caff : EntityBase
    {
        public string FilePath { get; set; } = string.Empty;
        public DateTime CreationDateTime { get; set; } = DateTime.UtcNow;
        public string Creator { get; set; } = string.Empty;
        public string Tags { get; set; } = string.Empty;
        public string ThumbnailPath { get; set; } = string.Empty;
        public string Caption { get; set; } = string.Empty;
        public List<Comment> Comments { get; set; } = new();
    }
}
