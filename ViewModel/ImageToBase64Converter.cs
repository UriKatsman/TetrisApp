using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class ImageToBase64Converter
    {
        public static string ImageToBase64(string imagePath)
        {
            try
            {
                // Read the image into a byte array
                byte[] imageBytes = File.ReadAllBytes(imagePath);

                //convert the byte array to a base64 string
                string base64String = Convert.ToBase64String(imageBytes);

                return base64String;
            }
            catch (Exception e)
            {
                Console.WriteLine("error converting image to base64: " + e.Message);
                return null;
            }

        }
    }
}
