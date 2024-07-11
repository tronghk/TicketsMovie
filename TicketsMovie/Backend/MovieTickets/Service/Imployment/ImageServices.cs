

using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System;
using ZingMp3API.Services;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AppGrIT.Services.Imployement
{
    public class ImageServices : IImages
    {
        private readonly IConfiguration _configuration;
        private readonly FirebaseAuthProvider _firebaseAuth;
        private readonly FirebaseStorage _storage;
        private readonly string Bucket;
        private IWebHostEnvironment _environment;
        public ImageServices( IConfiguration configuration, IWebHostEnvironment Environment) {
        
            
            _configuration = configuration;
            _firebaseAuth = new FirebaseAuthProvider(new Firebase.Auth.FirebaseConfig(_configuration["Firebase:API_Key"]));
            Bucket = _configuration["Firebase:Storage"]!;
            _storage= new FirebaseStorage(_configuration["Firebase:Storage"]);
            _environment = Environment;
        }

        public async Task<List<string>> AddImagesPostAsync(List<IFormFile> fileImage)    
        {

            var listLink = new List<string>();  
            foreach (IFormFile file in fileImage)
            {
                var fileName = file.FileName;
                DateTime dt = DateTime.Now; // Or whatever
                string s = dt.ToString("yyyyMMddHHmmss");
                string newName ="IMG-" + s + "-" + RandomChar() + fileName.Substring(fileName.Length-4);
                FileStream stream;
                if (fileName.Length > 0)
                {
                    
                    string path = Path.Combine(_environment.ContentRootPath, "StatisFileTesting","Images",fileName);

                    using (var st = System.IO.File.Create(path))
                    {
                        await file.CopyToAsync(st);
                    }
                    stream = new FileStream(Path.Combine(path), FileMode.Open);
                    var link = await Upload(stream, newName, "Images");
                    if (link != null)
                    {
                        if (File.Exists(path))
                        {
                            stream.Close();
                            File.Delete(path);
                        }
                        listLink.Add(link);


                    }

                }
            }
            return listLink;
        }
        public static string RandomChar()
        {
            Random random = new Random();
            const string chars = "TeamITNeverDie";
            return new string(Enumerable.Repeat(chars, 6)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public async Task<string> Upload(FileStream stream, string fileName,string storage)
        {
            string email = "user@example.com";
            string password = "string";
            var link = await _firebaseAuth.SignInWithEmailAndPasswordAsync(email, password);

           var cancellation = new CancellationTokenSource();
            var task = new FirebaseStorage(

                Bucket,
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(link.FirebaseToken),
                    ThrowOnCancel = true
                }
                ).Child(storage).Child(fileName).PutAsync(stream,cancellation.Token);
            try
            {
                string path = await task;
                return path;

            }catch(Exception ex)
            {
                return null!;
            }
        }
        public async Task<string> GetLinkAvatarDefault()
        {
            
            var starsRef = _storage.Child("IMG_Default").Child("sbcf-default-avatar.png");
            string link = await starsRef.GetDownloadUrlAsync();
            return link;
        }
        public async Task<string> GetLinkCoverDefault()
        {
           
            var starsRef = _storage.Child("IMG_Default").Child("cover_default.jpg");
            string link = await starsRef.GetDownloadUrlAsync();
            return link;
        }
        public async Task<string> DeleteFileStorageToPath(string url, string storageName)
        {
            try
            {
                var filename = System.IO.Path.GetFileName(url).ToString();
            int start = filename.IndexOf("IMG-");
            int end = filename.IndexOf("?");
            filename = filename.Substring(start,end-start);
           

            var loginInfo = await _firebaseAuth.SignInWithEmailAndPasswordAsync("user@example.com", "string");
            var storage = new FirebaseStorage(Bucket, new FirebaseStorageOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(loginInfo.FirebaseToken),
                ThrowOnCancel = false
            });

                
                
                await storage.Child(storageName).Child(filename).DeleteAsync();
                return "success";
            }
            catch (Exception e)
            {
                var msg = e.Message;
                return "success";
            }

        }

        public async Task<List<string>> AddMusicAsync(List<IFormFile> fileImage)
        {
            var listLink = new List<string>();
            foreach (IFormFile file in fileImage)
            {
                var fileName = file.FileName;
                DateTime dt = DateTime.Now; // Or whatever
                string s = dt.ToString("yyyyMMddHHmmss");
                string newName = "MP3-" + s + "-" + RandomChar() + fileName.Substring(fileName.Length - 4);
                FileStream stream;
                if (fileName.Length > 0)
                {

                    string path = Path.Combine(_environment.ContentRootPath, "StatisFileTesting", "Musics", fileName);

                    using (var st = System.IO.File.Create(path))
                    {
                        await file.CopyToAsync(st);
                    }
                    stream = new FileStream(Path.Combine(path), FileMode.Open);
                    var link = await Upload(stream, newName,"Musics");
                    if (link != null)
                    {
                        if (File.Exists(path))
                        {
                            stream.Close();
                            File.Delete(path);
                        }
                        listLink.Add(link);


                    }

                }
            }
            return listLink;
        }
    }
}
