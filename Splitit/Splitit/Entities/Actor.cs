using Splitit.Splitit.ValueObjects;

namespace Splitit.Splitit.Entities
{
    public class Actor
    {
        public string? Id { get; set; }

        public string Name { get; set; }

        public string Details { get; set; }

        public string Type { get; set; }

        public Rank Rank { get; set; }

        public string Source { get; set; }

        public Actor( string name, string details, string type, Rank rank, string source, string? id = null)
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

