using Splitit.Splitit.Entities;
using Splitit.Splitit.Repositories;

namespace Splitit.Infra.Repositories
{
    public class InMemoryActorRepository : IActorRepository
    {
        private readonly List<Actor> _actors = new List<Actor>();
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();
        private static int _currentId = 1;

        public IEnumerable<Actor> GetAll()
        {
            _lock.EnterReadLock();
            try
            {
                return _actors.ToList();
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public Actor GetById(string id)
        {
            _lock.EnterReadLock();
            try
            {
                return _actors.FirstOrDefault(a => a.Id == id);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public string Add(Actor actor)
        {
            _lock.EnterWriteLock();
            try
            {
                actor.Id = _currentId.ToString();
                _currentId++;
                _actors.Add(actor);
                return actor.Id;
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public void Update(Actor actor)
        {
            _lock.EnterWriteLock();
            try
            {
                var existingActor = _actors.FirstOrDefault(a => a.Id == actor.Id);
                if (existingActor != null)
                {
                    existingActor.Name = actor.Name;
                    existingActor.Details = actor.Details;
                    existingActor.Type = actor.Type;
                    existingActor.Rank = actor.Rank;
                    existingActor.Source = actor.Source;
                }
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public void Delete(string id)
        {
            _lock.EnterWriteLock();
            try
            {
                var actor = _actors.FirstOrDefault(a => a.Id == id);
                if (actor != null)
                {
                    _actors.Remove(actor);
                }
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }
    }
}

