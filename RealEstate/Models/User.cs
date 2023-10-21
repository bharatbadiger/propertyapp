using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using RealEstate.Constants;

namespace RealEstate.Models
{
    public class User : IAuditable
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public virtual string FullName { get; set; }
        public virtual string MobileNo { get; set; }
        public virtual string Email { get; set; }
        public virtual string Status { get; set; }
        public virtual string AadharFront { get; set; }
        public virtual string AadharBack { get; set; }
        public virtual string PAN { get; set; }
        public virtual UserType UserType { get; set; }
        public virtual DateTimeOffset CreatedDate { get; set; }
        public virtual DateTimeOffset UpdatedDate { get; set; }
    }
}
