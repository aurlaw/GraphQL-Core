using GraphQL.Types;
using NHLStats.Core.Data;
using NHLStats.Core.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace GraphQL.Api.Models
{
    public class NHLStatsMutation : ObjectGraphType
    {
        public NHLStatsMutation(IPlayerRepository playerRepository, ISkaterStatisticRepository skaterRepository, ILogger<NHLStatsMutation> logging)
        {
            Name = "PlayerStatMutation";

            Field<PlayerType>(
                "createPlayer",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<PlayerInputType>> { Name = "player" },
                    new QueryArgument<ListGraphType<SkaterStatisticInputType>> { Name = "skaterStats" }
                ),
                resolve: context => 
                {
                    var player = context.GetArgument<Player>("player");
                    var skaterStats = context.GetArgument<List<SkaterStatistic>>("skaterStats");
                    return playerRepository.AddWithSkaterStats(player, skaterStats);
                });

            Field<StatusResultType>(
                "deletePlayer",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "playerId" }
                ),
                resolve: context => 
                {
                    var playerId = context.GetArgument<int>("playerId");
                    return playerRepository.Delete(playerId);
                });

        }
    }
}