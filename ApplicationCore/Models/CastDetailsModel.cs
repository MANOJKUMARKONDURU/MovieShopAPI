using System.Collections.Generic;

namespace ApplicationCore.Models
{
    public class CastDetailsModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProfilePath { get; set; }

        
        
        public List<CastMovieModel> Movies { get; set; } = new();
    }
}

