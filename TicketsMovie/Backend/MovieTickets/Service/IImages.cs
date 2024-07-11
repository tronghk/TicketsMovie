

namespace ZingMp3API.Services
{
    public interface IImages
    {
        public Task<List<string>> AddImagesPostAsync(List<IFormFile> fileImage);

        public Task<List<string>> AddMusicAsync(List<IFormFile> file);



        public Task<string> GetLinkAvatarDefault();
        public Task<string> GetLinkCoverDefault();
        public Task<string> DeleteFileStorageToPath(string url, string storageName);
    }
}
