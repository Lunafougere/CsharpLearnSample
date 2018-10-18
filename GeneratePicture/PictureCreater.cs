using GeneratePicture.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratePicture
{
    public class PictureCreater
    {
        public static Bitmap GetPicture(string content,string qRCodercontent)
        {
            Bitmap bitmap = bitmap = Resources.model;
            Graphics g = Graphics.FromImage(bitmap);

            SolidBrush drawBrush = new SolidBrush(Color.White);
            Color color = Color.FromArgb(255, 5, 132, 229);
            Font font = new Font("宋体", 30, FontStyle.Regular);
            g.DrawString(content, font, drawBrush, new System.Drawing.Point(50, 10));

            Bitmap coderbitmap = QRCoderCreater.CreateQRCoder(qRCodercontent, color);
            g.DrawImage(coderbitmap, new System.Drawing.Point(50, 50));

            return bitmap;
        }
    }
}
