using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ItemManagment.Models.DataTransferModels
{
    public class UnionBarcodeDto
    {
        [JsonIgnore]
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }
        [Required]
        public int ShopId { get; set; }
        public bool IsMergedItems { get; set; }
    }
}
