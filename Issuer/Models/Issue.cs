using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Issuer.Models
{
    public class Issue
    {
        public string Id { get; set; }
        
        [Required]
        [DisplayName("Headline:")]
        public string Headline { get; set; }
        
        [DisplayName("Issue description:")]
        public string Description { get; set; }
        
        [DisplayName("Created at:")]
        public DateTime CreatedAt { get; set; }

        public bool Completed { get; set; }

        public int GetNumericId()
        {
            return int.Parse(Id.Substring(Id.IndexOf("-", System.StringComparison.Ordinal) + 1));
        }
    }
}