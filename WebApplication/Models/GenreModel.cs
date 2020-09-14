using System.Collections.Generic;

namespace WebApplication.Models
{
    public class GenreModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Parent { get; set; }
        public List<int> Subgenres { get; set; }
    }
}