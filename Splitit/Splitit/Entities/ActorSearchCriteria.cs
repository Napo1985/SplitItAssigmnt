namespace Splitit.Splitit.Entities
{
    public class ActorSearchCriteria
    {
        public string ?ActorName { get; set; }
        public int? MinRank { get; set; }
        public int? MaxRank { get; set; }
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 100;
    }
}

