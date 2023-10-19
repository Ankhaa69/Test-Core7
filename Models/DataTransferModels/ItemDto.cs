using ItemManagment.Helpers.Type;
using ItemManagment.Models.DataTransferModels;
using System.Text.Json.Serialization;

namespace ItemManagment.Models.DataTransferModels
{
    public class ItemDto
    {
        //[JsonIgnore]
        public int Id { get; set; }
        public required string Name { get; set; } = string.Empty;
        public required string BarCode { get; set; } = string.Empty;
        public decimal Price { get; set; } = decimal.Zero;
        public decimal Cost { get; set; } = decimal.Zero;
        public decimal WholePrice { get; set; } = decimal.Zero;
        public decimal WholeQuantity { get; set; } = decimal.Zero;
        public int MeasureId { get; set; } = 1;
        public MeasureDto? Measure { get; set; }
        public int? ItemGroupId { get; set; }
        public ItemGroupDto? ItemGroup { get; set; }
        public int? UnionBarcodeId { get; set; }
        public UnionBarcodeDto? UnionBarcode { get; set; }
        public int ShopId { get; set; }
        public TaxType TaxType { get; set; } = TaxType.Vat;
    }





    
    //public ItemGroupDto? ItemGroup { get; set; }
    //public UnionBarCodeDto? UnionBarcode { get; set; }
}
