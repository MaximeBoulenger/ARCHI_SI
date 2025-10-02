namespace UniversiteDomain.Entities;

public class Parcours
{
    public long Id { get; set; }
    public string NomParcours { get; set; } = String.Empty;
    public int AnneeFormation { get; set; }

    public override string ToString()
    {
        return $"{NomParcours} {AnneeFormation}";
    }
}