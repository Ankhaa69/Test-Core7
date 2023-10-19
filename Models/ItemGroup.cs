using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ItemManagment.Models
{
    public class ItemGroup : ISoftDeleteEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? Code { get; set; }
        public int Type { get; set; }
        public int ChildGroupId { get; set; }
        [Required]
        public int ShopId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<Item>? Items { get; set; }
    }
}
