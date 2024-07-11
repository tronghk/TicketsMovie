using ZingMp3API.Model;
using System.Security.Claims;

namespace ZingMp3API.Service
{
    public interface IUsers
    {
        public Task<ResponseModel> SignInAsync(SignInModel model);
        public Task<string> SignOutAsync(string email);
        public Task<ResponseModel> SignUp(SignUpModel model);
        public Task<ResponseModel> ForgotPassword(string email);
        public Task<ResponseModel> ResetPassword(string code, string newPassword, string email);
        public Task<ResponseModel> ChangePassword(ChangePasswordModel changePasswordModel);
        public Task<ResponseModel> UnlockAccount(string email, string code);
    }
}
