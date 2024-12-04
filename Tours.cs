using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Travel_agency
{
    public class Tours
    {
        public int Id { get; set; }
        public string Type { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Country { get; set; } = null!;
        public decimal Price { get; set; }
        public string Description { get; set; } = null!;
        public bool IsArchive { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public byte[]? ImageData { get; set; }
        public BitmapImage ImageSource
        {
            get
            {
                return ConvertByteArrayToImage(ImageData);
            }
        }
        
        public static BitmapImage ConvertByteArrayToImage(byte[] imageData)
        {
            using (var stream = new MemoryStream(imageData))
            {
                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = stream;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();

                return bitmapImage;
            }
        }
    }
}
