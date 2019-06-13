using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mobile.Api.Models
{
    public class Movie
    {
        static readonly IEnumerable<Movie> movies = new[] { new Movie { Id = 1, Name="我是谁" }, new Movie { Id = 2, Name = "超级警察" } };

        public static IEnumerable<Movie> GetAllMovies()
        {
            return movies;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
