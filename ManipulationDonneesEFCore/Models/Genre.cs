using System.ComponentModel.DataAnnotations;

namespace manipulationDonneesEFCore.Models
{
    public class Genre
    {
        public int Id { get; set; }

        [Required]
        public string GenreName { get; set; }

        // Navigation property
        public ICollection<Movie> Movies { get; set; }
    }
}
