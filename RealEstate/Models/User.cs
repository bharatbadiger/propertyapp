using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using RealEstate.Constants;
using Newtonsoft.Json;

namespace RealEstate.Models
{
    public class User : IAuditable
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public virtual string? FullName { get; set; }
        public virtual string MobileNo { get; set; }
        public virtual string Email { get; set; }
        public virtual string Status { get; set; }
        public virtual string? AadharFront { get; set; }
        public virtual string? AadharBack { get; set; }
        public virtual string? PAN { get; set; }
        public virtual string? VPA { get; set; }
        public virtual UserType UserType { get; set; }
        public virtual string? Image { get; set; }
        public virtual string? ReferralCode { get; set; }
        public virtual bool? IsKycVerified { get; set; }
        [NotMapped]
        public ICollection<int> FavoritePropertyIds { get; set; }

        [Column("FavoritePropertyIds")]
        public string FavoritePropertyIdsJson
        {
            get => JsonConvert.SerializeObject(FavoritePropertyIds);
            set => FavoritePropertyIds = JsonConvert.DeserializeObject<ICollection<int>>(value);
        }
        public virtual DateTimeOffset CreatedDate { get; set; }
        public virtual DateTimeOffset UpdatedDate { get; set; }
    }
}
