using UniversiteDomain.DataAdapters;
using UniversiteDomain.Entities;
using UniversiteDomain.Exceptions.ParcoursExceptions;


namespace UniversiteDomain.UseCases.ParcoursUseCases.Create;

public class CreateParcoursUseCase(IParcoursRepository parcoursRepository)
{
    public async Task<Parcours> ExecuteAsync(string NomParcours, int AnneeFormation, List<Etudiant> Inscrits,
        List<Ue> UesEnseignees)
    {
        var parcours = new Parcours
        {
            NomParcours = NomParcours, Inscrits = Inscrits, UesEnseignees = UesEnseignees,
            AnneeFormation = AnneeFormation
        };
        return await ExecuteAsync(parcours);
    }

    public async Task<Parcours> ExecuteAsync(Parcours parcours)
    {

        Parcours pc = await parcoursRepository.CreateAsync(parcours);
        parcoursRepository.SaveChangesAsync().Wait();
        return pc;
    }

    private async Task CheckBusinessRules(Parcours parcours)
    {
        ArgumentNullException.ThrowIfNull(parcours);
        ArgumentNullException.ThrowIfNull(parcours.NomParcours);
        ArgumentNullException.ThrowIfNull(parcoursRepository);

        // On recherche un parcours avec le même nom parcours
        List<Parcours> existe =
            await parcoursRepository.FindByConditionAsync(e => e.NomParcours.Equals(parcours.NomParcours));

        // Si un parcours avec le même nom parcours existe déjà, on lève une exception personnalisée
        if (existe is { Count: > 0 })
            throw new DuplicateNomParcoursException(parcours.NomParcours +
                                                    " - ce nom de parcours est déjà existant");

/*        // Vérification de l'année de formation
        if (!CheckEmail.IsValidEmail(etudiant.Email))
            throw new InvalidEmailException(etudiant.Email + " - Email mal formé");

        // On vérifie si l'email est déjà utilisé
        existe = await etudiantRepository.FindByConditionAsync(e => e.Email.Equals(etudiant.Email));
        // Une autre façon de tester la vacuité de la liste
        if (existe is { Count: > 0 })
            throw new DuplicateEmailException(etudiant.Email + " est déjà affecté à un étudiant");
        // Le métier définit que les nom doite contenir plus de 3 lettres
        if (etudiant.Nom.Length < 3)
            throw new InvalidNomEtudiantException(etudiant.Nom +
*/
    }
}