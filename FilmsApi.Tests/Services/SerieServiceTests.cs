using Moq;
using Xunit;
using FilmsApi.Api.Models;
using FilmsApi.Api.Repositories;
using FilmsApi.Api.Services;
using FilmsApi.Api.Exceptions;

namespace FilmsApi.Tests.Services;

public class SerieServiceTests
{
    [Fact]
    public void GetById_SerieExistante_RetourneLaSerie()
    {
        // Arrange
        var serie = new Serie { Id = 1, Titre = "Mentalist", AnneeDebut = 2008 };

        var mockRepo = new Mock<IRepository<Serie>>();
        mockRepo.Setup(r => r.GetById(1)).Returns(serie);

        var service = new SerieService(mockRepo.Object);

        // Act
        var resultat = service.GetById(1);

        // Assert
        Assert.Equal("Mentalist", resultat.Titre);
    }

    [Fact]
    public void GetById_SerieInexistante_LeveNotFoundException()
    {
        // Arrange
        var mockRepo = new Mock<IRepository<Serie>>();
        mockRepo.Setup(r => r.GetById(999)).Returns((Serie?)null);

        var service = new SerieService(mockRepo.Object);

        // Act + Assert — Assert.Throws exécute l'action et vérifie l'exception
        var exception = Assert.Throws<NotFoundException>(() => service.GetById(999));
        Assert.Contains("999", exception.Message);
    }

    [Fact]
    public void Add_TitreVide_LeveValidationException()
    {
        // Arrange
        var mockRepo = new Mock<IRepository<Serie>>();
        var service = new SerieService(mockRepo.Object);
        var serie = new Serie { Titre = "", AnneeDebut = 2008 };

        // Act + Assert
        var exception = Assert.Throws<ValidationException>(() => service.Add(serie));
        Assert.Contains("titre", exception.Errors.First(), StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void Add_DonneesValides_AppelleAddSurLeRepository()
    {
        // Arrange
        var mockRepo = new Mock<IRepository<Serie>>();
        var service = new SerieService(mockRepo.Object);
        var serie = new Serie { Titre = "Mentalist", AnneeDebut = 2010 };

        // Act
        service.Add(serie);

        // Assert — vérifie que Add a bien été appelé exactement 1 fois avec cette serie
        mockRepo.Verify(r => r.Add(serie), Times.Once);
    }

    [Theory]
    [InlineData(1800, null)]           // AnneeDebut invalide
    [InlineData(2008, 2000)]           // AnneeFin < AnneeDebut
    public void Add_AnneesInvalides_LeveValidationException(int anneeDebut, int? anneeFin)
    {
        var mockRepo = new Mock<IRepository<Serie>>();
        var service = new SerieService(mockRepo.Object);
        var serie = new Serie { Titre = "Test", AnneeDebut = anneeDebut, AnneeFin = anneeFin };

        Assert.Throws<ValidationException>(() => service.Add(serie));
    }
}
