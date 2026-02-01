using PackIT.Domain.Locations;
using PackIT.Shared.DtoTree;

namespace PackIt.Application.Locations.DTO
{
    public class LocationDto : ITreeNode<LocationDto, long>
    {
        public long Id { get; set; }
        public  string Name { get; set; }
        public string Code { get; set; }
        public string? Description { get; set; }
        public LocationTypeEnum Type { get; set; }
        public LocationDto? Parent { get; set; }
        public long? ParentId { get; set; }

        public List<LocationDto> Children { get; set; } = new();


        /// <summary>
        /// For conversion from flat Location list to LocationTree
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void AddChild(LocationDto entity)
        {
            Children.Add(entity);
        }
    }
}
