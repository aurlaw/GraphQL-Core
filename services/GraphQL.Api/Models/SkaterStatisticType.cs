 using GraphQL.Types;
using NHLStats.Core.Models;

namespace GraphQL.Api.Models
{
    public class SkaterStatisticType : ObjectGraphType<SkaterStatistic>
    {
        public SkaterStatisticType()
        {
            Field(x => x.Id);
            Field(x => x.SeasonId);
            Field(x => x.Season.Name).Name("season");
            Field(x => x.League.Abbreviation).Name("league");
            Field(x => x.Team.Name).Name("team");

            Field(x => x.GamesPlayed).Name("gp");
            Field(x => x.Goals);
            Field(x => x.Assists);
            Field(x => x.Points);
            // Field(x => x.PlusMinus);
            // cannot explicitly set nullible type
            Field<IntGraphType>("plusMinus", resolve: context => context.Source.PlusMinus);

        }
    }
}

