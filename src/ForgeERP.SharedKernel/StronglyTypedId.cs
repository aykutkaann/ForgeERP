
namespace ForgeERP.SharedKernel
{
    public abstract class StronglyTypedId : ValueObject
    {
        public Guid Value { get; }

        protected StronglyTypedId(Guid value)
        {
            if (value == Guid.Empty)
                throw new ArgumentException("Id cannot be empty.", nameof(value));

            Value = value;
        }

        protected StronglyTypedId() { }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value.ToString();
    }
}
