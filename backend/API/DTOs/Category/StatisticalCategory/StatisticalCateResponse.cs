namespace API.DTOs.Category.StatisticalCategory
{
    public class StatisticalCateResponse
    {
        public StatisticalCateResponse(int totalCate)
        {
            this.totalCate = totalCate;
        }

        public int totalCate { get; set; }
    }
}
