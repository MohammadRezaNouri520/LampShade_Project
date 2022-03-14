using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.Role;
using AccountManagement.Infrastructure.Configuration.Permissions;
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


        [NeedsPermissionAttribute(AccountPermissions.ListAccounts)]
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

        [NeedsPermission(AccountPermissions.CreateAccount)]
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

        [NeedsPermission(AccountPermissions.EditAccount)]
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

        [NeedsPermission(AccountPermissions.ChangePassword)]
        public IActionResult OnPostChangePassword(ChangePassword command)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("./Index");
            }

            var result = _accountApplication.ChangePassword(command);
            return new JsonResult(result);
        }

        [NeedsPermission(AccountPermissions.RemoveAccount)]
        public IActionResult OnGetRemove(long id)
        {
            var result = _accountApplication.Remove(id);
            return RedirectToPage("./Index");
        }

        [NeedsPermission(AccountPermissions.RestoreAccount)]
        public IActionResult OnGetRestore(long id)
        {
            var result = _accountApplication.Restore(id);
            return RedirectToPage("./Index");
        }
    }
}
