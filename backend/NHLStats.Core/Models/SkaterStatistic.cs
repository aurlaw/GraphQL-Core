 

namespace NHLStats.Core.Models
{
    public class SkaterStatistic
    {
        public int Id { get; set; }
        public int SeasonId { get; set; }
        public int LeagueId { get; set; }
        public int TeamId { get; set; }
        public int PlayerId { get; set; }
        public int GamesPlayed { get; set; }
        public int Goals { get; set; }
        public int Assists { get; set; }
        public int Points { get; set; }
        public int PenaltyMinutes { get; set; }
        public int? PlusMinus { get; set; }

        public virtual Season Season { get; set; }
        public virtual League League { get; set; }
        public virtual Team Team { get; set; }
    }
}
