using FootballLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace FootballConsole.managers
{
    class TransferManager
    {
        public static void ManageTransfers()
        {
            Boolean runMenu = true;
            while (runMenu)
            {
                Program.printHeader();
                Console.WriteLine("----- [TRANSFERS] -----");
                Console.WriteLine("[1] MAKE TRANSFER");
                Console.WriteLine("[2] PRINT TRANSFERS");
                Console.WriteLine("[3] GO BACK");
                Console.Write("Selection?: ");
                String selection = Console.ReadLine();

                switch (selection)
                {
                    case "1":
                        MakeTransfer();
                        break;
                    case "2":
                        PrintTransfers();
                        break;
                    case "3":
                        runMenu = false;
                        break;
                    default:
                        Console.Write("Wrong selection input, press ENTER to continue...");
                        Console.ReadLine();
                        break;
                }
            }
        }

        private static void MakeTransfer()
        {
            Player player;
            Team oldTeam = null;
            Team newTeam;
            int price;

            while (true)
            {
                Program.printHeader();
                Console.WriteLine("----- [MAKE TRANSFER] -----");
                Console.Write("Player ID?: ");
                String playerIDStr = Console.ReadLine();
                if (Int32.TryParse(playerIDStr, out int playerID))
                    using (DataContext ctx = new DataContext())
                    {
                        if (ctx.Teams.Where(x => x.ID == playerID).Any())
                        {
                            player = ctx.Players.Where(x => x.ID == playerID).First();
                            break;
                        }
                        else
                        {
                            Console.Write("No player found with that ID, press ENTER to continue...");
                            Console.ReadLine();
                        }
                    }
                else
                {
                    Console.Write("You must fill in a valid player id, press ENTER to continue...");
                    Console.ReadLine();
                }
            }

            if(player.TeamID != 0)
            {
                using (DataContext ctx = new DataContext())
                {
                    if (ctx.Teams.Where(x => x.ID == player.TeamID).Any())
                    {
                        oldTeam = ctx.Teams.Where(x => x.ID == player.TeamID).First();
                    }
                }
            }

            while (true)
            {
                Program.printHeader();
                Console.WriteLine("----- [MAKE TRANSFER] -----");
                Console.Write("New Team ID?: ");
                String teamIDStr = Console.ReadLine();
                if (Int32.TryParse(teamIDStr, out int teamID))
                    using (DataContext ctx = new DataContext())
                    {
                        if (ctx.Teams.Where(x => x.ID == teamID).Any())
                        {
                            newTeam = ctx.Teams.Where(x => x.ID == teamID).First();
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
                Console.WriteLine("----- [MAKE TRANSFER] -----");
                Console.Write("Transfer price?: ");
                String priceStr = Console.ReadLine();
                if (Int32.TryParse(priceStr, out price))
                {
                    if(price > 0)
                    {
                        break;
                    }
                    else
                    {
                        Console.Write("Price must be more than 0, press ENTER to continue...");
                        Console.ReadLine();
                    }
                }
                else
                {
                    Console.Write("Input was not a valid number, press ENTER to continue...");
                    Console.ReadLine();
                }
            }

            using (DataContext ctx = new DataContext())
            {
                player.Team = newTeam;
                player.TeamID = newTeam.ID;
                newTeam.Players.Add(player);
                if(oldTeam != null)
                {
                    oldTeam.Players.Remove(player);
                }
                Transfer transfer = new Transfer(0, price, player, oldTeam, newTeam);
                ctx.Transfers.Add(transfer);
                ctx.SaveChanges();
            }

            Console.Write("New transfer has been added, press ENTER to continue...");
            Console.ReadLine();
        }

        private static void PrintTransfers()
        {
            Program.printHeader();
            Console.WriteLine("----- [TRANSFER LIST] -----");
            using (DataContext ctx = new DataContext())
            {
                foreach (Transfer transfer in ctx.Transfers)
                    PrintTransfer(transfer);
            }
            Console.WriteLine(" ");
            Console.Write("Press ENTER to continue...");
            Console.ReadLine();
        }
        private static void PrintTransfer(Transfer transfer)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"ID: {transfer.ID}, Player:  {transfer.PlayerID}, Old Team:  {transfer.OldTeamID}, New Team:  {transfer.NewTeamID}");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
