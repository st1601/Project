namespace API.DTOs.Department
{
    public class StatisticalDepartmentResponse
    {
        public StatisticalDepartmentResponse()
        {
            totalDepartments = 0;
            details = new List<StatisticalDepartmentItem>();
        }

        public int totalDepartments { get; set; }

        public List<StatisticalDepartmentItem> details { get; set; }
    }
}
