namespace PackIt.Shared.Abstractions.Shared
{
    public abstract class ValueObject: IEquatable<ValueObject>
    {
        protected ValueObject()
        {
            
        }
        public abstract IEnumerable<object> GetAtomicValues();

        public bool Equals(ValueObject? other)
        {
            return other is not null && GetType() == other.GetType() && ValuesAreEqual(other);
        }
        public override bool Equals(object? obj)
        {
            return obj is ValueObject other && GetType() == other.GetType() && ValuesAreEqual(other);
        }

        public override int GetHashCode()
        {

            return this.GetAtomicValues().Aggregate(default(int), (hash, value) => HashCode.Combine(hash, value?.GetHashCode() ?? 0));
        }

        public static bool operator ==(ValueObject? left, ValueObject? right)
        {
            return left?.Equals(right) ?? right is null;
        }

        public static bool operator != (ValueObject? left, ValueObject? right)
        {
            return (left == right);
        }

        private bool ValuesAreEqual(ValueObject other)
        {
            return GetAtomicValues().SequenceEqual(other.GetAtomicValues());
        }


    }
}
