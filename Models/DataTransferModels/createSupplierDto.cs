using System.Text.Json.Serialization;

namespace ItemManagment.Models.DataTransferModels
{
    public class createSupplierDto
    {
        [JsonIgnore]
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
        [JsonIgnore]
        public DateTime CreatedAt { get; set; }
        public Supplier CreateEntity()
        {
            return new Supplier
            {
                TaxNumber = this.TaxNumber,
                Name = this.Name,
                Address = this.Address,
                Phone = this.Phone,
                Email = this.Email,
                Description = this.Description,
                ShopId = this.ShopId
            };
        }
        public Supplier UpdateToEntity(Supplier supplier)
        {
            supplier.TaxNumber = this.TaxNumber;
            supplier.Name = this.Name;
            supplier.Address = this.Address;
            supplier.Phone = this.Phone;
            supplier.Email = this.Email;
            supplier.Description = this.Description;
            supplier.ShopId = this.ShopId;
            return supplier;
        }
    }
}
