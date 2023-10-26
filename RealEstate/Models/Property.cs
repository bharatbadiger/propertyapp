using RealEstate.Constants;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RealEstate.Models
{
    public class Property : IAuditable
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public virtual string? Title { get; set; }
        public virtual string? SubTitle { get; set; }
        //[Range(0, double.MaxValue, ErrorMessage = "Price must be a non-negative value.")]
        public virtual string? Price { get; set; }
        //[Range(1, int.MaxValue, ErrorMessage = "Number of rooms must be a positive integer.")]
        public virtual string? NumberOfRooms { get; set; }
        //[Range(1, int.MaxValue, ErrorMessage = "BHK must be a positive integer.")]
        public virtual string? BHK { get; set; }
        public virtual string? Location { get; set; }
        public virtual string? City { get; set; }
        public virtual string? MainImage { get; set; }
        public virtual List<PropertyImage> Images { get; set; }
        public virtual PropertyType Type { get; set; }
        //[Range(0, double.MaxValue, ErrorMessage = "Area must be a non-negative value.")]
        public virtual string? Area { get; set; }
        public virtual AreaUnit AreaUnit { get; set; }
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public virtual string? Description { get; set; }
        public virtual DateTimeOffset CreatedDate { get; set; }
        public virtual DateTimeOffset UpdatedDate { get; set; }
    }
}
