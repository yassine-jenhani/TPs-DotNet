using System.ComponentModel.DataAnnotations;

namespace manipulationDonneesEFCore.Models
{
    public class Movie
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int? GenreId { get; set; }
        public Genre Genre { get; set; }

        // Nouvelles propriétés
        public string? ImageFile { get; set; }
        public DateTime? DateAjoutMovie { get; set; }

        public ICollection<Customer> Customers { get; set; }
    }
}
