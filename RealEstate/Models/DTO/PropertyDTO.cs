using RealEstate.Constants;

namespace RealEstate.Models.DTO
{
    public class PropertyDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public decimal Price { get; set; }
        public decimal NumberOfRooms { get; set; }
        public decimal BHK { get; set; }
        public string Location { get; set; }
        public string City { get; set; }
        public List<PropertyImageDTO> Images { get; set; }
        public PropertyType Type { get; set; }
        public double Area { get; set; }
        public AreaUnit AreaUnit { get; set; }
        public string Description { get; set; }
    }

}
