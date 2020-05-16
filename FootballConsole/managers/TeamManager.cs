using FootballLibrary;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace FootballConsole.managers
{
    class TeamManager
    {
        public static void ManageTeam()
        {
            Boolean runMenu = true;
            while (runMenu)
            {
                Program.printHeader();
                Console.WriteLine("----- [TEAM] -----");
                Console.WriteLine("[1] ADD TEAM");
                Console.WriteLine("[2] UPDATE TEAM");
                Console.WriteLine("[3] DELETE TEAM");
                Console.WriteLine("[4] PRINT TEAMS");
                Console.WriteLine("[5] GO BACK");
                Console.Write("Selection?: ");
                String selection = Console.ReadLine();

                switch (selection)
                {
                    case "1":
                        AddTeam();
                        break;
                    case "2":
                        UpdateTeam();
                        break;
                    case "3":
                        DeleteTeam();
                        break;
                    case "4":
                        PrintTeams();
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

        private static void PrintTeams()
        {
            Program.printHeader();
            Console.WriteLine("----- [TEAM LIST] -----");
            using (DataContext ctx = new DataContext())
            {
                foreach (Team team in ctx.Teams)
                    PrintTeam(team);
            }
            Console.WriteLine(" ");
            Console.Write("Press ENTER to continue...");
            Console.ReadLine();
        }

        private static void PrintTeam(Team team)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"ID: {team.ID}, Name: {team.Name}, Nickname: {team.NickName}, Trainer: {team.Trainer}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void AddTeam()
        {
            String teamName;
            String teamNickname;
            String teamTrainer;

            while (true)
            {
                Program.printHeader();
                Console.WriteLine("----- [ADD TEAM] -----");
                Console.Write("Team name?: ");
                teamName = Console.ReadLine();
                if (teamName != "")
                    break;
                else
                {
                    Console.Write("You must fill in a team name, press ENTER to continue...");
                    Console.ReadLine();
                }
            }

            while (true)
            {
                Program.printHeader();
                Console.WriteLine("----- [ADD TEAM] -----");
                Console.Write("Team nickname?: ");
                teamNickname = Console.ReadLine();
                if (teamNickname != "")
                    break;
                else
                {
                    Console.Write("You must fill in a team nickname, press ENTER to continue...");
                    Console.ReadLine();
                }
            }

            while (true)
            {
                Program.printHeader();
                Console.WriteLine("----- [ADD TEAM] -----");
                Console.Write("Team trainer?: ");
                teamTrainer = Console.ReadLine();
                if (teamTrainer != "")
                    break;
                else
                {
                    Console.Write("You must fill in a team trainer, press ENTER to continue...");
                    Console.ReadLine();
                }
            }

            using (DataContext ctx = new DataContext())
            {
                Team t = new Team(0, teamName, teamNickname, teamTrainer);
                ctx.Teams.Add(t);
                ctx.SaveChanges();
            }

            Console.Write("New team has been added, press ENTER to continue...");
            Console.ReadLine();
        }

        private static void UpdateTeam()
        {
            using(DataContext ctx = new DataContext())
            {
                while(true)
                {
                    Program.printHeader();
                    Console.WriteLine("----- [UPDATE TEAM] -----");
                    Console.Write("Team ID?: ");
                    String selection = Console.ReadLine();
                    if (Int32.TryParse(selection, out int teamID))
                    {
                        if (ctx.Teams.Where(x => x.ID == teamID).Any())
                        {
                            UpdateTeam(ctx.Teams.Where(x => x.ID == teamID).First());
                            ctx.SaveChanges();
                            break;
                        }
                        else
                        {
                            Console.Write("No team found with that ID, press ENTER to continue...");
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

        private static void UpdateTeam(Team team)
        {
            Boolean runMenu = true;
            while (runMenu)
            {
                Program.printHeader();
                Console.WriteLine("----- [UPDATE TEAM] -----");
                Console.WriteLine($"Team: {team.ToString()}");
                Console.WriteLine(" ");
                Console.WriteLine("[1] UPDATE NAME");
                Console.WriteLine("[2] UPDATE NICKNAME");
                Console.WriteLine("[3] UPDATE TRAINER");
                Console.WriteLine("[4] GO BACK");
                Console.Write("Selection: ");
                String selection = Console.ReadLine();
                switch (selection)
                {
                    case "1":
                        UpdateTeamName(team);
                        break;
                    case "2":
                        UpdateTeamNickname(team);
                        break;
                    case "3":
                        UpdateTeamTrainer(team);
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


        private static void UpdateTeamName(Team team)
        {
            while(true)
            {
                Program.printHeader();
                Console.WriteLine("----- [UPDATE TEAM NAME] -----");
                Console.WriteLine("");
                Console.WriteLine($"Team: {team.ToString()}");
                Console.Write("New team name?: ");
                String newName = Console.ReadLine();
                if(newName != "")
                {
                    team.Name = newName;
                    break;
                }
                else
                {
                    Console.Write("You must fill in a team name, press ENTER to continue...");
                    Console.ReadLine();
                }
            }
        }


        private static void UpdateTeamNickname(Team team)
        {
            while (true)
            {
                Program.printHeader();
                Console.WriteLine("----- [UPDATE TEAM NICKNAME] -----");
                Console.WriteLine("");
                Console.WriteLine($"Team: {team.ToString()}");
                Console.Write("New team nicnname?: ");
                String newName = Console.ReadLine();
                if (newName != "")
                {
                    team.NickName = newName;
                    break;
                }
                else
                {
                    Console.Write("You must fill in a team nickname, press ENTER to continue...");
                    Console.ReadLine();
                }
            }
        }


        private static void UpdateTeamTrainer(Team team)
        {
            while (true)
            {
                Program.printHeader();
                Console.WriteLine("----- [UPDATE TEAM NICKNAME] -----");
                Console.WriteLine("");
                Console.WriteLine($"Team: {team.ToString()}");
                Console.Write("New team nicnname?: ");
                String newName = Console.ReadLine();
                if (newName != "")
                {
                    team.Trainer = newName;
                    break;
                }
                else
                {
                    Console.Write("You must fill in a team nickname, press ENTER to continue...");
                    Console.ReadLine();
                }
            }
        }

        private static void DeleteTeam()
        {
            using (DataContext ctx = new DataContext())
            {
                while (true)
                {
                    Program.printHeader();
                    Console.WriteLine("----- [DELETE TEAM] -----");
                    Console.Write("Team ID?: ");
                    String selection = Console.ReadLine();
                    if (Int32.TryParse(selection, out int teamID))
                    {
                        if (ctx.Teams.Where(x => x.ID == teamID).Any())
                        {
                            ctx.Teams.Remove(ctx.Teams.Where(x => x.ID == teamID).First());
                            ctx.SaveChanges();

                            Console.Write("Team has been deleted, press ENTER to continue...");
                            Console.ReadLine();
                            break;
                        }
                        else
                        {
                            Console.Write("No team found with that ID, press ENTER to continue...");
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
