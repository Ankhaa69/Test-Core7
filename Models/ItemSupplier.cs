using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ItemManagment.Models
{
        public class ItemSupplier
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }
            public int ItemId { get; set; }
            public Item Item { get; set; }
            public int SupplierId { get; set; }
            public Supplier Supplier { get; set; }
        }
}
