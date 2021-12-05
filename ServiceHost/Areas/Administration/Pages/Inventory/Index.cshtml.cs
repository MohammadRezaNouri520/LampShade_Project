using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryManagement.Application.Contracts.Inventory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;

namespace ServiceHost.Areas.Administration.Pages.Inventory
{
    public class IndexModel : PageModel
    {
        public List<InventoryViewModel> Inventory { get; set; }
        public InventorySearchModel SearchModel { get; set; }
        public SelectList Products { get; set; }

        private readonly IInventoryApplication _inventoryApplication;
        private readonly IProductApplication _productApplication;

        public IndexModel(IInventoryApplication inventoryApplication, IProductApplication productApplication)
        {
            _inventoryApplication = inventoryApplication;
            _productApplication = productApplication;
        }


        public void OnGet(InventorySearchModel searchModel)
        {
            Inventory = _inventoryApplication.Search(searchModel);
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreateInventory
            {
                Products = _productApplication.GetProducts()
            };

            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(CreateInventory command)
        {
            var result = _inventoryApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var command = _inventoryApplication.GetDetails(id);
            command.Products = _productApplication.GetProducts();
            return Partial("./Edit", command);
        }

        public JsonResult OnPostEdit(EditInventory command)
        {
            var result = _inventoryApplication.Edit(command);
            return new JsonResult(result);
        }

        public PartialViewResult OnGetIncrease(long id)
        {
            var command = new IncreaseInventory { InventoryId = id };
            return Partial("./Increase", command);
        }

        public JsonResult OnPostIncrease(IncreaseInventory command)
        {
            var result = _inventoryApplication.Increase(command);
            return new JsonResult(result);
        }

        public PartialViewResult OnGetReduce(long id)
        {
            var command = new DecreaseInventory { InventoryId = id };
            return Partial("./Reduce", command);
        }

        public JsonResult OnPostReduce(DecreaseInventory command)
        {
            var result = _inventoryApplication.Reduce(command);
            return new JsonResult(result);
        }

        public PartialViewResult OnGetLog(long id)
        {
            var inventory = _inventoryApplication.GetInventoryOpertaions(id);
            return Partial("./OperationsLog", inventory);
        }
    }
}
