using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace EnergieatlasLeibnitz.Classes
{
    class Tile
    {
        public string imagePath { get; set; }
        Image tileImage;

        public Tile(string imagePath)
        {
            this.imagePath = imagePath;
        }

        public BitmapImage LoadImage(string imagePath)
        {
            tileImage = new Image();

            if (File.Exists(imagePath))
            {
                FileStream file = null;
                try
                {
                    file = File.OpenRead(imagePath);
                    var bitmap = new BitmapImage();

                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.StreamSource = file;
                    bitmap.EndInit();

                    bitmap.Freeze();
                    return bitmap;
                }
                catch
                {
                    MessageBox.Show("Error loading file: " + imagePath);
                    return null;
                }
            }
            else
                MessageBox.Show("File does not exist: " + imagePath);
            return null;
        }
    }
}
