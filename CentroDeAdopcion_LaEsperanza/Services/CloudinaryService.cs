using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace CentroDeAdopcion_LaEsperanza.Services
{
    public class CloudinaryService
    {

        private readonly Cloudinary _cloudinary;

        public CloudinaryService(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;

        }

        public async Task<string> UploadImageToCloudinary(IFormFile fotoFile)
        {
            if (fotoFile == null || fotoFile.Length == 0)
            {
                return null;
            }
            using (var stream = fotoFile.OpenReadStream())
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(fotoFile.FileName, stream),
                    UseFilename = true,
                    UniqueFilename = true,
                    Folder = "mascotas"
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                return uploadResult?.SecureUrl?.ToString();
            }
        }
    }
}
