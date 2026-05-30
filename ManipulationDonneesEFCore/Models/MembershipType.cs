namespace manipulationDonneesEFCore.Models
{
    public class MembershipType
    {
        public int Id { get; set; }
        public float SignUpFee { get; set; }
        public byte DurationInMonth { get; set; }
        public float DiscountRate { get; set; }

        // Navigation property optionnelle
        public ICollection<Customer>? Customers { get; set; }
    }
}
