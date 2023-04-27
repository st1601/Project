namespace Common.DataType
{
    public class DataList<T>
    {
        public DataList(IQueryable<T> source)
        {
            Items = source.ToList();
        }

        public List<T> Items { get; set; }
    }
}
