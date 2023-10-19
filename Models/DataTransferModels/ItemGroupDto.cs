using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ItemManagment.Models.DataTransferModels
{
    public class ItemGroupDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? Code { get; set; }
        public int Type { get; set; }
        public int HeaderGroupId { get; set; }
        public int ShopId { get; set; }
    }
}
