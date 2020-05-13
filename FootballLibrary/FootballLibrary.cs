using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace FootballLibrary
{
    public class FootBall
    {
        private List<Player> Players = new List<Player>();
        private List<Team> Teams = new List<Team>();
        private List<Transfer> Transfers = new List<Transfer>();

        public FootBall() {}

        public void AddPlayer(Player player)
        {
            Players.Add(player);
        }

        public void AddTeam(Team team)
        {
            Teams.Add(team);
        }

        public void AddTransfer(Transfer transfer)
        {
            Transfers.Add(transfer);
        }

        public Player GetPlayer(int ID)
        {
            return Players.First(x => x.ID.Equals(ID));
        }

        public Team GetTeam(int ID)
        {
            return Teams.First(x => x.ID.Equals(ID));
        }

        public Transfer GetTransfer(int ID)
        {
            return Transfers.First(x => x.ID.Equals(ID));
        }

        public List<Player> GetPlayers()
        {
            return Players;
        }

        public List<Team> GetTeams()
        {
            return Teams;
        }

        public List<Transfer> GetTransfers()
        {
            return Transfers;
        }

        public void UpdatePlayer(Player player)
        {
            int position = Players.FindIndex(x => x.ID.Equals(player.ID));
            Players[position] = player;
        }

        public void UpdateTeam(Team team)
        {
            int position = Teams.FindIndex(x => x.ID.Equals(team.ID));
            Teams[position] = team;
        }
    }

    public class Player
    {
        public Player(int ID, int Number, String Name, double TotalScore, int teamID)
        {
            this.ID = ID;
            this.Number = Number;
            this.Name = Name;
            this.TotalScore = TotalScore;
            this.Team = null;
            this.TeamID = teamID;
        }

        public Player(int ID, int Number, String Name, double TotalScore, Team team)
        {
            this.ID = ID;
            this.Number = Number;
            this.Name = Name;
            this.TotalScore = TotalScore;
            this.Team = team;
            this.TeamID = team.ID;
        }

        [Key]
        public int ID { get; set; }
        public int Number { get; set; }
        public String Name { get; set; }
        public Double TotalScore { get; set; }
        public int TeamID { get; set; }

        [NotMapped]
        public Team Team { get; set; }
    }

    public class Team
    {
        public Team(int ID, String name, string nickName, string trainer)
        {
            this.ID = ID;
            this.Name = name;
            this.NickName = nickName;
            this.Trainer = trainer;
            this.Players = new List<Player>();
        }

        [Key]
        public int ID { get; set; }
        public String Name { get; set; }
        public String NickName { get; set; }
        public String Trainer { get; set; }
        public ICollection<Player> Players { get; set; }

        public override string ToString()
        {
            return $"[ID {this.ID}] {this.Name}";
        }
    }

    public class Transfer
    {
        public Transfer(int ID, Double price, int oldTeamID, int newTeamID)
        {
            this.ID = ID;
            this.Price = price;
            this.OldTeamID = oldTeamID;
            this.NewTeamID = newTeamID;
        }

        public Transfer(int ID, double price, Team oldTeam, Team newTeam)
        {
            this.ID = ID;
            this.Price = price;
            this.OldTeam = oldTeam;
            this.OldTeamID = oldTeam.ID;
            this.NewTeam = newTeam;
            this.NewTeamID = newTeam.ID;
        }

        [Key]
        public int ID { get; set; }
        public Double Price { get; set; }
        public int OldTeamID { get; set; }
        public int NewTeamID { get; set; }


        [NotMapped]
        public Team OldTeam { get; set; }
        [NotMapped]
        public Team NewTeam { get; set; }
    }
}
