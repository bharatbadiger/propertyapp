using RealEstate.Constants;

namespace RealEstate.Models
{
    public class LeadCommentModel
    {
        public virtual string? Comment { get; set; }
        public virtual UserType UserType { get; set; }
        public virtual string? TimeStamp { get; set; }
    }
}