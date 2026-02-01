namespace PackIt.Persistance.EF.ItemTypes.ReadModels
{
    internal class ItemTypeReadModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int Version { get; set; }
    }
}
