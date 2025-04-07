using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Infrastructure.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}