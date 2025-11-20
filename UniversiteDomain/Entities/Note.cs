namespace UniversiteDomain.Entities;

public class Note
{
    public float Valeur { get; set; }
    
    // Chaque note appartient à 1 étudiant
    public long EtudiantId { get; set; }
    public Etudiant? Etudiant { get; set; }


    // Chaque note appartient à 1 UE
    public long UeId { get; set; }
    public Ue? Ue { get; set; }
    
    public override string ToString()
    {
        return "Note " + Valeur;
    }
}