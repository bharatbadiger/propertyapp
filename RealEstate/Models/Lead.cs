﻿using RealEstate.Constants;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RealEstate.Models
{
    public class Lead : IAuditable
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public virtual LeadPropertyType? LeadPropertyType { get; set; }
        public virtual PropertyType? PropertyType { get; set; }
        public virtual string MobileNo { get; set; }
        public virtual string FullName { get; set; }
        public virtual string Status { get; set; } // Open or Closed
        public virtual List<LeadCommentModel> LeadCommentModel { get; set; }
        // Reference to the User who created the lead
        [ForeignKey("CreatedById")]
        public virtual User? CreatedBy { get; set; }
        public int CreatedById { get; set; }
        public int? PropertyId { get; set; }
        public virtual DateTimeOffset CreatedDate { get; set; }
        public virtual DateTimeOffset UpdatedDate { get; set; }
    }
}
