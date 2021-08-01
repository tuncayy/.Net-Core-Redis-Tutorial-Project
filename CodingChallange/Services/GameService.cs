
using CodingChallange.ServiceTypes;
using System;
using System.Threading.Tasks;

namespace CodingChallange.Services
{
    public class GameService : IGameService
    {
        private readonly IRedisService _redisService;
        public GameService(IRedisService redisService)
        {
            _redisService = redisService;
        }
        public async Task<ReturnMessage> PlayGame(RequestType input)
        {
            //Create new player object
            Player player = new Player(input.PlayerName);
            try
            {
                //Get point value from db and update player object
                var accountOfPlayer = await _redisService.GetPoints(input.PlayerName);
                if(accountOfPlayer.isSuccess)
                {
                    player.Account = accountOfPlayer.Value;
                }
                else
                {
                    return Message(player, accountOfPlayer.Message);
                }
                
                var inputValidation = InputValidation(player,input); //Send objects to validation function
                if(inputValidation.isSuccess)
                {
                    var BetResult = ComparePrediciton(input.Number); //Generate number(0-9) and compare with user input
                    player = CalculateResultPoints(player, BetResult, input.Points); //Calculate new points according to prediction success
                    var isSaveCompleted = await _redisService.SaveUpdateRecord(player); //Save new values to db
                    if (isSaveCompleted.isSuccess)
                    {
                        return Message(player, BetResult ? "Won" : "Lost");
                    }
                    return Message(player, isSaveCompleted.Message);
                }
                return Message(player, inputValidation.Message);
            }
            catch(Exception e)
            {
                return Message(player,e.ToString());
            }
        }
        private StatusType InputValidation(Player player, RequestType input)
        {
                //Check entered points parameter
                if(input.Points <= 0)
                {
                    return new StatusType{isSuccess = false, Message = "Please enter a positive integer value as a point"};
                }
                //Check number range
                if(input.Number < 0 || input.Number > 9)
                {
                    return new StatusType{isSuccess = false, Message = "Please enter a number in 0-9 range"};
                }
                //Check user remaining points before play
                if (player.Account < input.Points)
                {
                    return new StatusType{isSuccess = false, Message = "Don't have enough credit in account"};
                }
                return new StatusType{isSuccess = true};
        }
        private bool ComparePrediciton(int number)
        {
            return number == GenerateNumber();
        }
        private int GenerateNumber()
        {
            Random random = new Random();
            return random.Next(-1, 10);
        }
        private Player CalculateResultPoints(Player player, bool isWon, int points)
        {
            if (isWon)
            {
                player.Account += points * 9;
                player.Points = "+" + (points * 9).ToString();
            }
            else
            {
                player.Account -= points;
                player.Points = "-" + points;
            }
            return player;

        }
        private ReturnMessage Message(Player player, string status)
        {
            return new ReturnMessage
            {
                PlayerName = player.PlayerName,
                Account = player.Account,
                Points = player.Points,
                Status = status,
            };
        }
    }
}
