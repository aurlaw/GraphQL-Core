using GraphQL;
using GraphQL.Types;

namespace GraphQL.Api.Models
{
    public class NHLStatsSchema : Schema
    {
        public NHLStatsSchema(IDependencyResolver resolver): base(resolver)
        {
            Query = resolver.Resolve<NHLStatsQuery>();
            Mutation = resolver.Resolve<NHLStatsMutation>();
        }
    }}