using FootballLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FootballConsole.managers
{
    class PlayerManager
    {
        public static void ManagePlayers()
        {
            Boolean runMenu = true;
            while (runMenu)
            {
                Program.printHeader();
                Console.WriteLine("----- [PLAYERS] -----");
                Console.WriteLine("[1] ADD PLAYER");
                Console.WriteLine("[2] UPDATE PLAYER");
                Console.WriteLine("[3] DELETE PLAYER");
                Console.WriteLine("[4] PRINT PLAYERS");
                Console.WriteLine("[5] GO BACK");
                Console.Write("Selection?: ");
                String selection = Console.ReadLine();

                switch (selection)
                {
                    case "1":
                        AddPlayer();
                        break;
                    case "2":
                        UpdatePlayer();
                        break;
                    case "3":
                        DeletePlayer();
                        break;
                    case "4":
                        PrintPlayers();
                        break;
                    case "5":
                        runMenu = false;
                        break;
                    default:
                        Console.Write("Wrong selection input, press ENTER to continue...");
                        Console.ReadLine();
                        break;
                }
            }
        }
        private static void PrintPlayers()
        {
            Program.printHeader();
            Console.WriteLine("----- [TEAM LIST] -----");
            using (DataContext ctx = new DataContext())
            {
                foreach (Player player in ctx.Players)
                    PrintPlayer(player);
            }
            Console.WriteLine(" ");
            Console.Write("Press ENTER to continue...");
            Console.ReadLine();
        }

        private static void PrintPlayer(Player player)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"ID: {player.ID}, Name: {player.Name}, Number: {player.Number}, Score: {player.TotalScore}, Team: {player.TeamID}, Value: {player.Value}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void AddPlayer()
        {
            String playerName;
            int playerNumber;
            Double playerScore;
            Team playerTeam;
            int playerValue;

            while (true)
            {
                Program.printHeader();
                Console.WriteLine("----- [ADD PLAYER] -----");
                Console.Write("Player name?: ");
                playerName = Console.ReadLine();
                if (playerName != null)
                    break;
                else
                {
                    Console.Write("You must fill in a player name, press ENTER to continue...");
                    Console.ReadLine();
                }
            }

            while (true)
            {
                Program.printHeader();
                Console.WriteLine("----- [ADD PLAYER] -----");
                Console.Write("Player Number?: ");
                String playerNumberStr = Console.ReadLine();
                if (Int32.TryParse(playerNumberStr, out playerNumber))
                    break;
                else
                {
                    Console.Write("You must fill in a valid player number, press ENTER to continue...");
                    Console.ReadLine();
                }
            }

            while (true)
            {
                Program.printHeader();
                Console.WriteLine("----- [ADD PLAYER] -----");
                Console.Write("Player Score?: ");
                String playerScoreStr = Console.ReadLine();
                if (Double.TryParse(playerScoreStr, out playerScore))
                    break;
                else
                {
                    Console.Write("You must fill in a valid player score, press ENTER to continue...");
                    Console.ReadLine();
                }
            }

            while (true)
            {
                Program.printHeader();
                Console.WriteLine("----- [ADD PLAYER] -----");
                Console.Write("Player Team?: ");
                String teamIDStr = Console.ReadLine();
                if (Int32.TryParse(teamIDStr, out int teamID))
                    using (DataContext ctx = new DataContext())
                    {
                        if (ctx.Teams.Where(x => x.ID == teamID).Any())
                        {
                            playerTeam = ctx.Teams.Where(x => x.ID == teamID).First();
                            break;
                        }
                        else
                        {
                            Console.Write("No team found with that ID, press ENTER to continue...");
                            Console.ReadLine();
                        }
                    }
                else
                {
                    Console.Write("You must fill in a valid team id, press ENTER to continue...");
                    Console.ReadLine();
                }
            }

            while (true)
            {
                Program.printHeader();
                Console.WriteLine("----- [ADD PLAYER] -----");
                Console.Write("Player Value?: ");
                String payerValueStr = Console.ReadLine();
                if (Int32.TryParse(payerValueStr, out playerValue))
                    break;
                else
                {
                    Console.Write("You must fill in a valid player value, press ENTER to continue...");
                    Console.ReadLine();
                }
            }

            using (DataContext ctx = new DataContext())
            {
                Player p = new Player(0, playerNumber, playerName, playerScore, playerTeam, playerValue);
                ctx.Players.Add(p);
                ctx.SaveChanges();
            }

            Console.Write("New player has been added, press ENTER to continue...");
            Console.ReadLine();
        }

        private static void UpdatePlayer()
        {
            using (DataContext ctx = new DataContext())
            {
                while (true)
                {
                    Program.printHeader();
                    Console.WriteLine("----- [UPDATE PLAYER] -----");
                    Console.Write("Player ID?: ");
                    String selection = Console.ReadLine();
                    if (Int32.TryParse(selection, out int playerID))
                    {
                        if (ctx.Players.Where(x => x.ID == playerID).Any())
                        {
                            UpdatePlayer(ctx.Players.Where(x => x.ID == playerID).First());
                            ctx.SaveChanges();
                            break;
                        }
                        else
                        {
                            Console.Write("No player found with that ID, press ENTER to continue...");
                            Console.ReadLine();
                            break;
                        }
                    }
                    else
                    {
                        Console.Write("Input was not a valid number, press ENTER to continue...");
                        Console.ReadLine();
                        break;
                    }
                }
            }
        }

        private static void UpdatePlayer(Player player)
        {
            Boolean runMenu = true;
            while (runMenu)
            {
                Program.printHeader();
                Console.WriteLine("----- [UPDATE TEAM] -----");
                Console.WriteLine($"Player: [ID: {player.ID}] {player.Name}");
                Console.WriteLine(" ");
                Console.WriteLine("[1] UPDATE NAME");
                Console.WriteLine("[2] UPDATE NUMBER");
                Console.WriteLine("[3] UPDATE SCORE");
                Console.WriteLine("[4] GO BACK");
                Console.Write("Selection: ");
                String selection = Console.ReadLine();
                switch (selection)
                {
                    case "1":
                        UpdatePlayerName(player);
                        break;
                    case "2":
                        UpdatePlayerNumber(player);
                        break;
                    case "3":
                        UpdatePlayerScore(player);
                        break;
                    case "4":
                        runMenu = false;
                        break;
                    default:
                        Console.Write("Wrong selection input, press ENTER to continue...");
                        Console.ReadLine();
                        break;
                }
            }
        }

        private static void UpdatePlayerName(Player player)
        {
            while (true)
            {
                Program.printHeader();
                Console.WriteLine("----- [UPDATE PLAYER NAME] -----");
                Console.WriteLine($"Player: [ID: {player.ID}] {player.Name}");
                Console.WriteLine("");
                Console.Write("New player name?: ");
                String newName = Console.ReadLine();
                if (newName != "")
                {   
                    player.Name = newName;
                    break;
                }
                else
                {
                    Console.Write("You must fill in a player name, press ENTER to continue...");
                    Console.ReadLine();
                }
            }
        }

        private static void UpdatePlayerNumber(Player player)
        {
            while (true)
            {
                Program.printHeader();
                Console.WriteLine("----- [UPDATE PLAYER NUMBER] -----");
                Console.WriteLine($"Player: [ID: {player.ID}] {player.Name}");
                Console.WriteLine("");
                Console.Write("New player number?: ");
                String newNumber = Console.ReadLine();
                if (Int32.TryParse(newNumber, out int number))
                {
                    player.Number = number;
                    break;
                }
                else
                {
                    Console.Write("Input was not a valid number, press ENTER to continue...");
                    Console.ReadLine();
                }
            }
        }

        private static void UpdatePlayerScore(Player player)
        {
            while (true)
            {
                Program.printHeader();
                Console.WriteLine("----- [UPDATE PLAYER SCORE] -----");
                Console.WriteLine($"Player: [ID: {player.ID}] {player.Name}");
                Console.WriteLine("");
                Console.Write("New player score?: ");
                String newScore = Console.ReadLine();
                if (Double.TryParse(newScore, out double score))
                {
                    player.TotalScore = score;
                    break;
                }
                else
                {
                    Console.Write("Input was not a valid score, press ENTER to continue...");
                    Console.ReadLine();
                }
            }
        }


        private static void DeletePlayer()
        {
            using (DataContext ctx = new DataContext())
            {
                while (true)
                {
                    Program.printHeader();
                    Console.WriteLine("----- [DELETE PLAYER] -----");
                    Console.Write("Player ID?: ");
                    String selection = Console.ReadLine();
                    if (Int32.TryParse(selection, out int playerID))
                    {
                        if (ctx.Players.Where(x => x.ID == playerID).Any())
                        {
                            ctx.Players.Remove(ctx.Players.Where(x => x.ID == playerID).First());
                            ctx.SaveChanges();

                            Console.Write("Player has been deleted, press ENTER to continue...");
                            Console.ReadLine();
                            break;
                        }
                        else
                        {
                            Console.Write("No player found with that ID, press ENTER to continue...");
                            Console.ReadLine();
                            break;
                        }
                    }
                    else
                    {
                        Console.Write("Input was not a valid number, press ENTER to continue...");
                        Console.ReadLine();
                        break;
                    }
                }
            }
        }

    }
}
