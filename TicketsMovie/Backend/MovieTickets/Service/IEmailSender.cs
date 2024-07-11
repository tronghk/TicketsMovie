namespace ZingMp3API.Service
{
    public interface IEmailSender
    {
        public Task<bool> SendEmailAsync(string email, string subject, string message);
        public Task<string> ComfirmEmail(string email);
        public Task<string> RegisterComfirmEmail(string email, string code);
    }
}
