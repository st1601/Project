namespace API.DTOs.Idea.Count
{
    public class IdeaCount
    {
        public IdeaCount(Data.Entities.Idea idea, int count)
        {
            this.idea = idea;
            this.count = count;
        }

        public Data.Entities.Idea idea { get; set; }

        public int count { get; set; }

        public static int compareIdeaCount(IdeaCount idea1, IdeaCount idea2)
        {
            if (idea1.count < idea2.count)
            {
                return 1;
            }
            else if (idea1.count > idea2.count)
            {
                return -1;
            }

            return 0;
        }
    }
}
