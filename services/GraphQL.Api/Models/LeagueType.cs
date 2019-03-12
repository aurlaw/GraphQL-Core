using GraphQL.Types;
using NHLStats.Core.Data;
using NHLStats.Core.Models;
namespace GraphQL.Api.Models
{
    public class LeagueType : ObjectGraphType<League>
    {
        public LeagueType()
        {
            Field(x => x.Id);
            Field(x => x.Name);
            Field(x => x.Abbreviation);
        }
    }
}