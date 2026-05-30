namespace EFCoreMovieCRUD.Models
{
    public class Movie
    {
        public int Id
        {
            get;
            set;
        }
        public string?
            Name
        { get; set; }

        // Clé étrangère
        public Guid GenreId { get; set; }

        // Navigation
        public Genre? Genre { get; set; }

        public Movie()
        {
        }
    }
}
