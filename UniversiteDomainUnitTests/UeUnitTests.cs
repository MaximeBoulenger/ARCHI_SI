using System.Linq.Expressions;
using UniversiteDomain.Entities;
using Moq;
using UniversiteDomain.DataAdapters;
using UniversiteDomain.UseCases.UeUseCases.Create;

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
        long id = 1;
        string numeroUe = "UE 3";
        string intitule = "Informatique";
        
        // On crée l'UE qui doit être ajoutée à la base
        Ue ueSansId = new Ue { NumeroUe = numeroUe, Intitule = intitule };
        
        // On crée le Mock du repository
        // On initialise une fausse datasource qui va simuler un UeRepository
        var mock = new Mock<IUeRepository>();
        
        // La réponse à l'appel FindByCondition est donc une liste vide
        var reponseFindByCondition = new List<Ue>();
        
        // On crée un bouchon dans le mock pour la fonction FindByCondition.
        // Quel que soit le paramètre de la fonction FindByCondition, on renvoie la liste vide.
        mock.Setup(repo => repo.FindByConditionAsync(It.IsAny<Expression<Func<Ue, bool>>>()))
            .ReturnsAsync(reponseFindByCondition);
        
        // Simulation de la fonction Create
        Ue ueCree = new Ue { Id = id, NumeroUe = numeroUe,Intitule = intitule };
        mock.Setup(repoUe => repoUe.CreateAsync(ueSansId)).ReturnsAsync(ueCree);
        
        // On crée le bouchon (une fause UeRepository) prêt à être utilisé
        var fauxUeRepository = mock.Object;
        
        // Création du use case en injectant notre faux repository
        CreateUeUseCase useCase = new CreateUeUseCase(fauxUeRepository);
        
        // Appel du use case
        var ueTeste = await useCase.ExecuteAsync(ueSansId);
        
        // Vérification du résultat
        Assert.That(ueTeste.Id, Is.EqualTo(ueCree.Id));
        Assert.That(ueTeste.NumeroUe, Is.EqualTo(ueCree.NumeroUe));
        Assert.That(ueTeste.Intitule, Is.EqualTo(ueCree.Intitule));
    }
}