using GraphQL.Types;
namespace GraphQL.Api.Models
{
    public class SkaterStatisticInputType : InputObjectGraphType
    {
        public SkaterStatisticInputType()
        {
            Name = "SkaterStatisticInput";
            Field<IntGraphType>("seasonId");
            Field<IntGraphType>("leagueId");
            Field<IntGraphType>("teamId");

            Field<IntGraphType>("gamesPlayed");
            Field<IntGraphType>("goals");
            Field<IntGraphType>("assists");
            Field<IntGraphType>("points");
            Field<IntGraphType>("plusMinus");

        }
    }
}
