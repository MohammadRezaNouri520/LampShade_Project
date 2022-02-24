using _0_Framework.Application;
using System.Collections.Generic;

namespace AccountManagement.Application.Contracts.Account
{
    public interface IAccountApplication
    {
        OperationResult Register(RegisterAccount command);
        OperationResult Edit(EditAccount command);
        EditAccount GetDetails(long id);
        List<AccountViewModel> Search(AccountSearchModel searchModel);
        OperationResult ChangePassword(ChangePassword command);
        OperationResult Remove(long id);
        OperationResult Restore(long id);

        OperationResult LogIn(Login command);
        void LogOut();
    }
}
