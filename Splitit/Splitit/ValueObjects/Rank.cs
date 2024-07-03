namespace Splitit.Splitit.ValueObjects
{
    public class Rank
    {
        public int Value { get; private set; }

        public Rank(int value)
        {
            if (value < 1 || value > 300)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Rank must be between 1 and 300.");
            }

            Value = value;
        }

        public static implicit operator int(Rank rank)
        {
            return rank.Value;
        }

        public static implicit operator Rank(int value)
        {
            return new Rank(value);
        }
    }
}

