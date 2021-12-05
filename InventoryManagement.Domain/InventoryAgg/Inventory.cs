using _0_Framework.Domain;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManagement.Domain.InventoryAgg
{
    public class Inventory : EntityBase
    {
        public long ProductId { get; private set; }
        public double UnitPrice { get; private set; }
        public bool IsInStock { get; private set; }

        public List<InventoryOperation> Operations { get; private set; }

        // Constructor:
        public Inventory()
        {

        }

        public Inventory(long productId, double unitPrice)
        {
            ProductId = productId;
            UnitPrice = unitPrice;
            IsInStock = false;
        }

        public void Edit(long productId, double unitPrice)
        {
            ProductId = productId;
            UnitPrice = unitPrice;
        }

        public void Increase(long operatorId, long count, string description)
        {
            var currentCount = CalculateCurrentCount() + count;
            var operation = new InventoryOperation(true, operatorId, count, 0, description, currentCount, Id);
            Operations.Add(operation);
            IsInStock = currentCount > 0;
        }

        public void Reduce(long operatorId, long count, string description, long orderId)
        {
            var currentCount = CalculateCurrentCount() - count;
            var operation = new InventoryOperation(false, operatorId, count, orderId, description, currentCount, Id);
            Operations.Add(operation);
            IsInStock = currentCount > 0;
        }


        public long CalculateCurrentCount()
        {
            var imports = Operations.Where(x => x.Operation).Sum(x => x.Count);
            var exports = Operations.Where(x => !x.Operation).Sum(x => x.Count);

            return imports - exports;
        }
    }
}
