using System.ComponentModel.DataAnnotations;
using PruebaConfiamed.Common.Enums;

namespace PruebaConfiamed.Entities
{
    public class WorkItem
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public Priority Priority { get; set; }

        [Required]
        public WorkItemStatus Status { get; set; }

        public int AppUserId { get; set; }

        public AppUser? AppUser { get; set; }
    }
}