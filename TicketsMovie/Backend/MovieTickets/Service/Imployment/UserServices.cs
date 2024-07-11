using ZingMp3API.Model;
using ZingMp3API.Service;
using Microsoft.AspNetCore.Identity;
using System.Data;
using ZingMp3API.Data;

namespace ZingMp3API.Service.Imployment
{
    public class UserServices : IUsers
    {
        private readonly UserManager<AccountIdentity> _userManager;
        private readonly SignInManager<AccountIdentity> signInManager;
        private readonly IConfiguration configuration;
        private readonly IRole _role;
        private readonly IHttpContextAccessor _httpContext;

        public UserServices(UserManager<AccountIdentity> userManager, SignInManager<AccountIdentity> signInManager,
            IConfiguration configuration, IRole role, IHttpContextAccessor httpContext)
        {

            this._userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
            this._role = role;
            _httpContext = httpContext;
        }
        public async Task<ResponseModel> ChangePassword(ChangePasswordModel changePasswordModel)
        {
            var user = await _userManager.FindByEmailAsync(changePasswordModel.Email);

            if (user == null)
            {
                return new ResponseModel
                {
                    Status = 401,
                    Message = "Not Found User"
                };

            }
            if (changePasswordModel.OldPassword == null
                || changePasswordModel.Password == null)
            {
                return new ResponseModel
                {
                    Status = 401,
                    Message = "Password unvalid"
                };
            }
            if (changePasswordModel.Password == changePasswordModel.OldPassword)
                return new ResponseModel
                {
                    Status = 401,
                    Message = "Password duplicate"
                };
            var result = await _userManager.ChangePasswordAsync(user, changePasswordModel.OldPassword, changePasswordModel.Password);

            if (result.Succeeded)
                return new ResponseModel
                {
                    Status = 200,
                    Message = "Change password success"
                };
            return new ResponseModel
            {
                Status = 401,
                Message = "Cannot change ..."
            };
        }

        public async Task<ResponseModel> ForgotPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var isComfirm = await _userManager.IsEmailConfirmedAsync(user!);
            if (user == null)
            {
                return new ResponseModel
                {
                    Status = 401,
                    Message = "Not Found"
                };
            }
            if (!isComfirm)
                return new ResponseModel
                {
                    Status = 401,
                    Message = "Email not comfirm"
                };

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            return new ResponseModel
            {
                Status = 200,
                Message = code
            };
        }

        public async Task<ResponseModel> ResetPassword(string code, string newPassword, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var result = await _userManager.ResetPasswordAsync(user, code, newPassword);
            if (result.Succeeded)
            {
                return new ResponseModel
                {
                    Status = 200,
                    Message = "Reset password success"
                };
            }
            return new ResponseModel
            {
                Status = 401,
                Message = "Reset password fail"
            };
        }

        public async Task<ResponseModel> SignInAsync(SignInModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            var passwordValid = await _userManager.CheckPasswordAsync(user!, model.Password);
            // khi check không hợp lệ count + 1 when count = 3 user is lock
            var isLock = await signInManager.PasswordSignInAsync(user, model.Password, true, true);

            if (user == null || !passwordValid)
            {
                return new ResponseModel
                {
                    Status = 401,
                    Message = "Login Fail"
                };
            }

            if (isLock.IsLockedOut)
                return new ResponseModel
                {
                    Status = 401,
                    Message = "Account is locked please try check email and send code to unlock"
                };

            var isComfirm = await _userManager.IsEmailConfirmedAsync(user!);
            if (!isComfirm)
            {
                return new ResponseModel
                {
                    Status = 401,
                    Message = "Account cannot comfirm please check email to comfirm"
                };
            }

            return new ResponseModel
            {
                Message = "Success"
            };
        }

        public async Task<string> SignOutAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return string.Empty;
            //    await _userManager.UpdateSecurityStampAsync(user!);
            return "success";
        }

        public async Task<ResponseModel> SignUp(SignUpModel model)
        {
            var userExists = await _userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
            {
                return new ResponseModel
                {
                    Status = 401,
                    Message = "Account exists"
                };
            }
            var user = new AccountIdentity
            {
                Email = model.Email,
                UserName = model.Email,
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return new ResponseModel
                {
                    Status = 401,
                    Message = "Account exists"
                };
            }
          //  await SetRoleDefault(model.Email, Role.Customer);


            // send email comfirm :
          //  var code = await ComfirmEmail(model!.Email!);

            return new ResponseModel
            {
                Status = 200,
                Message = "Success"
            };
        }

        public async Task<ResponseModel> UnlockAccount(string email, string code)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {

                return new ResponseModel
                {
                    Status = 401,
                    Message = "Not Found"
                };
            }
            user.LockoutEnd = null;
            user.LockoutEnabled = true;
            await _userManager.UpdateAsync(user);
            return new ResponseModel
            {
                Status = 200,
                Message = "Account was unlock"
            };
        }
    }
}
