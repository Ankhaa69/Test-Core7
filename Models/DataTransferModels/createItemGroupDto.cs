using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ItemManagment.Models.DataTransferModels
{
    public class createItemGroupDto
    {
        [JsonIgnore]
        public int Id { get; set; }
        [Required]
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? Code { get; set; }
        public int Type { get; set; }
        public int ChildGroudId { get; set; }
        [Required]
        public required int ShopId { get; set; }

        public ItemGroup ConvertToModel()
        {
            return new ItemGroup
            {
                Name = this.Name,
                Description = this.Description,
                Code = this.Code,
                Type = this.Type,
                ChildGroupId = this.ChildGroudId,
                ShopId = this.ShopId
            };
        }
        public ItemGroup UpdateToEntity(ItemGroup itemGroup)
        {
            itemGroup.Name = this.Name;
            itemGroup.Description = this.Description;
            itemGroup.Code = this.Code;
            itemGroup.Type = this.Type;
            itemGroup.ChildGroupId = this.ChildGroudId;
            itemGroup.ShopId = this.ShopId;
            return itemGroup;
        }
    }
}
