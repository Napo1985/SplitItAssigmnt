using Splitit.Splitit.Dto;
using Swashbuckle.AspNetCore.Filters;

namespace Splitit.Swagger
{
    public class GetAllActorsExample : IExamplesProvider<IEnumerable<NameAndIdActorDto>>
    {
        public IEnumerable<NameAndIdActorDto> GetExamples()
        {
            return new List<NameAndIdActorDto>
            {
                new NameAndIdActorDto (  "1","name1"),
                new NameAndIdActorDto (   "1","name2")
            };
        }
    }

    public class GetActorsExample : IExamplesProvider<DetailedActorDto>
    {
        public DetailedActorDto GetExamples()
        {
            return new DetailedActorDto("1", "name", "some details", "type string", new Splitit.ValueObjects.Rank(1), "source"); 
        }
    }

    public class OperationSuccessExample : IExamplesProvider<string>
    {
        public string GetExamples()
        {
            return new string("Success");
        }
    }
}

