using System.ComponentModel.DataAnnotations;

namespace manipulationDonneesEFCore.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        // Foreign Key optionnelle
        public int? MembershipTypeId { get; set; }

        // Navigation properties optionnelles
        public MembershipType? MembershipType { get; set; }
        public ICollection<Movie>? Movies { get; set; }
    }
}
