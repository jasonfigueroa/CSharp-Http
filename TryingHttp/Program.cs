using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;


namespace TryingHttp
{
    class Movie
    {
        public string Title { get; set; }
        public string PosterPath { get; set; }
        public string ReleaseDate { get; set; }
        public double VoteAverage { get; set; }

        public void PrintMovie()
        {
            Console.WriteLine("movie");
            Console.WriteLine("---------------");
            Console.WriteLine($"title: {this.Title}");
            Console.WriteLine($"poster_path: {this.PosterPath}");
            Console.WriteLine($"release_date: {this.ReleaseDate}");
            Console.WriteLine($"vote_average: {this.VoteAverage}");
            Console.WriteLine();
        }
    }

    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        static void Main(string[] args)
        {
            RunAsync().GetAwaiter().GetResult();
        }
        static async Task RunAsync()
        {
            string search = "Toy Story 3";
            string condensedSearch = search.Replace(" ", "+");
            string apiKey = "858deec9a8305f575390bb92f4c3eab8";
            string url = $"https://api.themoviedb.org/3/search/movie?api_key={apiKey}&query={condensedSearch}";

            var responseString = await client.GetStringAsync($"{url}");

            JObject json = JObject.Parse(responseString);
            JToken movieToken = json.GetValue("results")[0];

            Movie toyStory3 = new Movie
            {
                Title = movieToken.Value<string>("title"),
                PosterPath = movieToken.Value<string>("poster_path"),
                ReleaseDate = movieToken.Value<string>("release_date"),
                VoteAverage = movieToken.Value<double>("vote_average")
            };

            toyStory3.PrintMovie();
        }
    }
}
