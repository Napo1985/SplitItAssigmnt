using System.ComponentModel.DataAnnotations;

namespace Splitit.App.Models
{
    public class ActorRequest
    {
        [StringLength(100)]
        public string? Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Details { get; set; }

        [StringLength(50)]
        public string Type { get; set; }

        [Required]
        public int Rank { get; set; }

        [StringLength(100)]
        public string Source { get; set; }
    }
}

