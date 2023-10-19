namespace RealEstate.Models
{
    public class PropertyImage
    {
        public Guid Id { get; set; }
        public virtual string MediaType { get; set; }
        public virtual string Url { get; set; }
    }
}
