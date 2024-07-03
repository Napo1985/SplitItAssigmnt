using Swashbuckle.AspNetCore.Filters;

namespace Splitit.Swagger
{
	public class InvalidOperationResponseExample : IExamplesProvider<object>
    {
        public object GetExamples()
        {
            return new
            {
                StatusCode = 409,
                Message = "Invalid operation",
                Detailed = "An actor with the rank 12 already exists."
            };
        }
    }

    public class BadRequestExample : IExamplesProvider<object>
    {
        public object GetExamples()
        {
            return new
            {
                StatusCode = 400,
                Message = "Bad request",
                Detailed = "Invalid actor details provided."
            };
        }
    }

    public class NotFoundExample : IExamplesProvider<object>
    {
        public object GetExamples()
        {
            return new
            {
                StatusCode = 404,
                Message = "Not found",
                Detailed = "Id not found."
            };
        }
    }
    
}

