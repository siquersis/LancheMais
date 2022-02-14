using Microsoft.AspNetCore.Identity;

namespace LancheMais.WebApp.Services
{
    public class SeedUserInitial : ISeedUserInitial
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedUserInitial(UserManager<IdentityUser> userManager,
                               RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void SeedRoles()
        {
            if (!_roleManager.RoleExistsAsync("Member").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Member";
                role.NormalizedName = "MEMBER";
                IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
            }

            if (!_roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                role.NormalizedName = "ADMIN";
                IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
            }
        }

        public void SeedUsers()
        {
            if (_userManager.FindByEmailAsync("usuario@localhost").Result is null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "usuario@localhost";
                user.Email = "usuario@localhost";
                user.NormalizedUserName = "USUARIO@LOCALHOST";
                user.NormalizedEmail = "USUARIO@LOCALHOST";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = _userManager.CreateAsync(user, "Numsey#2022").Result;

                if (result.Succeeded)
                    _userManager.AddToRoleAsync(user, "Member").Wait();
            }

            if (_userManager.FindByEmailAsync("siquer@localhost").Result is null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "siquer@localhost";
                user.Email = "siquer@localhost";
                user.NormalizedUserName = "SIQUER@LOCALHOST";
                user.NormalizedEmail = "SIQUER@LOCALHOST";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = _userManager.CreateAsync(user, "Qwe.123").Result;

                if (result.Succeeded)
                    _userManager.AddToRoleAsync(user, "Admin").Wait();
            }
        }
    }
}
