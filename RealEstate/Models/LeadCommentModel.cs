using RealEstate.Constants;

namespace RealEstate.Models
{
    public class LeadCommentModel
    {
        public Guid Id { get; set; }
        public virtual string? Comment { get; set; }
        public virtual UserType UserType { get; set; }
        public virtual DateTimeOffset? TimeStamp { get; set; }
    }
}