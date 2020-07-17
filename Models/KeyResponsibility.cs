namespace TalentHunt.Models
{
    public class KeyResponsibility
    {
        public int VacancyId { get; set; }
        public Vacancy Vacancy { get; set; }
        public int ResponsibilityId { get; set; }
        public Responsibility Responsibility { get; set; }
    }
}