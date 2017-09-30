using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
namespace CargoMate.Web.FrontEnd.Shared
{
    public static  class CargoMateImageHandler
    {
        public static string VehicleImagesUrl = $"{HttpContext.Current.Server.MapPath(@"\")}{"SystemImages/FrontEndImages"}";

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

            var imageUrl = string.Format("{0}\\{1}", VehicleImagesUrl, file.FileName);
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

                var imageName = $"{folderPath}{DateTime.Now.ToString("yyyyMMddTHHmmss")}-{randomNumber}.jpg";

                var imgPath = Path.Combine(VehicleImagesUrl, imageName);

                byte[] imageBytes = Convert.FromBase64String(imgStr.Split(',')[1]);

                File.WriteAllBytes(imgPath, imageBytes);

                return imageName;
            }
            catch (Exception)
            {
                return "SystemImages/placeholder.png";
            }
        }
    }
}