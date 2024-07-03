using Splitit.Infra.Repositories;
using Splitit.Splitit.Entities;
using Splitit.Splitit.ValueObjects;

namespace SplititTests.Infra.Repositories
{
    public class InMemoryActorRepositoryTests
    {
        [Fact]
        public void GetAll_ReturnsAllActors()
        {
            var repository = new InMemoryActorRepository();
            repository.Add(new Actor("John Doe", "Details", "Type", new Rank(1), "Source"));
            repository.Add(new Actor("Jane Smith", "Details", "Type", new Rank(2), "Source"));

            var actors = repository.GetAll();

            Assert.Equal(2, actors.Count());
        }

        [Fact]
        public void GetById_ReturnsCorrectActor()
        {
            var repository = new InMemoryActorRepository();
            var id = repository.Add(new Actor("John Doe", "Details", "Type", new Rank(1), "Source"));

            var actor = repository.GetById(id);

            Assert.NotNull(actor);
            Assert.Equal("John Doe", actor.Name);
        }

        [Fact]
        public void Add_AddsNewActor()
        {
            var repository = new InMemoryActorRepository();
            var actor = new Actor("John Doe", "Details", "Type", new Rank(1), "Source");

            var id = repository.Add(actor);
            var addedActor = repository.GetById(id);

            Assert.NotNull(addedActor);
            Assert.Equal("John Doe", addedActor.Name);
        }

        [Fact]
        public void Update_UpdatesExistingActor()
        {
            var repository = new InMemoryActorRepository();
            var id = repository.Add(new Actor("John Doe", "Details", "Type", new Rank(1), "Source"));
            var updatedActor = new Actor("Jane Smith", "Updated details", "Updated type", new Rank(2), "Updated source", id);

            repository.Update(updatedActor);
            var retrievedActor = repository.GetById(id);

            Assert.NotNull(retrievedActor);
            Assert.Equal("Jane Smith", retrievedActor.Name);
            Assert.Equal("Updated details", retrievedActor.Details);
            Assert.Equal("Updated type", retrievedActor.Type);
            Assert.Equal(2, retrievedActor.Rank.Value);
            Assert.Equal("Updated source", retrievedActor.Source);
        }

        [Fact]
        public void Delete_RemovesActor()
        {
            var repository = new InMemoryActorRepository();
            var id = repository.Add(new Actor("John Doe", "Details", "Type", new Rank(1), "Source"));

            repository.Delete(id);
            var deletedActor = repository.GetById(id);

            Assert.Null(deletedActor);
        }

        [Fact]
        public void Delete_RemovesNotExistingActor()
        {
            var repository = new InMemoryActorRepository();
            var id = "1";

            repository.Delete(id);
            var deletedActor = repository.GetById(id);

            Assert.Null(deletedActor);
        }
    }
}

