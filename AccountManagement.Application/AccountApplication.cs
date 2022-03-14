using _0_Framework.Application;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Domain.AccountAgg;
using AccountManagement.Domain.RoleAgg;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AccountManagement.Application
{
    public class AccountApplication : IAccountApplication
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IFileUploader _fileUploader;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAuthHelper _authHelper;
        private readonly IRoleRepository _roleRepository;

        public AccountApplication(IAccountRepository accountRepository, IFileUploader fileUploader, IPasswordHasher passwordHasher, IAuthHelper authHelper, IRoleRepository roleRepository)
        {
            _accountRepository = accountRepository;
            _fileUploader = fileUploader;
            _passwordHasher = passwordHasher;
            _authHelper = authHelper;
            _roleRepository = roleRepository;
        }

        public OperationResult ChangePassword(ChangePassword command)
        {
            var operation = new OperationResult();

            if (command.NewPassword != command.RePassword)
                return operation.Failed(ApplicationMessages.PasswordNotMatch);

            var account = _accountRepository.Get(command.Id);

            if (account == null)
                return operation.Failed(ApplicationMessages.AccountNotFound);

            var encryptedNewPassword = _passwordHasher.Hash(command.NewPassword);
            account.ChangePassword(encryptedNewPassword);
            _accountRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Register(RegisterAccount command)
        {
            var operation = new OperationResult();

            if (_accountRepository.Exist(x => x.UserName == command.UserName || x.Mobile == command.Mobile))
                return operation.Failed(ApplicationMessages.DuplicatedAccount);

            var encryptedPassword = _passwordHasher.Hash(command.Password);
            var picturePath = _fileUploader.UploadProfilePhoto(command.ProfilePhoto);

            var account = new Account(command.FullName, command.UserName, command.Mobile, encryptedPassword, command.RoleId, picturePath);
            _accountRepository.Create(account);
            _accountRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditAccount command)
        {
            var operation = new OperationResult();
            var account = _accountRepository.Get(command.Id);
            if (account == null)
                return operation.Failed(ApplicationMessages.AccountNotFound);

            if (_accountRepository.Exist(x => (x.UserName == command.UserName || x.Mobile == command.Mobile) && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedAccount);

            var picturePath = _fileUploader.UploadProfilePhoto(command.ProfilePhoto);
            account.Edit(command.FullName, command.UserName, command.Mobile, command.RoleId, picturePath);
            _accountRepository.SaveChanges();
            return operation.Succeeded();
        }

        public EditAccount GetDetails(long id)
        {
            return _accountRepository.GetDetails(id);
        }

        public OperationResult LogIn(Login command)
        {
            var operation = new OperationResult();
            var account = _accountRepository.GetBy(command.UserName);

            if (account == null)
                return operation.Failed(ApplicationMessages.WrongUserNameOrPassword);

            var result = _passwordHasher.Check(account.Password, command.Password);
            if (!result.Verified)
                return operation.Failed(ApplicationMessages.WrongUserNameOrPassword);

            var permissions = _roleRepository.Get(account.RoleId).RolePermissions.Select(x => x.Code).ToList();

            var authViewModel = new AuthViewModel(account.Id, account.FullName, account.UserName, permissions, account.RoleId, account.Role.Title);
            _authHelper.SignIn(authViewModel);
            return operation.Succeeded();
        }

        public void LogOut()
        {
            _authHelper.SignOut();
        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();
            var account = _accountRepository.Get(id);
            if (account == null)
                return operation.Failed(ApplicationMessages.AccountNotFound);

            account.Remove();
            _accountRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();
            var account = _accountRepository.Get(id);

            if (account == null)
                return operation.Failed(ApplicationMessages.AccountNotFound);

            account.Restore();
            _accountRepository.SaveChanges();
            return operation.Succeeded();
        }

        public List<AccountViewModel> Search(AccountSearchModel searchModel)
        {
            return _accountRepository.Search(searchModel);
        }

    }
}
