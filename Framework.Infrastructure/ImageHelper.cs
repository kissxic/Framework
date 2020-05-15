using System;
using System.Drawing;
using System.IO;

namespace Framework.Infrastructure
{
    /// <summary>
    /// 图片处理帮助类
    /// </summary>
    public class ImageHelper
    {
        public static ImageHelper Instance { get; } = new ImageHelper();
        /// <summary>
        /// 根据base64字符串返回一个封装好的GDI+位图。
        /// </summary>
        /// <param name="base64string">可转换成位图的base64字符串。</param>
        /// <returns>Bitmap对象。</returns>
        public Bitmap GetImageFromBase64(string base64string)
        {
            byte[] b = Convert.FromBase64String(base64string);
            MemoryStream ms = new MemoryStream(b);
            Bitmap bitmap = new Bitmap(ms);
            return bitmap;
        }

        /// <summary>
        /// 将图片转换成base64字符串。
        /// </summary>
        /// <param name="imagefile">需要转换的图片文件。</param>
        /// <returns>base64字符串。</returns>
        public string GetBase64FromImage(string imagefile)
        {
            string strbaser64 = "";
            try
            {
                Bitmap bmp = new Bitmap(imagefile);
                using (MemoryStream ms = new MemoryStream())
                {
                    bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    byte[] arr = new byte[ms.Length];
                    ms.Position = 0;
                    ms.Read(arr, 0, (int)ms.Length);
                    ms.Close();

                    strbaser64 = Convert.ToBase64String(arr);
                }
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("将图片转换成base64字符串失败，图片地址：" + imagefile);
            }

            return strbaser64;
        }
    }
}
