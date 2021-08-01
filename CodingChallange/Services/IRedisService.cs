using CodingChallange.ServiceTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodingChallange.Services
{
    public interface IRedisService
    {
        Task<StatusType> GetPoints(string playerName);
        Task<StatusType> SaveUpdateRecord(Player player);
    }
}
