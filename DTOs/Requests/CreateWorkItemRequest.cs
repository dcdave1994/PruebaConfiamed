using System.ComponentModel.DataAnnotations;
using PruebaConfiamed.Common.Enums;

namespace PruebaConfiamed.DTOs.Requests
{
    public class CreateWorkItemRequest
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public Priority Priority { get; set; }
    }
}