using _0_Framework.Application;
using _0_Framework.Infrastructure;
using InventoryManagement.Application.Contracts.Inventory;
using InventoryManagement.Domain.InventoryAgg;
using ShopManagement.Infrastructure.EFCore;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManagement.Infrastructure.EFCore.Repository
{
    public class InventoryRepository : RepositoryBase<long, Inventory>, IInventoryRepository
    {
        private readonly InventoryContext _context;
        private readonly ShopContext _shopContext;

        public InventoryRepository(InventoryContext context, ShopContext shopContext) : base(context)
        {
            _context = context;
            _shopContext = shopContext;
        }

        public Inventory GetBy(long productId)
        {
            return _context.Inventory.FirstOrDefault(x => x.ProductId == productId);
        }

        public EditInventory GetDetails(long id)
        {
            return _context.Inventory.Select(x => new EditInventory
            {
                Id=x.Id,
                ProductId=x.ProductId,
                UnitPrice=x.UnitPrice
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<InventoryOperationViewModel> GetInventoryOperations(long id)
        {
            var inventory = _context.Inventory.FirstOrDefault(x => x.Id == id);
            return inventory.Operations.Select(x => new InventoryOperationViewModel 
            {
                Id=x.Id,
                Operation=x.Operation,
                OperatorId=x.OperatorId,
                Operator="مدیر سیستم",
                OperationDate=x.OperationDate.ToFarsi(),
                Count=x.Count,
                CurrentCount=x.CurrentCount,
                Description=x.Description,
                OrderId=x.OrderId
            }).OrderByDescending(x => x.Id).ToList();
        }

        public List<InventoryViewModel> Search(InventorySearchModel searchModel)
        {
            var products = _shopContext.Products.Select(p => new {p.Id, p.Name }).ToList();
            var query = _context.Inventory.Select(x => new InventoryViewModel 
            {
                Id=x.Id,
                ProductId=x.ProductId,
                UnitPrice=x.UnitPrice,
                IsInStock=x.IsInStock,
                CurrentCount=x.CalculateCurrentCount(),
                CreationDate=x.CreationDate.ToFarsi()
            });

            if (searchModel.ProductId > 0)
                query = query.Where(x => x.ProductId == searchModel.ProductId);

            if (searchModel.IsInStock)
                query = query.Where(x => !x.IsInStock);

            var inventories = query.OrderByDescending(x => x.Id).ToList();

            //foreach(var inventory in inventories)
            //{
            //    inventory.Product = products.FirstOrDefault(p => p.Id == inventory.ProductId)?.Name;
            //}

            inventories.ForEach(inventory => { inventory.Product = products.FirstOrDefault(p => p.Id == inventory.ProductId)?.Name; });

            return inventories;
        }
    }
}
