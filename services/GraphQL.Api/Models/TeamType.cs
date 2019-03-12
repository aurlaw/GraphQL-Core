using GraphQL.Types;
using NHLStats.Core.Data;
using NHLStats.Core.Models;
namespace GraphQL.Api.Models
{
    public class TeamType : ObjectGraphType<Team>
    {
        public TeamType()
        {
            Field(x => x.Id);
            Field(x => x.Name);
            Field(x => x.Abbreviation);

        }
    }
}