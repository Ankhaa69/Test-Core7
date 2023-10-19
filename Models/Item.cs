using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ItemManagment.Helpers.Type;
using Microsoft.EntityFrameworkCore;
using System;
using System.Runtime.CompilerServices;

namespace ItemManagment.Models
{
    [Table("Items")]
    [Index(nameof(BarCode), nameof(InternalCode), nameof(ShopId))]
    public class Item : ISoftDeleteEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int InternalCode { get; set; }
        
        public required string BarCode { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Cost { get; set; }
        public decimal WholePrice { get; set; }
        public decimal WholeQuantity { get; set; }
        public decimal? PackageQuantity { get; set; }
        /////////////////////////////
        public int? MeasureId { get; set; }
        public Measure? Measure { get; set; }
       
        public int? ItemGroupId { get; set; }
        public ItemGroup? ItemGroup { get; set; }

        public int? UnionBarcodeId { get; set; }
        public UnionBarcode? UnionBarcode { get; set; }

        public ICollection<ItemSupplier> ItemSuppliers { get; set; }

        public int ShopId { get; set; }
        /////////////////////////////
        public TaxType TaxType { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
