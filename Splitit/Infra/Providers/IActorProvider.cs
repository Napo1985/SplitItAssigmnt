using Splitit.Splitit.Entities;

namespace Splitit.Infra.Providers
{
    public interface IActorProvider
    {
        Task<IEnumerable<Actor>> GetActorsAsync();
    }
}

