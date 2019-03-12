
 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NHLStats.Core.Data;
using NHLStats.Core.Models;
using Microsoft.Extensions.Logging;

using Hangfire;
using NHLStats.Core;

namespace NHLStats.Data.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly NHLStatsContext _db;
        private readonly ILogger<PlayerRepository> _logger;
        private readonly IProcessor _processor;
        public PlayerRepository(NHLStatsContext db, ILogger<PlayerRepository>  logger, IProcessor processor)
        {
            _db = db;
            _logger = logger;
            _processor = processor;
        }

        public async Task<Player> Get(int id)
        {
            return await _db.Players.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Player> GetRandom()
        {
            return await _db.Players.OrderBy(o => Guid.NewGuid()).FirstOrDefaultAsync();
        }

        public async Task<List<Player>> All()
        {
            return await _db.Players.ToListAsync();
        }

        public async Task<Player> Add(Player player)
        {
            await _db.Players.AddAsync(player);
            await _db.SaveChangesAsync();

            return player;
        }

        public async Task<Player> AddWithSkaterStats(Player player, List<SkaterStatistic> statsList)
        {
            using(var transaction = _db.Database.BeginTransaction())
            {
                try 
                {
                    await _db.Players.AddAsync(player);
                    await _db.SaveChangesAsync();
                    if(statsList != null && statsList.Any())
                    {
                        foreach(var stat in statsList)
                        {
                            stat.PlayerId = player.Id;
                            await _db.SkaterStatistics.AddAsync(stat);
                        }
                        await _db.SaveChangesAsync();

                    }

                    transaction.Commit();
                    BackgroundJob.Enqueue(() => _processor.Process(player));

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error saving player and stats: {ex.Message}");
                    throw;
                }
            }
            return player;
        }

        public async Task<Status> Delete(int id)
        {
            var status = new Status();
            status.Id = id;
            using(var transaction = _db.Database.BeginTransaction())
            {
                try 
                {
                   var delPlayer = await _db.Players.FirstOrDefaultAsync(p => p.Id == id);     
                    if(delPlayer != null) 
                    {
                        var statsList = await _db.SkaterStatistics.Where(p => p.PlayerId == id).ToArrayAsync();
                        if(statsList != null) 
                        {
                             _db.SkaterStatistics.RemoveRange(statsList);
                             await _db.SaveChangesAsync();
                        }
                        _db.Players.Remove(delPlayer);
                        await _db.SaveChangesAsync();

                    }
                    transaction.Commit();
                    BackgroundJob.Enqueue(() => _processor.Process(id));
                    status.StatusType = StatusType.Deleted;
                    status.Message = $"{delPlayer.Name} deleted";
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error deleting player and stats: {ex.Message}");
                    status.StatusType = StatusType.Error;
                    status.Message = ex.Message;
                }
            }
            return status;
        }


    }
}
