using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using BreakingBad.Model;

namespace BreakingBad
{
    class Program
    {
        //Main Program of Console Application
        static void Main(string[] args)
        {
            bool showMenu = true;
            while (showMenu)
            {
                showMenu = MainMenu();
            }
        }

        //Creates HttpClient and set its properties
        private static HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://www.breakingbadapi.com/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }
        
        //Main menu for the application. User chooses next step here.
        private static bool MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1) Show all episodes");
            Console.WriteLine("2) Show all deaths");
            Console.WriteLine("3) Show all characters");
            Console.WriteLine("4) Show all quotes");
            Console.WriteLine("5) Show quotes by author");
            Console.WriteLine("6) Exit");
            Console.Write("\r\nSelect an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    GetAllEpisodes().Wait();
                    return true;
                case "2":
                    GetAllDeaths().Wait();
                    return true;
                case "3":
                    GetAllCharacters().Wait();
                    return true;
                case "4":
                    GetAllQuotes().Wait();
                    return true;
                case "5":
                    SubMenuForCharacterQuotes();
                    return true;
                case "6":
                    return false;
                default:
                    return true;
            }
        }
        
        //Provides list of episodes.
        private static async Task GetAllEpisodes()
        {
            try
            {
                HttpClient client = GetClient();

                HttpResponseMessage response = await client.GetAsync("episodes");
                response.EnsureSuccessStatusCode();

                List<EpisodeBB> episodes = await response.Content.ReadAsAsync<List<EpisodeBB>>();

                foreach (var ep in episodes)
                {
                    Console.WriteLine("Episode number: {0}\nTitle: {1}\nSeason: {2}\nSeries: {3}\n", ep.Episode_id, ep.Title, ep.Season, ep.Series);
                }

                Console.ReadLine();                
            }
            
            catch (HttpRequestException e)
            {
                Console.WriteLine("{0}", e.Message);
            }
        }

        //Provides list of deaths.
        private static async Task GetAllDeaths()
        {
            try
            {
                HttpClient client = GetClient();

                HttpResponseMessage response = await client.GetAsync("deaths");
                    response.EnsureSuccessStatusCode();

                List<DeathBB> deaths = await response.Content.ReadAsAsync<List<DeathBB>>();

                foreach (var d in deaths)
                {
                    Console.WriteLine("Who is dead: {0}\nCause: {1}\nLast words: {2}\n", d.Death, d.Cause, d.Last_Words);
                }
                
                Console.ReadLine();
            }

            catch (HttpRequestException e)
            {
                Console.WriteLine("{0}", e.Message);
            }
        }

        //Provides list of quotes.
        private static async Task GetAllQuotes()
        {
            try
            {
                HttpClient client = GetClient();

                HttpResponseMessage response = await client.GetAsync("quotes");
                response.EnsureSuccessStatusCode();

                List<QuoteBB> quotes = await response.Content.ReadAsAsync<List<QuoteBB>>();

                foreach (var q in quotes)
                {
                    Console.WriteLine("Author: {0}\nQuote: {1}\n", q.Author, q.Quote);
                }

                Console.ReadLine();
            }

            catch (HttpRequestException e)
            {
                Console.WriteLine("{0}", e.Message);
            }
        }

        //Provides list of characters.
        private static async Task GetAllCharacters()
        {
            try
            {
                HttpClient client = GetClient();

                HttpResponseMessage response = await client.GetAsync("characters");
                response.EnsureSuccessStatusCode();

                List<Character> characters = await response.Content.ReadAsAsync<List<Character>>();

                foreach (var c in characters)
                {
                    Console.WriteLine("Name: {0}\nNickName: {1}\nStatus: {2}\nPortrayed by: {3}\n", c.Name, c.NickName, c.Portrayed);
                }

                Console.ReadLine();
            }

            catch (HttpRequestException e)
            {
                Console.WriteLine("{0}", e.Message);
            }
        }

        //Provides quotes of the respective chcracter.
        private static async Task GetQuotesByAuthor(string author)
        {
            try
            {              
                HttpClient client = GetClient();

                HttpResponseMessage response = await client.GetAsync($"quote?author={author}");

                response.EnsureSuccessStatusCode();

                List<QuoteBB> quotes = await response.Content.ReadAsAsync<List<QuoteBB>>();

                if (quotes.Count==0)
                {
                    Console.WriteLine($"There are no quotes of {author}");
                }
                else
                {
                    Console.WriteLine($"Author: {author}\n");

                    foreach (var q in quotes)
                    {
                        Console.WriteLine("Author: {0}\nQuote: {1}\n", q.Author, q.Quote);
                    }
                }              
                
                Console.ReadLine();
            }

            catch (HttpRequestException e)
            {
                Console.WriteLine("{0}", e.Message);
            }
        }

        //Submenu for characters' quotes.
        private static void SubMenuForCharacterQuotes()
        {
            bool showMenu = true;
            while (showMenu)
            {
                showMenu = CharacterListMenu();
            }
        }

        //List of available characters.
        private static bool CharacterListMenu()
        {
            Console.WriteLine("Whose quotes you want to see?");
            Console.WriteLine("1) Walter White");
            Console.WriteLine("2) Jesse Pinkman");
            Console.WriteLine("3) Gustavo Fring");
            Console.WriteLine("4) Exit");
            Console.Write("\r\nSelect an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    GetQuotesByAuthor ("Walter White").Wait();
                    return true;
                case "2":
                    GetQuotesByAuthor("Jesse Pinkman").Wait();
                    return true;
                case "3":
                    GetQuotesByAuthor("Gustavo Fring").Wait();
                    return true;
                case "4":                    
                    return false;                    
                default:
                    return true;                    
            }
        }
    }
}

