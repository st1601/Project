using Common.Enums;

namespace API.DTOs.Department
{
    public class StatisticalDepartmentItem
    {
        public StatisticalDepartmentItem(string departmentName, int total)
        {
            this.departmentName = departmentName;
            this.total = total;
        }

        public string departmentName { get; set; }

        public int total { get; set; }
    }
}
