namespace ShoppingLikeFiles.DataAccessLogic.Entities
{
    /// <summary>
    /// Class <c>Caff</c> entity to store CAFF information 
    /// (<seealso href="https://www.crysys.hu/downloads/vihima06/2020/CAFF.txt" />)
    /// </summary>
    public class Caff : EntityBase
    {
        /// <summary>
        /// Creator name of the file.
        /// </summary>
        public string Creator { get; set; } = string.Empty;

        /// <summary>
        /// File download url.
        /// </summary>
        public string FilePath { get; set; } = string.Empty;

        /// <summary>
        /// Date the <c>CAFF</c> file was created.
        /// </summary>
        public DateTime CreationDateTime { get; set; } = DateTime.UtcNow;



        /// <summary>
        /// Tags in the file. Each separated by <c>';'</c>.
        /// </summary>
        public string Tags { get; set; } = string.Empty;

        /// <summary>
        /// Download path to the generated thumbnail image.
        /// </summary>
        public string ThumbnailPath { get; set; } = string.Empty;

        /// <summary>
        /// Caption of the file.
        /// </summary>
        public string Caption { get; set; } = string.Empty;

        /// <summary>
        /// <see cref="Comment"/>s for a single caff image 
        /// </summary>
        public List<Comment> Comments { get; set; } = new();
    }
}
