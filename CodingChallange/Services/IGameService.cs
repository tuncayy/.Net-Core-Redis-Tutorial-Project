using CodingChallange.ServiceTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodingChallange.Services
{
    public interface IGameService
    {
        Task<ReturnMessage> PlayGame(RequestType input);
    }
}
