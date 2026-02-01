using PackIt.Shared.Abstractions.Shared;

namespace PackIT.Domain.Orders.ValueObjects
{
    public class LocationVO : ValueObject
    {
        protected LocationVO():base()
        {
            
        }
        public LocationVO(string name, string code, string type)
        {
            Name = name;
            Code = code;
            Type = type;
        }
        public string Name { get; set; }
        public string Code { get; set; }

        public  string Type { get; set; }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Name;
            yield return Code;
            yield return Type;
        }

        public override string ToString()
        {
            return $"Name:{this.Name} Code:{this.Code} Type:{this.Type}";
        }
    }
}
