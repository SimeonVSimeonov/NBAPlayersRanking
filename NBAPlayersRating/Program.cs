namespace NBAPlayersRating
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    class Program
    {
        static void Main()
        {
            int maxPlayedYears = int.MinValue;
            string inputYears = string.Empty;
            bool isValidYears = false;

            int minQualifyRating = int.MinValue;
            string inputRating = string.Empty;
            bool isValidRating = false;

            StringBuilder stringBuilder = new StringBuilder();
            int currentYear = DateTime.UtcNow.Year;

            Console.WriteLine(Phrase.hello);  

            while (!isValidYears)
            {
                Console.Write(Phrase.maxPlayedPhrase);
                inputYears = Console.ReadLine();
                isValidYears = int.TryParse(inputYears, out maxPlayedYears);
            }

            while (!isValidRating)
            {
                Console.Write(Phrase.minQualifyPfrase);
                inputRating = Console.ReadLine();
                isValidRating = int.TryParse(inputRating, out minQualifyRating);
            }

            using (StreamReader reader = new StreamReader(Routes.pathToDataJSON))
            {
                string dataJSON = reader.ReadToEnd();
                List<Player> allPlayers = JsonConvert.DeserializeObject<List<Player>>(dataJSON);

                List<Player> selectedPlayers = allPlayers
               .Where(player => player.Rating >= minQualifyRating && player.PlayingSince >= currentYear - maxPlayedYears)
               .OrderByDescending(player => player.Rating)
               .ToList();


                stringBuilder.AppendLine("Name, Rating");

                foreach (var player in selectedPlayers)
                {
                    stringBuilder.AppendLine($"{player.Name}, {player.Rating}");
                }
            }

            File.WriteAllText(Routes.dataSaveDirectory, stringBuilder.ToString());

            Console.WriteLine(Phrase.exit);
        }
    }
}
