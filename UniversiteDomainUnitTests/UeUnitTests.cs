using UniversiteDomain.Entities;
using Moq;
using UniversiteDomain.DataAdapters;

namespace UniversiteDomainUnitTests;

public class UeUnitTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task CreateUeUseCase()
    {
        long Id = 1;
        string NumeroUe = "UE 3";
        string Intitule = "Informatique";
        // Manque la liste des parcours jcrois
        
        // On crée l'UE qui doit être ajoutée à la base
        Ue ueSansId = new Ue { NumeroUe = NumeroUe, Intitule = Intitule };
        
        // On crée le Mock du repository
        // On initialise une fausse datasource qui va simuler un UeRepository
        var mock = new Mock<IUeRepository>();
    }
}