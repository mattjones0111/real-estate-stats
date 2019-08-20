namespace ConsoleApp
{
    public class Counted<T>
    {
        public T Object { get; }
        public int Count { get; }

        public Counted(T @object, int count)
        {
            Object = @object;
            Count = count;
        }

        public override string ToString()
        {
            return $"{Object} - {Count}";
        }
    }
}
