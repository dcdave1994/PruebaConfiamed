using System.ComponentModel.DataAnnotations;

namespace PruebaConfiamed.Entities
{
    public class AppUser
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Username { get; set; } = string.Empty;
    }
}