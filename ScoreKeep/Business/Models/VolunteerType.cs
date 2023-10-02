namespace ScoreKeep.Business.Models;
public class VolunteerType
{
    public int Id { get; set; }
    public String Label { get; set; }

    public override string ToString()
    {
        return $"{Label}";
    }
}

