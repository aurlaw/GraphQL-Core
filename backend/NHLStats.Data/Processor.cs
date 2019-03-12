using NHLStats.Core;
using NHLStats.Core.Models;
using System;
namespace NHLStats.Data
{
    public class Processor : IProcessor
    {
        public Processor()
        {
            
        }
        public void Process(Player player)
        {
            Console.WriteLine($"processed player {player.Name}");
        }

        public void Process(int playerId)
        {
            Console.WriteLine($"processed player {playerId}");
        }
    }
}