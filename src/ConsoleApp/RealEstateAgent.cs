namespace ConsoleApp
{
    public class RealEstateAgent
    {
        public long Id { get; }
        public string Name { get; }

        public RealEstateAgent(long id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }

        protected bool Equals(RealEstateAgent other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((RealEstateAgent) obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
