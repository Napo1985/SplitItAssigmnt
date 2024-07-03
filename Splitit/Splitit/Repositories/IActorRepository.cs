using Splitit.Splitit.Entities;

namespace Splitit.Splitit.Repositories
{
    public interface IActorRepository
    {
        IEnumerable<Actor> GetAll();
        Actor GetById(string id);
        string Add(Actor actor);
        void Update(Actor actor);
        void Delete(string id);
    }
}

