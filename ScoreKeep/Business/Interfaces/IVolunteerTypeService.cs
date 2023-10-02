namespace ScoreKeep.Business.Interfaces;

public interface IVolunteerTypeService
{
    Task<List<VolunteerType>> GetAllVolunteerTypesAsync();
}