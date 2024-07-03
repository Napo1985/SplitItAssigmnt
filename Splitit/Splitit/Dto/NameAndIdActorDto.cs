namespace Splitit.Splitit.Dto
{
    public class NameAndIdActorDto
    {
        public NameAndIdActorDto(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Id { get; set; }
        public string Name { get; set; }
    }
}

