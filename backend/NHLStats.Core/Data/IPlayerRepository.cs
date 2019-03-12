﻿
using System.Collections.Generic;
using System.Threading.Tasks;
using NHLStats.Core.Models;

namespace NHLStats.Core.Data
{
    public interface IPlayerRepository
    {
        Task<Player> Get(int id);
        Task<Player> GetRandom();
        Task<List<Player>> All();
        Task<Player> Add(Player player);

        Task<Player> AddWithSkaterStats(Player player, List<SkaterStatistic> statsList);


        Task<Status> Delete(int id);

    }
}
