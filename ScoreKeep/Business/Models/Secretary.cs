using ScoreKeep.Business.Models.Interface;

namespace ScoreKeep.Business.Models;

public class Secretary : Person
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}