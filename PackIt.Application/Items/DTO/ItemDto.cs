namespace PackIt.Application.Items.DTO
{
    public class ItemDto
    {
        public long Id { get; set; }
        public string  Name { get; set; }
        public string  Code { get; set; }
        public ItemTypeDto Type { get; set; }
    }
}
