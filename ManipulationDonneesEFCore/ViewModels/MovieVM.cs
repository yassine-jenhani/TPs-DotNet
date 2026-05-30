using manipulationDonneesEFCore.Models;

namespace manipulationDonneesEFCore.ViewModels
{
    public class MovieVM
    {
        public Movie movie { get; set; }
        public IFormFile photo { get; set; }
    }
}
