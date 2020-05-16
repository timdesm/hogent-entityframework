using System;
using System.Collections.Generic;
using System.Text;

namespace FootballConsole.managers
{
    class EditManager
    {

        public static void EditMenu()
        {
            Boolean runMenu = true;
            while (runMenu)
            {
                Program.printHeader();
                Console.WriteLine("----- [EDIT MENU] -----");
                Console.WriteLine("[1] EDIT TEAMS");
                Console.WriteLine("[2] EDIT PLAYERS");
                Console.WriteLine("[3] MAKE TRANSFER");
                Console.WriteLine("[4] GO BACK");
                Console.Write("Selection?: ");
                String selection = Console.ReadLine();

                switch (selection)
                {
                    case "1":
                        TeamManager.ManageTeam();
                        break;
                    case "2":
                        PlayerManager.ManagePlayers();
                        break;
                    case "3":
                        TransferManager.ManageTransfers();
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
    }
}
