using ItemManagment.Helpers;
using static ItemManagment.Helpers.Specifications.ItemSpecification;

namespace ItemManagment.Models.DataTransferModels
{
    public class ItemSpecificationDto
    {
        public required Dictionary<SearchField, string> SearchFieldParams { get; set; }
        public required PaginationParamDto paramInfo { get; set; }
    }
}
