using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ItemManagment.Models.DataTransferModels
{
    public class SupplierDto
    {
        public int Id { get; set; }
        public string TaxNumber { get; set; }
        public string Name { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Description { get; set; }
        public int ShopId { get; set; }
        [JsonIgnore]
        public bool IsDeleted { get; set; }
        [JsonIgnore]
        public DateTime DeletedAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
