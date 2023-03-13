namespace HandAPI.Models
{
    public class Match
    {
        public int Id { get; set; }        
        public DateTime MatchDate { get; set; }        
        public string VisitorTeamName { get; set; }
        public string Categorie { get; set; }
        public int IdGymnase { get; set; }
        public int IdTimeKeeper { get; set; }        
        public int IdSecretaty { get; set; }        
        public int IdRoomManager { get; set; }        
        public int IdLocalTeam { get; set; }        
        public LocalTeam LocalTeam { get; set; }
    }
}
