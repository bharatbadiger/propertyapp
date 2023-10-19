using RealEstate.Constants;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RealEstate.Models
{
    public class Lead : IAuditable
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public virtual PropertyType PropertyType { get; set; }
        public virtual string PhoneNo { get; set; }
        public virtual string FullName { get; set; }
        public virtual string Status { get; set; } // Open or Closed
        public virtual string Comment { get; set; }
        public virtual DateTimeOffset CreatedDate { get; set; }
        public virtual DateTimeOffset UpdatedDate { get; set; }
    }
}
