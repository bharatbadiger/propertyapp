using RealEstate.Constants;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RealEstate.Models
{
    public class Property : IAuditable
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public virtual string Title { get; set; }
        //[Range(0, double.MaxValue, ErrorMessage = "Price must be a non-negative value.")]
        public virtual decimal Price { get; set; }
        //[Range(1, int.MaxValue, ErrorMessage = "Number of rooms must be a positive integer.")]
        public virtual decimal NumberOfRooms { get; set; }
        //[Range(1, int.MaxValue, ErrorMessage = "BHK must be a positive integer.")]
        public virtual decimal BHK { get; set; }
        public virtual string Location { get; set; }
        public virtual string City { get; set; }
        public virtual List<PropertyImage> Images { get; set; }
        public virtual PropertyType Type { get; set; }
        //[Range(0, double.MaxValue, ErrorMessage = "Area must be a non-negative value.")]
        public virtual double Area { get; set; }
        public virtual AreaUnit AreaUnit { get; set; }
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public virtual string Description { get; set; }
        public virtual DateTimeOffset CreatedDate { get; set; }
        public virtual DateTimeOffset UpdatedDate { get; set; }
    }
}
