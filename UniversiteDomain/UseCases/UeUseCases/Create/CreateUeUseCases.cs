using UniversiteDomain.DataAdapters;
using UniversiteDomain.Entities;
using UniversiteDomain.Exceptions.UeExceptions;

namespace UniversiteDomain.UseCases.UeUseCases.Create;

public class CreateUeUseCase(IUeRepository ueRepository)
{
    public async Task<Ue> ExecuteAsync(string numeroUe,  string intitule)
    {
        var ue = new Ue { NumeroUe = numeroUe, Intitule = intitule };
        return await ExecuteAsync(ue);
    }

    public async Task<Ue> ExecuteAsync(Ue ue)
    {
        await CheckBusinessRules(ue);
        Ue u = await ueRepository.CreateAsync(ue);
        ueRepository.SaveChangesAsync().Wait();
        return u;
    }

    private async Task CheckBusinessRules(Ue ue)
    {
        // On check si une valeur est null ou pas
        ArgumentNullException.ThrowIfNull(ue);
        ArgumentNullException.ThrowIfNull(ue.NumeroUe);
        ArgumentNullException.ThrowIfNull(ue.Intitule);
        
        // On cherche une UE avec le même nom
        List<Ue> existe = await ueRepository.FindByConditionAsync(u => u.NumeroUe.Equals(ue.NumeroUe));

        if (existe is { Count: > 0 })
        {
            throw new DuplicateUeException(ue.NumeroUe + " - ce numéro d'UE est déjà affecté à une UE");
        }
        
        // On vérifie que l'intitulé de l'UE fait au moins 3 caractères
        if (ue.Intitule.Length < 3)
        {
            throw new InvalidIntituleException(ue.Intitule + " incorrect - L'intitulé doit contenir plus de 3 caractères");
        }
    }
}