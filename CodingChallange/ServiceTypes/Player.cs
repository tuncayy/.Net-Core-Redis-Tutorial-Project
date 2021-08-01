using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodingChallange.ServiceTypes
{
    public class Player
    {
        public Player(string playerName)
        {
            PlayerName = playerName;
            Account = 10000;
        }
        public Player(string playerName, int account)
        {
            PlayerName = playerName;
            Account = account;
        }
        public string PlayerName { get; set; }
        public int Account { get; set; }
        public string Points { get; set; }
    }
}
