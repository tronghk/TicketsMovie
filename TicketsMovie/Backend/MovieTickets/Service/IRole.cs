namespace ZingMp3API.Service
{
    public interface IRole
    {
        public Task<bool> checkExistsRole(string role);
        public Task<string> AddRole(string role);

        public Task<string> DeleteRole(string nameRole);

        public Task<string> UpdateNameRole(string nameRoleOld, string newNameRole);

        public Task<string> AddRoleDefault(string role);
        public Task<string> AddRoleFromUser(string email, string nameRole);
        public Task<string> RemoveRoleFromUser(string email, string nameRole);
        public Task SetRoleDefault(string email, string role);

    }
}
