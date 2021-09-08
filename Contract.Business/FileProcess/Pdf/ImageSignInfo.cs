using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contract.Common.Extensions;
using System.Drawing.Imaging;
using Contract.Business.Constants;
using Contract.Data.Utils;
using System.IO;
namespace Contract.Business
{
    public class ImageSignInfo
    {
        [DataConvert("CoordinateX")]
        public float CoordinateX { get; set; }

        [DataConvert("CoordinateY")]
        public float CoordinateY { get; set; }

        public string FullPathFile { get; set; }

        [DataConvert("Width")]
        public int Width { get; set; }

        [DataConvert("Height")]
        public int Height { get; set; }

        [DataConvert("Page")]
        public int Page { get; set; }

        public string Extension { get; set; }

        public Image ImagesSign
        {
            get
            {
                if (this.FullPathFile.IsNullOrEmpty() || !File.Exists(this.FullPathFile))
                {
                    return null;
                }
               
                Image image = Image.FromFile(this.FullPathFile);
                return (Image)(new Bitmap(image, new Size((this.Width / 2), (this.Height/2))));
            }
        }

        public ImageFormat ImageFomatType
        {
            get
            {
                ImageFormat result = ImageFormat.Png;
                if (this.Extension.IsNullOrEmpty())
                {
                    return result;
                }
                switch (this.Extension)
                {
                    case FileExtension.Png:
                        result = ImageFormat.Png;
                        break;
                    case FileExtension.Jpeg:
                        result = ImageFormat.Jpeg;
                        break;
                    case FileExtension.Jpg:
                        result = ImageFormat.Jpeg;
                        break;
                    default:
                        result = ImageFormat.Png;
                        break;
                }

                return result;
            }
        }

        public ImageSignInfo()
        {

        }
        public ImageSignInfo(object srcObject)
            : this()
        {
            if (srcObject != null)
            {
                DataObjectConverter.Convert<object, ImageSignInfo>(srcObject, this);
            }
        }

        public ImageSignInfo(object srcObject, string fullPathFile, string extension) :
            this(srcObject)
        {
            this.FullPathFile = fullPathFile;
            this.Extension = extension;
        }
    }
}
