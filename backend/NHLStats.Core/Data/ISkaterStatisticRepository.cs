
using System.Threading.Tasks;
using System.Collections.Generic;
using NHLStats.Core.Models;

namespace NHLStats.Core.Data
{
    public interface ISkaterStatisticRepository
    {
        Task<List<SkaterStatistic>> Get(int playerId, int? limit = null, int? offset = null, bool? sortAsc=null);

    //    Task<SkaterStatistic> Add(SkaterStatistic stat);
       Task AddRange(List<SkaterStatistic> statList);
 

        Task<List<Season>> GetAllSeasons();

        Task<List<Team>> GetAllTeams();

        Task<List<League>> GetAllLeagues();
    }
}
