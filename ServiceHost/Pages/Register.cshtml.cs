using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class RegisterModel : PageModel
    {
        [TempData]
        public string Message { get; set; }

        [BindProperty]
        public RegisterAccount NewAccount { get; set; }

        private readonly IAccountApplication _accountApplication;

        public RegisterModel(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }

        public void OnGet()
        {
            NewAccount = new RegisterAccount();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = _accountApplication.Register(NewAccount);

            if (!result.IsSucceeded)
            {
                Message = result.Message;
                return Page();
            }
            return RedirectToPage("/Login");
        }
    }
}