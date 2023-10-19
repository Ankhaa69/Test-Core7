using ItemManagment.Models;
using System.Linq.Expressions;

namespace ItemManagment.Helpers.Specifications
{
    public class ItemSpecification : ISpecification<Item>
    {
        private readonly Dictionary<SearchField, string> _searchFieldParams;

        public ItemSpecification(Dictionary<SearchField, string> searchFieldParams)
        {
            _searchFieldParams = searchFieldParams ?? new Dictionary<SearchField, string>();
        }

        public Expression<Func<Item, bool>> Criteria
        {
            get
            {
                return item =>
                    (_searchFieldParams.ContainsKey(SearchField.Name) && item.Name != null && item.Name.Contains(_searchFieldParams[SearchField.Name])) ||
                    (_searchFieldParams.ContainsKey(SearchField.BarCode) && item.BarCode != null && item.BarCode.Contains(_searchFieldParams[SearchField.BarCode])) ||
                    (_searchFieldParams.ContainsKey(SearchField.InternalCode) && item.InternalCode.ToString().Contains(_searchFieldParams[SearchField.InternalCode])) ||
                    (_searchFieldParams.ContainsKey(SearchField.ShopId) && item.ShopId.ToString().Equals(_searchFieldParams[SearchField.ShopId]));
            }
        }

        public enum SearchField
        {
            Name,
            BarCode,
            InternalCode,
            ShopId
        }
    }

}
