namespace PackIT.Domain.Orders.ValueObjects
{
    public record ItemVO
    {
        private ItemVO()
        {
            
        }
        public ItemVO(string name, string code, string typeName, string typeCode)
        {
            Name = name;
            Code = code;
            TypeName = typeName;
            TypeCode = typeCode;
        }

        public  string Name { get; set; }
        public  string Code { get; set; }
        public  string TypeName { get; set; }
        public  string TypeCode { get; set; }
    }
}
