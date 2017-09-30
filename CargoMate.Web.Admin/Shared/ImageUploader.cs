using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace CargoMate.Web.Admin.Shared
{
    public static class ImageUploader
    {

        public static string SingleFileUploader(HttpPostedFileBase file,string folderPath)
        {
            if (file == null)
            {
                return string.Empty;
            }

            if (file.ContentLength <= 0)
            {
                return string.Empty;
            }
            //try
            //{
            var isExists = Directory.Exists($"{HttpContext.Current.Server.MapPath(@"\")}{folderPath}");

            if (!isExists)
                Directory.CreateDirectory($"{HttpContext.Current.Server.MapPath(@"\")}{folderPath}");

            var imageUrl = $"{$"{HttpContext.Current.Server.MapPath(@"\")}{folderPath}"}\\{file.FileName}";
            file.SaveAs(imageUrl);

            return SessionKeys.UserImagesPath;
            //}
            //catch (Exception ex)
            //{
            //    return string.Empty;
            //}

        }

        public static string SaveImageFromBase64(string imgStr,string folderPath)
        {
            try
            {

                if (!string.IsNullOrEmpty(imgStr))
                {
                    var imageName = $"Vehicle{Guid.NewGuid()}image.jpg";

                    var imgPath = Path.Combine($"{HttpContext.Current.Server.MapPath(@"\")}{folderPath}", imageName);

                    var imageBytes = Convert.FromBase64String(imgStr.Split(',')[1]);

                    File.WriteAllBytes(imgPath, imageBytes);

                    return $"{folderPath}/{imageName}"; 
                }
                return string.Empty;
            }
            catch (Exception)
            {

                return imgStr;
            }
        }
    }
}