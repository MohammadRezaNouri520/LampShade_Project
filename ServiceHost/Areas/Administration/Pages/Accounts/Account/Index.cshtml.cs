using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace ServiceHost.Areas.Administration.Pages.Accounts.Account
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }

        public List<AccountViewModel> Accounts { get; set; }
        public AccountSearchModel SearchModel { get; set; }
        public SelectList Roles { get; set; }

        private readonly IAccountApplication _accountApplication;
        private readonly IRoleApplication _roleApplication;

        public IndexModel(IAccountApplication accountApplication, IRoleApplication roleApplication)
        {
            _accountApplication = accountApplication;
            _roleApplication = roleApplication;
        }

        public void OnGet(AccountSearchModel searchModel)
        {
            Accounts = _accountApplication.Search(searchModel);
            Roles = new SelectList(_roleApplication.GetRolesList(), "Id", "Title");
        }


        public IActionResult OnGetCreate()
        {
            var command = new RegisterAccount
            {
                Roles = _roleApplication.GetRolesList()
            };
            return Partial("Create", command);
        }

        public IActionResult OnPostCreate(RegisterAccount command)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("./Index");
            }
            var result = _accountApplication.Register(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var command = _accountApplication.GetDetails(id);
            command.Roles = _roleApplication.GetRolesList();
            return Partial("Edit", command);
        }

        public IActionResult OnPostEdit(EditAccount command)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("./Index");
            }

            var result = _accountApplication.Edit(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetChangePassword(long id)
        {
            var command = new ChangePassword
            {
                Id = id
            };
            return Partial("ChangePassword", command);
        }

        public IActionResult OnPostChangePassword(ChangePassword command)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("./Index");
            }

            var result = _accountApplication.ChangePassword(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetRemove(long id)
        {
            var result = _accountApplication.Remove(id);
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetRestore(long id)
        {
            var result = _accountApplication.Restore(id);
            return RedirectToPage("./Index");
        }
    }
}
