using ASPCoreFirstApp.Models;
namespace ASPCoreFirstApp.ViewModels
{
    public class RandomMovieViewModel
    {
        public Movie Movie { get; set; } = new Movie();
        public List<Customer> Customers { get; set; } = new List<Customer>();
    }
}
