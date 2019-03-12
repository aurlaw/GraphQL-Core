 using GraphQL.Types;
using NHLStats.Core.Models;
namespace GraphQL.Api.Models
{
    public class StatusResultType: ObjectGraphType<Status>
    {
        public StatusResultType()
        {
            // Field<EnumerationGraphType<Status>>("statusType", resolve: context => context.Source.StatusType);
            Field(x => x.Id);
            Field(x => x.Message);
            Field<StatusTypeEnum>("statusType", resolve: context => context.Source.StatusType);

        }
    }

    public class StatusTypeEnum : EnumerationGraphType<StatusType>
    {
    } 
}