using ItemManagment.Helpers.Type;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ItemManagment.Models.DataTransferModels
{
    public class createItemDto
    {
        [JsonIgnore]
        public int Id { get; set; }
        public int InternalCode { get; set; }

        [Required]
        [MaxLength(18)]
        public required string BarCode { get; set; }
        [Required]
        public required string Name { get; set; }

        [Column(TypeName = "decimal(14,2)")]
        public decimal Price { get; set; }
        [Column(TypeName = "decimal(14,2)")]
        public decimal Cost { get; set; }
        [Column(TypeName = "decimal(14,2)")]
        public decimal WholePrice { get; set; }
        [Column(TypeName = "decimal(14,2)")]
        public decimal WholeQuantity { get; set; }

        [Column(TypeName = "decimal(14,2)")]
        public decimal? PackageQuantity { get; set; }

        public int MeasureId { get; set; }
        [JsonIgnore]
        public MeasureDto? Measure { get; set; }

        public int ItemGroupId { get; set; }
        [JsonIgnore]
        public ItemGroupDto? ItemGroup { get; set; }

        public int UnionBarcodeId { get; set; }
        [JsonIgnore]
        public UnionBarcodeDto? UnionBarcode { get; set; }
        [Required]
        public required int ShopId { get; set; }
        [Required]
        public required bool IsDeleted { get; set; } = false;
        public TaxType TaxType { get; set; } = TaxType.Vat;

        public Item ConvertToEntity()
        {
            return new Item
            {
                Name = this.Name,
                BarCode = this.BarCode,
                Cost = this.Cost,
                Price = this.Price,
                WholePrice = this.WholePrice,
                WholeQuantity = this.WholeQuantity,
                PackageQuantity = this.PackageQuantity,
                MeasureId = this.MeasureId,
                ItemGroupId = this.ItemGroupId,
                UnionBarcodeId = this.UnionBarcodeId,
                ShopId = this.ShopId,
                IsDeleted = this.IsDeleted,
                TaxType = this.TaxType
            };
        }
        public Item UpdateOnEntity(Item itemEntity)
        {
            itemEntity.Name = this.Name;
            itemEntity.BarCode = this.BarCode;
            itemEntity.Cost = this.Cost;
            itemEntity.Price = this.Price;
            itemEntity.WholePrice = this.WholePrice;
            itemEntity.WholeQuantity = this.WholeQuantity;
            itemEntity.PackageQuantity = this.PackageQuantity;
            //itemEntity.MeasureId = this.MeasureId;
            //itemEntity.ItemGroupId = this.ItemGroupId;
            //itemEntity.UnionBarcodeId = this.UnionBarcodeId;
            itemEntity.ShopId = this.ShopId;
            itemEntity.IsDeleted = this.IsDeleted;
            itemEntity.TaxType = this.TaxType;
            return itemEntity;
        }
    }

}
