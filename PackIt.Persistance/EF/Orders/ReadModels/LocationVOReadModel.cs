namespace PackIt.Persistance.EF.Orders.ReadModels
{
    internal class LocationVoReadModel
    {
        public LocationVoReadModel(string name, string code, string type)
        {
            Name = name;
            Code = code;
            Type = type;
        }

        public required string Name { get; set; }
        public required string Code { get; set; }
        public required string Type { get; set; }

    }
}
