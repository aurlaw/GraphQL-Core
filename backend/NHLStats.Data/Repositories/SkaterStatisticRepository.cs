
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NHLStats.Core.Data;
using NHLStats.Core.Models;
using Hangfire;

namespace NHLStats.Data.Repositories
{
    public class SkaterStatisticRepository : ISkaterStatisticRepository
    {
        private readonly NHLStatsContext _db;

        public SkaterStatisticRepository(NHLStatsContext db)
        {
            _db = db;
        }

        public async Task<List<SkaterStatistic>> Get(int playerId, int? limit = null, int? offset = null, bool? sortAsc=null)
        {
            var results = _db.SkaterStatistics.Include(ss=>ss.Season).Include(ss=>ss.League).Include(ss=>ss.Team).Where(ss => ss.PlayerId == playerId)
                .Where(ss => ss.Season.Name.Contains("Regular"));
            if(sortAsc.HasValue) 
            {
                if(sortAsc.Value) 
                {
                    results = results.OrderByDescending(ss => ss.Season.Name);
                } else
                 {
                    results = results.OrderBy(ss => ss.Season.Name);
                }
            } 
            else 
            {
                results = results.OrderByDescending(ss => ss.Season.Name);
            }
            if(offset.HasValue) 
            {
                results = results.Skip(offset.Value);
            }
            if(limit.HasValue) 
            {
                results = results.Take(limit.Value);
            }
            
            return await results.ToListAsync();
        }


        public async Task AddRange(List<SkaterStatistic> statList)
        {

            await _db.SkaterStatistics.AddRangeAsync(statList);
            await _db.SaveChangesAsync();
        }

        public async Task<List<Season>> GetAllSeasons() 
        {
            return await _db.Seasons.Where(s => s.Name.Contains("Regular")).OrderBy(s => s.Name).ToListAsync();
        }

        public async Task<List<Team>> GetAllTeams()
        {
            return await _db.Teams.OrderBy(t => t.Name).ToListAsync();
        }

        public async Task<List<League>> GetAllLeagues()
        {
             return await _db.Leagues.ToListAsync();   
        }

    }
}
