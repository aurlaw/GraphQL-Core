using Microsoft.AspNetCore.Http;

namespace GraphQLApi.Ui.Playground {

    public class GraphQLPlaygroundOptions {

        public PathString Path { get; set; } = "/ui/playground";

        /// <summary>
        /// The GraphQL EndPoint
        /// </summary>
        public PathString GraphQLEndPoint { get; set; } = "/graphql";

    }

}
