using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ItemManagment.Models.DataTransferModels
{
    public class MeasureDto
    {
        public int? Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public int Type { get; set; }
        public int ShopId { get; set; }
    }
}
