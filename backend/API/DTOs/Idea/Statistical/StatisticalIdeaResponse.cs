namespace API.DTOs.Idea.Statistical
{
    public class StatisticalIdeaResponse
    {
        public StatisticalIdeaResponse()
        {
            totalIdeas = 0;
            details = new List<StatisticalIdeaItem>();
        }

        public int totalIdeas { set; get; }

        public List<StatisticalIdeaItem> details { set; get; }
    }
}
