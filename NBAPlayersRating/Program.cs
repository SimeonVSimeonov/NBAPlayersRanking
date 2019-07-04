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
            StringBuilder stringBuilder = new StringBuilder();
            int currentYear = DateTime.UtcNow.Year;

            string pathToDataJSON = Console.ReadLine();
            int maxPlayedYears = int.Parse(Console.ReadLine());
            int minQualifyRating = int.Parse(Console.ReadLine());
            string dataSaveDirectory = Console.ReadLine();

            StreamReader reader = new StreamReader(pathToDataJSON);
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

            File.AppendAllText(dataSaveDirectory, stringBuilder.ToString());
        }
    }
}
