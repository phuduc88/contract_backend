using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.pdf;
using System.Drawing.Imaging;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Contract.Business
{
    public static class PdfProcess
    {

        public static string DrawImageToPdf(string fullPathFilePdf, List<ImageSignInfo> imagesSign, int id) 
        {
            string directoryName = Path.GetDirectoryName(fullPathFilePdf);
            string fileOutPutSign = Path.Combine(directoryName, string.Format("{0}_{1}.pdf", DateTime.Now.ToString("yyyyMMddHHmmss"), id));
            using (FileStream os = new FileStream(fileOutPutSign, FileMode.Create))
            {
                using (PdfReader reader = new PdfReader(fullPathFilePdf))
                {
                    
                    //PdfStamper stamper = PdfStamper.CreateSignature(reader, os, '\0', null, true);
                    PdfStamper stamper = new PdfStamper(reader, os);
                    int numberPage = reader.NumberOfPages;
                    for (int i = 1; i <= numberPage; i++)
                    {
                        var content = stamper.GetOverContent(i);
                        var ImagesInfo = imagesSign.Where(p => p.Page == i);
                        foreach (var ImageInfo in ImagesInfo)
                        {
                            if (ImageInfo == null || ImageInfo.ImagesSign == null)
                            {
                                continue;
                            }

                            var image = iTextSharp.text.Image.GetInstance(ImageInfo.ImagesSign, ImageInfo.ImageFomatType);
                            //image.Alignment = iTextSharp.text.Image.UNDERLYING;
                            float signPositionY = (content.PdfDocument.PageSize.Height - (ImageInfo.CoordinateY / 2)) - image.Height;
                            float signPositionX = (ImageInfo.CoordinateX / 2);
                            image.SetAbsolutePosition(signPositionX, signPositionY);
                            content.AddImage(image);
                        }
                       
                    }
                    stamper.Close();
                }
            }
            return fileOutPutSign;
        }

        public static string SignFilePdf(string fullPathFilePdf, List<ImageSignInfo> imagesSign, RSA Key,X509Certificate2 certificate)
        {
            string fileOutPutSign = @"D:\Project\Contract\BE\Contract\Contract.API\Data\Asset\duclv.pdf";
            using (FileStream os = new FileStream(fileOutPutSign, FileMode.Create))
            {
                using (PdfReader reader = new PdfReader(fullPathFilePdf))
                {

                    PdfStamper stamper = PdfStamper.CreateSignature(reader, os, '\0', null, true);
                    int numberPage = reader.NumberOfPages;
                    for (int i = 1; i <= numberPage; i++)
                    {
                        var content = stamper.GetOverContent(i);
                        ImageSignInfo ImageInfo = imagesSign.FirstOrDefault(p => p.Page == i);

                        if (ImageInfo == null || ImageInfo.ImagesSign == null)
                        {
                            continue;
                        }

                        var image = iTextSharp.text.Image.GetInstance(ImageInfo.ImagesSign, ImageInfo.ImageFomatType);
                        //image.Alignment = iTextSharp.text.Image.UNDERLYING;
                        float signPositionY = content.PdfDocument.PageSize.Height - ImageInfo.CoordinateY;
                        float signPositionX = ImageInfo.CoordinateX - ((image.Width - 99) / 2);
                        image.SetAbsolutePosition(signPositionX, signPositionY);
                        content.AddImage(image);
                    }
                    stamper.Close();
                }
            }

            return string.Empty;
        }

        public static string MergeFile(List<string> fullPathFileInvoice)
        {
            if (fullPathFileInvoice == null || fullPathFileInvoice.Count == 0)
            {
                return null;
            }

            string directoryName = Path.GetDirectoryName(fullPathFileInvoice[0]);
            string fileOutPutSign = Path.Combine(directoryName, string.Format("{0}_Merge.pdf", DateTime.Now.ToString("yyyyMMddHHmmss")));
            using (FileStream stream = new FileStream(fileOutPutSign, FileMode.Create, FileAccess.ReadWrite, FileShare.Read))
            using (Document doc = new Document())
            using (PdfCopy pdf = new PdfCopy(doc, stream))
            {
                doc.Open();
                PdfReader reader = null;
                PdfImportedPage page = null;
                foreach (var item in fullPathFileInvoice)
                {
                    reader = new PdfReader(item);
                    for (int i = 0; i < reader.NumberOfPages; i++)
                    {
                        page = pdf.GetImportedPage(reader, i + 1);
                        pdf.AddPage(page);
                    }
                    pdf.FreeReader(reader);
                    reader.Close();
                    File.Delete(item);
                }
            }

            return fileOutPutSign;
        }

    }
}
