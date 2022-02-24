using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class LoginModel : PageModel
    {
        [TempData]
        public string LoginMessage { get; set; }

        [BindProperty]
        public Login LoginUser { get; set; }

        private readonly IAccountApplication _accountApplication;

        public LoginModel(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }

        public void OnGet()
        {
            LoginUser = new Login();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            var result = _accountApplication.LogIn(LoginUser);
            
            if(!result.IsSucceeded)
            {
                LoginMessage = result.Message;
                return Page();
            }
            return RedirectToPage("/Index");
                
        }

        public IActionResult OnGetLogOut()
        {
            _accountApplication.LogOut();
            return RedirectToPage("/Login");
        }
    }
}
