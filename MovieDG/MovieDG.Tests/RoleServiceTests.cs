namespace MovieDG.Tests
{
    using Microsoft.AspNetCore.Identity;
    using Moq;
    using MovieDG.Common;
    using MovieDG.Core.Services;
    using MovieDG.Data.Data.Models;
    using NUnit.Framework;
    using System.Threading.Tasks;

    [TestFixture]
    public class RoleServiceTests
    {
        private RoleService roleService;
        private Mock<UserManager<ApplicationUser>> mockUserManager;
        private Mock<RoleManager<ApplicationRole>> mockRoleManager;

        [SetUp]
        public void SetUp()
        {
            var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            var roleStoreMock = new Mock<IRoleStore<ApplicationRole>>();

            mockUserManager = new Mock<UserManager<ApplicationUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
            mockRoleManager = new Mock<RoleManager<ApplicationRole>>(roleStoreMock.Object, null, null, null, null);

            roleService = new RoleService(mockUserManager.Object, mockRoleManager.Object);
        }

        [Test]
        public async Task AddUserToRoleAsyncTest()
        {
            string userName = "ToBeAdmin";
            string role = "Admin";
            var user = new ApplicationUser { UserName = userName };
            var mockRoleStore = new Mock<IRoleStore<ApplicationRole>>();
            mockRoleManager.Setup(rm => rm.RoleExistsAsync(role))
                .ReturnsAsync(true);
            mockUserManager.Setup(um => um.FindByNameAsync(userName))
                .ReturnsAsync(user);
            mockUserManager.Setup(um => um.IsInRoleAsync(user, role))
                .ReturnsAsync(false);
            mockUserManager.Setup(um => um.AddToRoleAsync(user, role))
                .Returns(Task.FromResult(IdentityResult.Success));

            
            await roleService.AddUserToRoleAsync(userName, role);

            mockUserManager.Verify(um => um.FindByNameAsync(userName), Times.Once);
            mockRoleManager.Verify(rm => rm.RoleExistsAsync(role), Times.Once);
            mockUserManager.Verify(um => um.IsInRoleAsync(user, role), Times.Once);
            mockUserManager.Verify(um => um.AddToRoleAsync(user, role), Times.Once);
        }

        [Test]
        public void AddUserToRoleShouldThrowsArgumentExceptionIfUserNotFoundTest()
        {
            string userName = "nonExistentUser";
            string role = "Admin";
            ApplicationUser user = null;
            mockUserManager.Setup(um => um.FindByNameAsync(userName))
                .ReturnsAsync(user);

            Assert.ThrowsAsync<ArgumentException>(
                async () => await roleService.AddUserToRoleAsync(userName, role));
            mockUserManager.Verify(um => um.FindByNameAsync(userName), Times.Once);
            mockRoleManager.Verify(rm => rm.RoleExistsAsync(role), Times.Never);
            mockUserManager.Verify(um => um.IsInRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()), Times.Never);
            mockUserManager.Verify(um => um.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void RoleDoesNotExistThrowsArgumentExceptionTest()
        {
            string userName = "ToBeAdmin";
            string role = "Moderator";
            ApplicationUser user = new ApplicationUser { UserName = userName };
            mockUserManager.Setup(um => um.FindByNameAsync(userName))
                .ReturnsAsync(user);
            mockRoleManager.Setup(rm => rm.RoleExistsAsync(role))
                .ReturnsAsync(false);

            Assert.ThrowsAsync<ArgumentException>(
                async () => await roleService.AddUserToRoleAsync(userName, role));
            mockUserManager.Verify(um => um.FindByNameAsync(userName), Times.Once);
            mockRoleManager.Verify(rm => rm.RoleExistsAsync(role), Times.Once);
            mockUserManager.Verify(um => um.IsInRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()), Times.Never);
            mockUserManager.Verify(um => um.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void UserAlreadyInRoleThrowsArgumentExceptionTest()
        {
            string userName = "ToBeAdmin";
            string role = "Admin";
            ApplicationUser user = new ApplicationUser { UserName = userName };
            mockUserManager.Setup(um => um.FindByNameAsync(userName))
                .ReturnsAsync(user);
            mockRoleManager.Setup(rm => rm.RoleExistsAsync(role))
                .ReturnsAsync(true);
            mockUserManager.Setup(um => um.IsInRoleAsync(user, role))
                .ReturnsAsync(true);

            Assert.ThrowsAsync<ArgumentException>(
                async () => await roleService.AddUserToRoleAsync(userName, role));
            mockUserManager.Verify(um => um.FindByNameAsync(userName), Times.Once);
            mockRoleManager.Verify(rm => rm.RoleExistsAsync(role), Times.Once);
            mockUserManager.Verify(um => um.IsInRoleAsync(user, role), Times.Once);
            mockUserManager.Verify(um => um.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()), Times.Never);
        }

        [Test]
        public async Task GetAllUsersRolesAsyncTest()
        {
            var memberRole = new ApplicationRole { Name = GlobalConstants.MemberRoleName };
            var adminRole = new ApplicationRole { Name = GlobalConstants.AdminRoleName };

            var adminUser1 = new ApplicationUser { UserName = "admin1", Email = "admin1@example.com" };
            var adminUser2 = new ApplicationUser { UserName = "admin2", Email = "admin2@example.com" };

            var memberUser1 = new ApplicationUser { UserName = "member1" };
            var memberUser2 = new ApplicationUser { UserName = "member2" };

            var mockRoleStore = new Mock<IRoleStore<ApplicationRole>>();
            var mockUserStore = new Mock<IUserStore<ApplicationUser>>();

            var mockRoleManager = new Mock<RoleManager<ApplicationRole>>(mockRoleStore.Object, null, null, null, null);
            var mockUserManager = new Mock<UserManager<ApplicationUser>>(mockUserStore.Object, null, null, null, null, null, null, null, null);

            mockRoleManager.Setup(rm => rm.FindByNameAsync(GlobalConstants.MemberRoleName))
                .ReturnsAsync(memberRole);
            mockRoleManager.Setup(rm => rm.FindByNameAsync(GlobalConstants.AdminRoleName))
                .ReturnsAsync(adminRole);

            mockUserManager.Setup(um => um.GetUsersInRoleAsync(adminRole.Name))
                .ReturnsAsync(new List<ApplicationUser> { adminUser1, adminUser2 });
            mockUserManager.Setup(um => um.GetUsersInRoleAsync(memberRole.Name))
                .ReturnsAsync(new List<ApplicationUser> { memberUser1, memberUser2 });

            var roleService = new RoleService(mockUserManager.Object, mockRoleManager.Object);

            var result = await roleService.GetAllUsersRolesAsync();

            Assert.NotNull(result);
            Assert.NotNull(result.Admins);
            Assert.NotNull(result.Members);
            Assert.AreEqual(2, result.Admins.Count());
            Assert.AreEqual(2, result.Members.Count());

            Assert.Contains(adminUser1.UserName, result.Admins.Select(a => a.UserName).ToList());
            Assert.Contains(adminUser2.UserName, result.Admins.Select(a => a.UserName).ToList());
            Assert.Contains(memberUser1.UserName, result.Members.Select(m => m.UserName).ToList());
            Assert.Contains(memberUser2.UserName, result.Members.Select(m => m.UserName).ToList());

        }

        [Test]
        public async Task RemoveRoleFromUserAsyncTest()
        {
            string userName = "ToBeAdmin";
            string role = "Admin";
            var user = new ApplicationUser { UserName = userName };

            mockUserManager.Setup(um => um.FindByNameAsync(userName))
                .ReturnsAsync(user);
            mockRoleManager.Setup(rm => rm.RoleExistsAsync(role))
                .ReturnsAsync(true);
            mockUserManager.Setup(um => um.RemoveFromRoleAsync(user, role))
                .Returns(Task.FromResult(IdentityResult.Success));

            await roleService.RemoveRoleFromUser(userName, role);

            mockUserManager.Verify(um => um.FindByNameAsync(userName), Times.Once);
            mockRoleManager.Verify(rm => rm.RoleExistsAsync(role), Times.Once);
            mockUserManager.Verify(um => um.RemoveFromRoleAsync(user, role), Times.Once);
        }

        [Test]
        public void RemoveRoleFromUserShouldThrowsArgumentExceptionIfUserNotFoundTest()
        {
            string userName = "nonExistentUser";
            string role = "Admin";
            ApplicationUser user = null;
            mockUserManager.Setup(um => um.FindByNameAsync(userName))
                .ReturnsAsync(user);

            Assert.ThrowsAsync<ArgumentException>(
                async () => await roleService.RemoveRoleFromUser(userName, role));
            mockUserManager.Verify(um => um.FindByNameAsync(userName), Times.Once);
            mockRoleManager.Verify(rm => rm.RoleExistsAsync(It.IsAny<string>()), Times.Never);
            mockUserManager.Verify(um => um.RemoveFromRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void RemoveRoleFromUserShouldThrowsArgumentExceptionIfRoleNotExistVoidTest()
        {
            string userName = "Admin";
            string roleName = "NonExistentRole";

            var user = new ApplicationUser { UserName = userName };

            mockUserManager.Setup(um => um.FindByNameAsync(userName))
            .ReturnsAsync(user);
            mockRoleManager.Setup(rm => rm.RoleExistsAsync(roleName))
               .ReturnsAsync(false);

            Assert.ThrowsAsync<ArgumentException>(
                async () => await roleService.RemoveRoleFromUser(userName, roleName));

            mockUserManager.Verify(um => um.FindByNameAsync(userName), Times.Once);
            mockRoleManager.Verify(rm => rm.RoleExistsAsync(roleName), Times.Once);
            mockUserManager.Verify(um => um.RemoveFromRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()), Times.Never);
        }
    }
}
