using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using ConsoleTables;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace TamagotchiAPICS
{
    class Pet
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]

        public string Name { get; set; }
        [JsonPropertyName("birthday")]

        public DateTime Birthday { get; set; }
        [JsonPropertyName("happinessLevel")]

        public int HappinessLevel { get; set; }
        [JsonPropertyName("hungerLevel")]

        public int HungerLevel { get; set; }
    }
    class Program
    {
        static async Task ViewAllPets()
        {
            var client = new HttpClient();
            var responseAsStream = await client.GetStreamAsync("https://localhost:5001/index.html/api/Pets");
            List<Pet> pets = await JsonSerializer.DeserializeAsync<List<Pet>>(responseAsStream);

            Console.WriteLine($"There are {pets.Count()} Pets in the Tamagotchi Pets API");

            var table = new ConsoleTable("Id", "Name", "Birthday", "Happiness Level", "Hunger Level");
            foreach (var pet in pets)
            {
                table.AddRow(pet.Id, pet.Name, pet.Birthday, pet.HappinessLevel, pet.HungerLevel);
            }
            table.Write(Format.Minimal);
        }

        static async Task SelectAPet(int idToSendToApi, string selectionToSendToApi)
        {
            var client = new HttpClient();
            var url = $"https://localhost:5001/index.html/api/Pets/{idToSendToApi}/{selectionToSendToApi};
            var jsonBody = JsonSerializer.Serialize(newItem);
            var jsonBodyAsContent = new StringContent(jsonBody);
            jsonBodyAsContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync(url, jsonBodyAsContent);
            var responseJson = await response.Content.ReadAsStreamAsync();
            var pet = await JsonSerializer.DeserializeAsync<Pet>(responseJson);

        }
        static async Task AddOnePet(Pet newPet)
        {
            var client = new HttpClient();
            var url = "https://localhost:5001/index.html/api/Pets";

            var jsonBody = JsonSerializer.Serialize(newPet);
            var jsonBodyAsContent = new StringContent(jsonBody);
            jsonBodyAsContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync(url, jsonBodyAsContent);
            var responseJson = await response.Content.ReadAsStreamAsync();
            var pet = await JsonSerializer.DeserializeAsync<Pet>(responseJson);

            var table = new ConsoleTable("Id", "Name", "Birthday", "Happiness Level", "Hunger Level");
            table.AddRow(pet.Id, pet.Name, pet.Birthday, pet.HappinessLevel, pet.HungerLevel);
            table.Write(Format.Minimal);
        }

        static async Task DeleteAPet(int idToSendToApi)
        {
            try
            {
                var client = new HttpClient();
                var url = $"https://localhost:5001/index.html/api/Pets/{idToSendToApi}";
                await client.DeleteAsync(url);
            }
            catch (HttpRequestException)
            {
                Console.WriteLine("I could not find that item!");
            }
        }
        static async Task Main(string[] args)
        {
            // While keep going equals true
            var keepGoing = true;
            while (keepGoing)
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("Menu:");
                Console.WriteLine();
                Console.WriteLine("View All Pets (VIEW)");
                Console.WriteLine("Select a Pet (SELECT)");
                Console.WriteLine("Create a Pet (CREATE)");
                Console.WriteLine("Delete a Pet (DELETE)");
                Console.WriteLine("Quit (QUIT)");
                Console.WriteLine();

                Console.Write("Which would you like to choose? ");
                var choice = Console.ReadLine().ToUpper().Trim();

                switch (choice)
                {
                    case "VIEW":
                        await ViewAllPets();

                        Console.WriteLine("Press ENTER to continue");
                        Console.ReadLine();

                        break;

                    case "SELECT":
                        Console.Write("Which Pet (by Id) would you like to Select?");
                        var idSelect = int.Parse(Console.ReadLine());
                        Console.Write("Would you like play with (playtimes), feed (feedings), or scold (scoldings) the selected Pet? ");
                        var selection = Console.ReadLine();

                        await SelectAPet(idSelect, selection);

                        Console.WriteLine("Press ENTER to continue");
                        Console.ReadLine();

                        break;

                    case "CREATE":
                        Console.Write("What is the Name of your New Pet? ");
                        var newName = Console.ReadLine();

                        var newPet = new Pet()
                        {
                            Name = newName
                        };

                        await AddOnePet(newPet);

                        Console.WriteLine("Press ENTER to continue");
                        Console.ReadLine();

                        break;

                    case "DELETE":
                        Console.Write("Which Pet (by Id) would you like to Delete? ");
                        var id = int.Parse(Console.ReadLine());

                        await DeleteAPet(id);

                        Console.WriteLine("Press ENTER to continue");
                        Console.ReadLine();

                        break;

                    case "QUIT":
                        keepGoing = false;
                        break;
                }
            }
        }
    }
}


