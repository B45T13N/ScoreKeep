namespace ScoreKeep.Business.Interfaces;

public interface IVolunteerService
{
    Task<bool> AddVolunteerAsync(Volunteer volunteer, String password);
}