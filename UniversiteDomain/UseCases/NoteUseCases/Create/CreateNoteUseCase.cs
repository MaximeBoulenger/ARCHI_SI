using UniversiteDomain.DataAdapters;
using UniversiteDomain.Entities;

namespace UniversiteDomain.UseCases.NoteUseCases.Create;

public class CreateNoteUseCase(INoteRepository noteRepository)
{
    public async Task<Note> ExecuteAsync(float valeur)
    {
        var note = new Note { Valeur = valeur };
        return await ExecuteAsync(note);
    }

    public async Task<Note> ExecuteAsync(Note note)
    {
        await CheckBusinessRules(note);
        Note n = await noteRepository.CreateAsync(note);
        noteRepository.SaveChangesAsync().Wait();
        return n;
    }

    private async Task CheckBusinessRules(Note note)
    {
        // On check si une valeur est null ou pas
        ArgumentNullException.ThrowIfNull(note);
        ArgumentNullException.ThrowIfNull(note.Valeur);
        
        // On vérifie q'un étudiant n'a qu'une note au maximum par Ue
        
        // On vérifie que la note est comprise entre 0 et 20
        
        // On vérifie qu'un étudiant ne peut avoir une note que dans une Ue du parcours dans lequel il est inscrit
        
    }
}