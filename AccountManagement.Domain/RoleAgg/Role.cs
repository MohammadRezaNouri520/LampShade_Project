using _0_Framework.Domain;
using AccountManagement.Domain.AccountAgg;
using System.Collections.Generic;

namespace AccountManagement.Domain.RoleAgg
{
    public class Role : EntityBase
    {
        public string Title { get; private set; }
        public List<Account> Accounts { get; private set; }


        public Role(string title)
        {
            Title = title;
        }

        public void Edit(string title)
        {
            Title = title;
        }
    }
}
