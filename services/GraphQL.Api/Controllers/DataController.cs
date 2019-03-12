using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NHLStats.Core;
using NHLStats.Core.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
using NHLStats.Core.Models;

namespace GraphQL.Api.Controllers
{
	[Route("api/[controller]")]
	public class DataController : Controller
	{
		private readonly IPlayerRepository _playerRepo;
		private readonly ISkaterStatisticRepository _skaterRepo;

		public DataController(IPlayerRepository playerRepository, ISkaterStatisticRepository skaterStatistic) 
		{
			_playerRepo = playerRepository;
			_skaterRepo = skaterStatistic;
		}
		// GET: api/data/players
		[HttpGet("players")]
		public async Task<IEnumerable<Player>> Get()
		{
			return await _playerRepo.All();

		}


		// GET api/values/5
		[HttpGet("{id}")]
		public string Get(int id)
		{
			return "value";
		}

	}
}
