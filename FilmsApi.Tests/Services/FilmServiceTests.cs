using Moq;
using Xunit;
using FilmsApi.Api.Models;
using FilmsApi.Api.Repositories;
using FilmsApi.Api.Services;
using FilmsApi.Api.Exceptions;

namespace FilmsApi.Tests.Services;

public class FilmServiceTests
{
    [Fact]
    public void GetById_FilmExistant_RetourneLeFilm()
    {
        // Arrange
        var film = new Film { Id = 1, Titre = "Inception", Annee = 2010 };

        var mockRepo = new Mock<IRepository<Film>>();
        mockRepo.Setup(r => r.GetById(1)).Returns(film);

        var service = new FilmService(mockRepo.Object);

        // Act
        var resultat = service.GetById(1);

        // Assert
        Assert.Equal("Inception", resultat.Titre);
    }

    [Fact]
    public void GetById_FilmInexistant_LeveNotFoundException()
    {
        // Arrange
        var mockRepo = new Mock<IRepository<Film>>();
        mockRepo.Setup(r => r.GetById(999)).Returns((Film?)null);

        var service = new FilmService(mockRepo.Object);

        // Act + Assert — Assert.Throws exécute l'action et vérifie l'exception
        var exception = Assert.Throws<NotFoundException>(() => service.GetById(999));
        Assert.Contains("999", exception.Message);
    }

    [Fact]
    public void Add_TitreVide_LeveValidationException()
    {
        // Arrange
        var mockRepo = new Mock<IRepository<Film>>();
        var service = new FilmService(mockRepo.Object);
        var film = new Film { Titre = "", Annee = 2010 };

        // Act + Assert
        var exception = Assert.Throws<ValidationException>(() => service.Add(film));
        Assert.Contains("titre", exception.Errors.First(), StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void Add_DonneesValides_AppelleAddSurLeRepository()
    {
        // Arrange
        var mockRepo = new Mock<IRepository<Film>>();
        var service = new FilmService(mockRepo.Object);
        var film = new Film { Titre = "Inception", Annee = 2010 };

        // Act
        service.Add(film);

        // Assert — vérifie que Add a bien été appelé exactement 1 fois avec ce film
        mockRepo.Verify(r => r.Add(film), Times.Once);
    }

    [Theory]
    [InlineData(1800)]   // année trop ancienne
    [InlineData(2050)]   // année trop future
    public void Add_AnneeInvalide_LeveValidationException(int annee)
    {
        var mockRepo = new Mock<IRepository<Film>>();
        var service = new FilmService(mockRepo.Object);
        var film = new Film { Titre = "Test", Annee = annee };

        Assert.Throws<ValidationException>(() => service.Add(film));
    }
}
