using NHLStats.Core.Models;

namespace NHLStats.Core
{
    public interface IProcessor
    {
         void Process(Player player);
         void Process(int playerId);
    }
}