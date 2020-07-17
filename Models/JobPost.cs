namespace TalentHunt.Models
{
    public class JobPost
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int VacancyId { get; set; }
        public Vacancy Vacancy { get; set; }
    }
}