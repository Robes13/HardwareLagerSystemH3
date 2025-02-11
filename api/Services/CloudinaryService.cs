using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

public class CloudinaryService
{
    private readonly Cloudinary _cloudinary;

    public CloudinaryService()
    {
        string cloudServer = Environment.GetEnvironmentVariable("CLOUD_SERVER");
        string cloudPass = Environment.GetEnvironmentVariable("CLOUD_PASS");
        string cloudKey = Environment.GetEnvironmentVariable("CLOUD_KEY");

        var account = new Account(
            cloudServer,
            cloudPass,
            cloudKey
        );

        _cloudinary = new Cloudinary(account);
        _cloudinary.Api.Secure = true;
    }

    public async Task<string> UploadImageAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            throw new ArgumentException("File is empty.");
        }

        using var stream = file.OpenReadStream();
        
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(file.FileName, stream),
            Transformation = new Transformation().Width(500).Height(500).Crop("limit"), 
            Folder = "hardware-images"
        };

        var uploadResult = await _cloudinary.UploadAsync(uploadParams);

        return uploadResult.SecureUrl.AbsoluteUri;
    }
}
