using System.Text.Json.Serialization;

namespace ItemManagment.Models.DataTransferModels
{
    public class createMeasureDto
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int Type { get; set; }
        public int ShopId { get; set; }
        [JsonIgnore]
        public bool IsDeleted { get; set; } = false;
        [JsonIgnore]
        public DateTime? DeletedAt { get; set; }
        public Measure CreateEntity()
        {
            return new Measure
            {
                Name = this.Name,
                Description = this.Description,
                Type = this.Type,
                ShopId = this.ShopId
            };
        }
        public Measure UpdateToEntity(Measure measure)
        {
            measure.Name = this.Name;
            measure.Description = this.Description;
            measure.Type = this.Type;
            measure.ShopId = this.ShopId;
            return measure;
        }
    }
}
