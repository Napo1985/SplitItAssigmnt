using Splitit.Splitit.ValueObjects;

namespace Splitit.Splitit.Dto
{
    public class DetailedActorDto
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public string Type { get; set; }
        public Rank Rank { get; set; }
        public string Source { get; set; }

        public DetailedActorDto(string? id, string name, string details, string type, Rank rank, string source)
        {
            Id = id;
            Name = name;
            Details = details;
            Type = type;
            Rank = rank;
            Source = source;
        }
    }
}

