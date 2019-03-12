 
using System;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
using GraphQL.Api.Models;
using Microsoft.Extensions.Logging;

namespace GraphQL.Api.Controllers
{

    [Route("[controller]")] 
    public class GraphQLController : Controller
    {
        private readonly IDocumentExecuter _documentExecuter;
        private readonly ISchema _schema;
        private readonly ILogger<GraphQLController> _logger;

        public GraphQLController(ISchema schema, IDocumentExecuter documentExecuter, ILogger<GraphQLController> logging)
        {
            _schema = schema;
            _documentExecuter = documentExecuter;
            _logger = logging;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GraphQLQuery query)
        {
            _logger.LogInformation("Executing GraphQL");

            if (query == null) { throw new ArgumentNullException(nameof(query)); }
            var inputs = query.Variables.ToInputs();
            var executionOptions = new ExecutionOptions
            {
                Schema = _schema,
                Query = query.Query,
                Inputs = inputs
            };

            var result = await _documentExecuter.ExecuteAsync(executionOptions).ConfigureAwait(false);

            if (result.Errors?.Count > 0)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }    
}