using Microsoft.EntityFrameworkCore;
using System;
using FootballLibrary;
using System.IO;
using Microsoft.EntityFrameworkCore.Migrations;
using FootballConsole.managers;

namespace FootballConsole
{
    class Program
    {
        public static String Path = @"..\..\..\data\foot.csv";
        public static FootBall repo;

        static void Main(string[] args)
        {
            repo = new FootBall();
            printHeader();
            ReadDataFile(Path);

            Boolean runMenu = true;
            while (runMenu)
            {
                Program.printHeader();
                Console.WriteLine("----- [MENU] -----");
                Console.WriteLine("[1] UPLOAD DATA");
                Console.WriteLine("[2] EDIT DATA");
                Console.WriteLine("[3] CLOSE APPLICATION");
                Console.Write("Selection?: ");
                String selection = Console.ReadLine();

                switch (selection)
                {
                    case "1":
                        using (var dataContext = new DataContext())
                        using (var context = dataContext.Database.BeginTransaction())
                        {
                            dataContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Teams] ON");
                            TeamAdd(dataContext);
                            dataContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Teams] OFF");

                            dataContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Players] ON");
                            PlayerAdd(dataContext);
                            dataContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Players] OFF");

                            dataContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Transfers] ON");
                            TransferAdd(dataContext);
                            dataContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Transfers] OFF");
                            context.Commit();
                        }
                        break;
                    case "2":
                        EditManager.EditMenu();
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

        private static void ReadDataFile(String path)
        {
            string[] readText = File.ReadAllLines(path);
            for (int i = 1; i < readText.Length; i++)
                BuildObjects(readText[i]);
        }

        private static void BuildObjects(String Line)
        {
            Random random = new Random();
            String[] temp = Line.Split(",");

            if (repo.GetTeams().Find(x => x.ID.Equals(Int16.Parse(temp[4]))) == null)
                repo.AddTeam(new Team(Int16.Parse(temp[4]), temp[2], temp[6], temp[5]));
            Team x = repo.GetTeams().Find(x => x.ID.Equals(Int16.Parse(temp[4])));
            Player p = new Player(repo.GetPlayers().Count + 1, Int32.Parse(temp[1]), temp[0], random.Next(0, 100), x);
            repo.AddPlayer(p);
        }

        public static void PlayerAdd(DataContext context)
        {
            foreach (Player item in repo.GetPlayers())
            {
                context.Players.Add(item);
            }
            context.SaveChanges();
        }
        public static void TeamAdd(DataContext context)
        {
            foreach (var item in repo.GetTeams())
            {
                context.Teams.Add(item);
            }
            context.SaveChanges();
        }
        public static void TransferAdd(DataContext context)
        {
            foreach (var item in repo.GetTransfers())
            {
                context.Transfers.Add(item);
            }
            context.SaveChanges();
        }

        public static void printHeader()
        {
            Console.Clear();
            Console.WriteLine("--------------------------");
            Console.WriteLine("Project created by Tim De Smet");
            Console.WriteLine("HoGent GPS - Football Project");
            Console.WriteLine("--------------------------");
            Console.WriteLine(" ");
        }
    }
}
