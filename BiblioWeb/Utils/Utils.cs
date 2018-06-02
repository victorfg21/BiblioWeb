using ImageMagick;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Transforms;
using System;
using System.IO;

namespace BiblioWeb.Utils
{
    public static class Utils
    {
        public static byte[] ToByteArray(this IFormFile file)
        {
            using (BinaryReader reader = new BinaryReader(file.OpenReadStream()))
            {
                return Redimensionar(reader.ReadBytes((int)file.Length));
            }
        }

        private static byte[] Redimensionar(byte[] file)
        {
            using (MagickImage image = new MagickImage(file))
            {
                image.Resize(0, 300);
                return image.ToByteArray();
            }
        }
    }
}
