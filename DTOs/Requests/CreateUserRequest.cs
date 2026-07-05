using System.ComponentModel.DataAnnotations;

namespace PruebaConfiamed.DTOs.Requests
{
    public class CreateUserRequest
    {
        [Required]
        [MaxLength(100)]
        public string Username { get; set; } = string.Empty;
    }
}