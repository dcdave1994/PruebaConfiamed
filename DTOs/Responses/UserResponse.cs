namespace PruebaConfiamed.DTOs.Responses
{
    public class UserResponse
    {
        public int Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public int PendingItems { get; set; }

        public int CompletedItems { get; set; }

        public int HighPriorityItems { get; set; }
    }
}