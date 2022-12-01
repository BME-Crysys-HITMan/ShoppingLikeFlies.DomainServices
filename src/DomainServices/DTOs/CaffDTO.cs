namespace ShoppingLikeFiles.DomainServices.DTOs
{
    public class CaffDTO
    {
        public int Id { get; set; }
        public string FilePath { get; set; }
        public ushort Year { get; set; }
        public byte Month { get; set; }
        public byte Day { get; set; }
        public byte Hour { get; set; }
        public byte Minute { get; set; }
        public string Creator { get; set; }
        public List<CaffTagDTO> Tags { get; set; }
        public string ThumbnailPath { get; set; }
        public List<CommentDTO> Comments { get; set; }
        public List<CaptionDTO> Captions { get; set; }
    }
}
