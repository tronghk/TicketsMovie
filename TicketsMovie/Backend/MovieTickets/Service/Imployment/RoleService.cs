
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ZingMp3API.Data;
using ZingMp3API.Service;

namespace ZingMp3API.Service.Imployment
{
    public class RoleService : IRole
    {
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly UserManager<AccountIdentity> _userManager;
        public RoleService(RoleManager<ApplicationRole> roleManager, UserManager<AccountIdentity> userManager)
        {
            this.roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<bool> checkExistsRole(string role)
        {
            var result = await roleManager.RoleExistsAsync(role);
            return result;
        }
        public async Task<string> AddRole(string role)
        {
            var result = await roleManager.CreateAsync(new ApplicationRole(role));
            if (result.Succeeded)
            {
                return "success";
            }
            return string.Empty;
        }
        public async Task<string> DeleteRole(string nameRole)
        {
            if (!await roleManager.RoleExistsAsync(nameRole))
                return "Not Found";
            var role = await roleManager.FindByNameAsync(nameRole);
            var result = await roleManager.DeleteAsync(role!);
            if (result.Succeeded)
            {
                return "success";
            }
            return string.Empty;
        }
        public async Task<string> UpdateNameRole(string nameRoleOld, string newNameRole)
        {
            if (!await roleManager.RoleExistsAsync(nameRoleOld))
                return "Not Found";
            var role = await roleManager.FindByNameAsync(nameRoleOld);
            role!.Name = newNameRole;
            var result = await roleManager.UpdateAsync(role!);
            if (result.Succeeded)
            {
                return "success";
            }
            return string.Empty;
        }

        public async Task<string> AddRoleDefault(string role)
        {
            if(await checkExistsRole(role))
                return string.Empty;
            await AddRole(role);
            return "success";
        }

        public async Task<string> AddRoleFromUser(string email, string nameRole)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var role = await checkExistsRole(nameRole);

            if (user == null || !role)
            {
                return string.Empty;
            }
            var result = await _userManager.AddToRoleAsync(user, nameRole);
            if (result.Succeeded)
            {
                return "success";
            }
            return "UnValid Duplicate";
        }

        public async Task<string> RemoveRoleFromUser(string email, string nameRole)
        {
            // nếu muốn xóa role thì xóa luôn token và create new token lý do token cũ còn claims role đó
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return "Not Found";
            if (!await checkRoleValid(email, nameRole))
            { return "Error"; }
            await _userManager.RemoveFromRoleAsync(user, nameRole);
            return "success";
        }
        public async Task<bool> checkRoleValid(string email, string nameRole)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return false;
            }
            var userRole = await _userManager.GetRolesAsync(user);
            foreach (var role in userRole)
            {
                if (role.ToString().ToLower() == nameRole.ToLower())
                { return true; }
            }
            return false;

        }

        public async Task SetRoleDefault(string email, string role)
        {
            await AddRoleDefault(role);
            var user = await _userManager.FindByEmailAsync(email);
            await _userManager.AddToRoleAsync(user!, role);
        }
    }
}
