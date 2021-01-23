namespace Core.Entities
{
    public class Picture
    {
        public string Id { get; set; }
        public int PictureOrder { get; set; }
        public string MangaId { get; set; }
        public Manga Manga { get; set; }
        public string ImageLocation { get; set; }
    }
}