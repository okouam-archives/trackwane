using NUnit.Framework;
using Shouldly;
using Trackwane.AccessControl.Domain.Organizations;
using Trackwane.AccessControl.Domain.Users;

namespace Trackwane.Tests.Behavior.Domain
{
    [TestFixture]
    public class OrganizationTests
    {
        private Organization organization;
        private User user;

        [SetUp]
        public void SetUp()
        {
            user = new User { Key = "X" };
            organization = new Organization();
        }

        [Test]
        public void Can_Manage_Organization_When_User_Has_Manage_Permission()
        {
            organization.GrantManagePermission("X");
            organization.CanManage(user).ShouldBeTrue();
        }

        [Test]
        public void Cannot_Manage_Organization_When_User_Does_Not_Have_Manage_Permission()
        {
            organization.CanManage(user).ShouldBeFalse();
        }

        [Test]
        public void HasWriteAccess_Is_False_When_A_User_Only_Has_Read_Access()
        {
            organization.GrantViewPermission("X");
            organization.CanManage(user).ShouldBeFalse();
        }

        [Test]
        public void HasWriteAccess_Is_True_When_User_Is_System_Manager_Without_Write_Access()
        {
            user.Role = Role.SystemManager;
            organization.CanManage(user).ShouldBeTrue();
        }
        
        [Test]
        public void HasReadAccess_Is_True_When_User_Is_System_Manager_Without_Read_Access()
        {
            user.Role = Role.SystemManager;
            organization.CanView(user).ShouldBeTrue();
        }

        [Test]
        public void HasReadAccess_Is_True_When_User_Has_Read_Access()
        {
            organization.GrantViewPermission("X");
            organization.CanView(user).ShouldBeTrue();
        }

        [Test]
        public void HasReadAccess_Is_True_When_User_Has_Write_Access()
        {
            organization.GrantManagePermission("X");
            organization.CanView(user).ShouldBeTrue();
        }

        [Test]
        public void Having_View_Permission_Granted_Revokes_Administrate_Permission()
        {
            organization.GrantAdministratePermission("X");
            organization.GrantViewPermission("X");
            organization.GetAdministrators().ShouldBeEmpty();
        }

        [Test]
        public void Having_View_Permission_Granted_Revokes_Manage_Permission()
        {
            organization.GrantManagePermission("X");
            organization.GrantViewPermission("X");
            organization.GetManagers().ShouldBeEmpty();
        }

        [Test]
        public void Having_Manage_Permission_Granted_Revokes_Administrate_Permission()
        {
            organization.GrantAdministratePermission("X");
            organization.GrantManagePermission("X");
            organization.GetAdministrators().ShouldBeEmpty();
        }

        [Test]
        public void Having_Manage_Permission_Granted_Revokes_View_Permission()
        {
            organization.GrantViewPermission("X");
            organization.GrantManagePermission("X");
            organization.GetViewers().ShouldBeEmpty();
        }

        [Test]
        public void Having_Administrate_Permission_Granted_Revokes_Manage_Permission()
        {
            organization.GrantManagePermission("X");
            organization.GrantAdministratePermission("X");
            organization.GetManagers().ShouldBeEmpty();
        }

        [Test]
        public void Having_Administrate_Permission_Granted_Revokes_View_Permission()
        {
            organization.GrantViewPermission("X");
            organization.GrantAdministratePermission("X");
            organization.GetViewers().ShouldBeEmpty();
        }

        [Test]
        public void After_Having_View_Permission_Revoked_User_Cannot_View_Organization()
        {
            organization.GrantViewPermission("X");
            organization.RevokeViewPermission("X");
            organization.CanView(user).ShouldBeFalse();
        }

        [Test]
        public void After_Having_Manage_Permission_Revoked_User_Cannot_Manage_Organization()
        {
            organization.GrantManagePermission("X");
            organization.RevokeManagePermission("X");
            organization.CanManage(user).ShouldBeFalse();
        }

        [Test]
        public void Revoking_Manage_Permission_Does_Not_Revoke_View_Permissions()
        {
            organization.GrantViewPermission("X");
            organization.RevokeManagePermission("X");
            organization.CanView(user).ShouldBeTrue();
        }

        [Test]
        public void Revoking_Manage_Permission_Does_Not_Revoke_Administrate_Permissions()
        {
            organization.GrantAdministratePermission("X");
            organization.RevokeManagePermission("X");
            organization.CanAdministrate(user).ShouldBeTrue();
        }

        [Test]
        public void Revoking_View_Permission_Does_Not_Revoke_Manage_Permissions()
        {
            organization.GrantManagePermission("X");
            organization.RevokeViewPermission("X");
            organization.CanManage(user).ShouldBeTrue();
        }

        [Test]
        public void Revoking_View_Permission_Does_Not_Revoke_Administrate_Permissions()
        {
            organization.GrantAdministratePermission("X");
            organization.RevokeViewPermission("X");
            organization.CanAdministrate(user).ShouldBeTrue();
        }
    }
}
