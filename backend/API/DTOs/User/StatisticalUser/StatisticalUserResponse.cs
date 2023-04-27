namespace API.DTOs.User.StatisticalUser
{
    public class StatisticalUserResponse
    {
        public StatisticalUserResponse(int totalEmployees) {
            this.totalEmployees = totalEmployees;
        }
        public int totalEmployees { get; set; }
    }
}
