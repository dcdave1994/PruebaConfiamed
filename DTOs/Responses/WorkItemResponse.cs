using PruebaConfiamed.Common.Enums;

namespace PruebaConfiamed.DTOs.Responses
{
    public class WorkItemResponse
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateTime DueDate { get; set; }

        public Priority Priority { get; set; }

        public WorkItemStatus Status { get; set; }

        public string AssignedUser { get; set; } = string.Empty;
    }
}