using Abp.Authorization;
using Eoss.Backend.Authorization.Roles;
using Eoss.Backend.Authorization.Users;

namespace Eoss.Backend.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
