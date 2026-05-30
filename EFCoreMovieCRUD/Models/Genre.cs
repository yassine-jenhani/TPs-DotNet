namespace EFCoreMovieCRUD.Models
{
    public class Genre
    {
        public Guid Id
        {
            get;
            set;
        }
        public string?
            Name
        { get; set; }

        // Navigation inverse
        public ICollection<Movie> Movies { get; set; } = new List<Movie>();
    }
}
