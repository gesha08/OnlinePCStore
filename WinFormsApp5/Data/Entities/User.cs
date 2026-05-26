using Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [Required]
        public Role Role { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
