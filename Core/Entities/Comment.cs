namespace Core.Entities
{
    public class Comment
    {
        public string Id { get; set; }
        public string CommentMessage { get; set; }
        public string MangaId { get; set; }
        public string UserId { get; set; }
        public Manga Manga { get; set; }
        public User User { get; set; }
    }
}