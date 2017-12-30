using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
namespace CargoMate.Web.FrontEnd.Shared
{
    public static  class CargoMateImageHandler
    {
        public static string VehicleImagesUrl = $"{HttpContext.Current.Server.MapPath(@"\")}SystemImages/FrontEndImages";

        public static string SingleFileUploader(HttpPostedFileBase file)
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
            var isExists = Directory.Exists(VehicleImagesUrl);

            if (!isExists)
                Directory.CreateDirectory(VehicleImagesUrl);

            var imageUrl = $"{VehicleImagesUrl}\\{file.FileName}";
            file.SaveAs(imageUrl);

            return SessionKeys.VehicleImagePath;
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
                var random = new Random();
                var randomNumber = random.Next(0, 100);

                var imageName = $"{DateTime.Now:yyyyMMddTHHmmss}-{randomNumber}.jpg";

                var imgPath = Path.Combine(folderPath, imageName);
                var source = imgStr.Split(',').Count() > 1 ? imgStr.Split(',')[1] : imgStr;
                var imageBytes = Convert.FromBase64String(source);

                File.WriteAllBytes(imgPath, imageBytes);

                return imageName;
            }
            catch (Exception)
            {
                return imgStr;
            }
        }
    }
}