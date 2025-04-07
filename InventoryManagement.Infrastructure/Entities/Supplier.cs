using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Infrastructure.Entities
{
    public class Supplier
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}