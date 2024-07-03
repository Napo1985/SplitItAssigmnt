using Moq;
using Splitit.Infra.Providers;
using Splitit.Splitit.Dto;
using Splitit.Splitit.Entities;
using Splitit.Splitit.Repositories;
using Splitit.Splitit.Services;
using Splitit.Splitit.ValueObjects;

namespace SplititTests.Splitit.Services
{
    public class ActorServiceTests
    {
        [Fact]
        public void GetAllActors_ReturnsRankFilteredActors()
        {
            var mockRepository = new Mock<IActorRepository>();
            var mockProvider = new Mock<IActorProvider>();
            var service = new ActorService(mockRepository.Object, mockProvider.Object);

            var criteria = new ActorSearchCriteria
            {
                MinRank = 5,
                MaxRank = 10
            };

            var mockActors = new List<Actor>
            {
                new Actor("John Doe", "Details", "Type", new Rank(7), "Source", "1"),
                new Actor("Jane Smith", "Details", "Type", new Rank(8), "Source" , "2"),
                new Actor("Michael Johnson", "Details", "Type", new Rank(12), "Source", "3")
            };

            mockRepository.Setup(r => r.GetAll()).Returns(mockActors);

            var result = service.GetAllActors(criteria);

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void GetAllActors_ReturnsNameFilteredActors()
        {
            var mockRepository = new Mock<IActorRepository>();
            var mockProvider = new Mock<IActorProvider>();
            var service = new ActorService(mockRepository.Object, mockProvider.Object);

            var criteria = new ActorSearchCriteria
            {
                ActorName = "John Doe"
            };

            var mockActors = new List<Actor>
            {
                new Actor("John Doe", "Details", "Type", new Rank(7), "Source", "1"),
                new Actor("Jane Smith", "Details", "Type", new Rank(8), "Source" , "2"),
                new Actor("Michael Johnson", "Details", "Type", new Rank(12), "Source", "3")
            };

            mockRepository.Setup(r => r.GetAll()).Returns(mockActors);

            var result = service.GetAllActors(criteria);

            Assert.Equal(1, result.Count());
        }

        [Fact]
        public void GetAllActors_ReturnsPartNameFilteredActors()
        {
            var mockRepository = new Mock<IActorRepository>();
            var mockProvider = new Mock<IActorProvider>();
            var service = new ActorService(mockRepository.Object, mockProvider.Object);

            var criteria = new ActorSearchCriteria
            {
                ActorName = "J"
            };

            var mockActors = new List<Actor>
            {
                new Actor("John Doe", "Details", "Type", new Rank(7), "Source", "1"),
                new Actor("Jane Smith", "Details", "Type", new Rank(8), "Source" , "2"),
                new Actor("Michael Johnson", "Details", "Type", new Rank(12), "Source", "3")
            };

            mockRepository.Setup(r => r.GetAll()).Returns(mockActors);

            var result = service.GetAllActors(criteria);

            Assert.Equal(3, result.Count());
        }

        [Fact]
        public void GetActorById_ReturnsCorrectActor()
        {
            var mockRepository = new Mock<IActorRepository>();
            var mockProvider = new Mock<IActorProvider>();
            var service = new ActorService(mockRepository.Object, mockProvider.Object);

            var actorId = "1";
            var mockActor = new Actor("John Doe", "Details", "Type", new Rank(7), "Source", actorId);

            mockRepository.Setup(r => r.GetById(actorId)).Returns(mockActor);

            var result = service.GetActorById(actorId);

            Assert.NotNull(result);
            Assert.Equal(actorId, result.Id);
        }

        [Fact]
        public void AddActor_ThrowsInvalidOperationException_WhenRankIsDuplicate()
        {
            var mockRepository = new Mock<IActorRepository>();
            var mockProvider = new Mock<IActorProvider>();
            var service = new ActorService(mockRepository.Object, mockProvider.Object);

            var actorDto = new DetailedActorDto(null, "John Doe", "Details", "Type", new Rank(7), "Source");

            mockRepository.Setup(r => r.GetAll()).Returns(new List<Actor>
            {
                new Actor("Existing Actor", "Details", "Type", new Rank(7), "Source", "1")
            });

            Assert.Throws<InvalidOperationException>(() => service.AddActor(actorDto));
        }

        [Fact]
        public void UpdateActor_ThrowsInvalidOperationException_WhenActorNotFound()
        {
            var mockRepository = new Mock<IActorRepository>();
            var mockProvider = new Mock<IActorProvider>();
            var service = new ActorService(mockRepository.Object, mockProvider.Object);

            var actorId = "1";
            var actorDto = new DetailedActorDto(actorId, "John Doe", "Details", "Type", new Rank(7), "Source");

            mockRepository.Setup(r => r.GetById(actorId)).Returns((Actor)null);

            Assert.Throws<InvalidOperationException>(() => service.UpdateActor(actorId, actorDto));
        }

        [Fact]
        public void DeleteActor_RemovesActorFromRepository()
        {
            var mockRepository = new Mock<IActorRepository>();
            var mockProvider = new Mock<IActorProvider>();
            var service = new ActorService(mockRepository.Object, mockProvider.Object);

            var actorId = "1";

            service.DeleteActor(actorId);

            mockRepository.Verify(r => r.Delete(actorId), Times.Once);
        }

        [Fact]
        public void GetActorsFromImdb_ReturnsActorsFromProvider()
        {
            var mockRepository = new Mock<IActorRepository>();
            var mockProvider = new Mock<IActorProvider>();
            var service = new ActorService(mockRepository.Object, mockProvider.Object);

            var mockActors = new List<Actor>
            {
                new Actor("John Doe", "Details", "Type", new Rank(7), "Source", "1"),
                new Actor("Jane Smith", "Details", "Type", new Rank(8), "Source", "2"),
                new Actor("Michael Johnson", "Details", "Type", new Rank(12), "Source", "3")
            };

            mockProvider.Setup(p => p.GetActorsAsync()).ReturnsAsync(mockActors);

            var result = service.GetActorsFromImdb();

            Assert.Equal(mockActors, result);
        }

    }
}

