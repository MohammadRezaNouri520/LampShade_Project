using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace ServiceHost.Areas.Administration.Pages.Accounts.Role
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public EditRole Command { get; set; }

        public List<SelectListItem> Permissions = new List<SelectListItem>();

        private readonly IRoleApplication _roleApplication;
        private readonly IEnumerable<IPermissionExposer> _exposers;


        public EditModel(IRoleApplication roleApplication, IEnumerable<IPermissionExposer> exposers)
        {
            _roleApplication = roleApplication;
            _exposers = exposers;
        }

        public void OnGet(long id)
        {
            Command = _roleApplication.GetDetails(id);

            //////////
            foreach (var exposer in _exposers)
            {
                var exposedPermissions = exposer.Expose();
                foreach (var dictionary in exposedPermissions)
                {
                    var groupName = dictionary.Key;

                    foreach (var permission in dictionary.Value)
                    {
                        var item = new SelectListItem
                        {
                            Group = new SelectListGroup
                            {
                                Name = groupName
                            },
                            Text = permission.Name,
                            Value = permission.Code.ToString(),
                            Selected = Command.Permissions.Any(code => code == permission.Code)
                        };
                        Permissions.Add(item);
                    }
                }
            }
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            var result = _roleApplication.Edit(Command);
            return RedirectToPage("./Index");
        }
    }
}
