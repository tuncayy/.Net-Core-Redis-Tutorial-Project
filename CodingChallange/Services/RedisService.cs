using CodingChallange.ServiceTypes;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodingChallange.Services
{
    public class RedisService : IRedisService
    {
        private readonly IDistributedCache _distributedCache;
        public RedisService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }
        public async Task<StatusType> GetPoints(string playerName)
        {
            try{
                var value = await _distributedCache.GetStringAsync(playerName); //Get points data if player have data in cache
                if (String.IsNullOrEmpty(value))
                {
                    return new StatusType{isSuccess = true,Value = 10000}; // If there is no data, return initial point value
                }
                return new StatusType{isSuccess = true,Value = Convert.ToInt32(value)}; // retrun saved points
            }
            catch(Exception e)
            {
                return new StatusType{isSuccess = false,Message = e.ToString()};
            }
        }
        public async Task<StatusType> SaveUpdateRecord(Player player)
        {
            //Since IDistributedCache dont have update function, This function do remove and set operations. 
            try
            {
                if (await IsPlayerExist(player.PlayerName))
                {
                    await _distributedCache.RemoveAsync(player.PlayerName);
                }
                await _distributedCache.SetStringAsync(player.PlayerName, player.Account.ToString());
                return new StatusType {isSuccess = true, Message = "Successfully Saved"} ;
            }
            catch(Exception e)
            {
                return new StatusType {isSuccess = false, Message = e.ToString()} ;
            }
        }
        private async Task<bool> IsPlayerExist(string playerName)
        {
            var value = await _distributedCache.GetStringAsync(playerName);
            return String.IsNullOrEmpty(value) ? true : false;
        }
    }
}