namespace API.DTOs.Idea.Statistical
{
    public class StatisticalIdeaItem
    {

        public StatisticalIdeaItem(int eventId, string eventName, int totalIdeas)
        {
            this.eventId = eventId;
            this.eventName = eventName;
            this.totalIdeas = totalIdeas;
        }

        public int eventId {  get; set; }

        public string eventName { get; set; }

        public int totalIdeas { get; set; }
    }
}
