using GraphQL.Types;
using System.Linq;
using NHLStats.Core.Data;
using NHLStats.Core.Models;
using Microsoft.Extensions.Logging;

namespace GraphQL.Api.Models
{
    public class NHLStatsQuery : ObjectGraphType
    {
        public NHLStatsQuery(IPlayerRepository playerRepository, ISkaterStatisticRepository skaterStatisticRepository)
        {
            
            Field<PlayerType>(
                "player",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" }),
                resolve: context =>  playerRepository.Get(context.GetArgument<int>("id")));

            Field<PlayerType>(
                "randomPlayer",
                resolve: context => playerRepository.GetRandom());

            Field<ListGraphType<PlayerType>>(
                "players",
                resolve: context => playerRepository.All());


            Field<ListGraphType<LeagueType>>(
                "leagues",
                resolve: context =>  skaterStatisticRepository.GetAllLeagues());

            Field<ListGraphType<TeamType>>("teams",
                resolve: context =>  skaterStatisticRepository.GetAllTeams());

            Field<ListGraphType<SeasonType>>("seasons",
                resolve: context =>  skaterStatisticRepository.GetAllSeasons());


        }
    }
}