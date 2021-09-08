using System.IO;
using System.IO.Compression;
using Contract.Common.Extensions;
using Contract.Common;
using System;
using System.Diagnostics;
using Contract.Business.Constants;
using System.Drawing;

namespace Contract.Business
{
    public static class FileProcess
    {
        private static readonly Logger logger = new Logger();
        public static void ExtractFile(string fullPathSourceFile, string fullPathDesfile)
        {
            string fullPathDesfileTemp = fullPathSourceFile.Replace(".zip", "_temp");
            ZipFile.ExtractToDirectory(fullPathSourceFile, fullPathDesfileTemp);
            string[] filePaths = Directory.GetFiles(fullPathDesfileTemp);
            foreach (var item in filePaths)
            {
                string result = Path.GetFileName(item);
                File.Copy(item, Path.Combine(fullPathDesfile, result), true);
                logger.UserAction("Duclv", "CopyFile", string.Format("File Copy {0} File des {1}", item, Path.Combine(fullPathDesfile, result)));
            }
        }

        public static string ZipDeliverFile(string fullPathSourceFile, string fullPathDesfile)
        {
            string fileName = Path.GetFileName(fullPathSourceFile);
            File.Copy(fullPathSourceFile, Path.Combine(fullPathDesfile, fileName), true);
            string zipFile = string.Format("{0}.zip", fullPathDesfile);
            if (File.Exists(zipFile))
            {
                File.Delete(zipFile);
            }

            ZipFile.CreateFromDirectory(fullPathDesfile, zipFile);
            return zipFile;
        }

        private static void CreateFolder(string fullPathFile)
        {
            if (!File.Exists(fullPathFile))
            {
                Directory.CreateDirectory(fullPathFile);
            }
        }

        private static string ZipFIle(string fullPathFile)
        {
            string zipFile = string.Format("{0}.zip", fullPathFile);
            ZipFile.CreateFromDirectory(fullPathFile, zipFile);
            Directory.Delete(fullPathFile, true);
            return zipFile;
        }

        private static string GetInforeCompany(string subJect, string key)
        {
            string companyName = string.Empty;
            foreach (var str in subJect.ConvertToList(','))
            {
                string item = str.Trim();
                if (item.Trim().Contains(key))
                {
                    companyName = item.Replace(key, "");
                    break;
                }
            }

            return companyName;
        }

        public static string GetBase64StringFile(string filePath)
        {
            if (!File.Exists(filePath)) return string.Empty;

            var bytes = File.ReadAllBytes(filePath);
            return Convert.ToBase64String(bytes);
        }

        public static string ConverFile2Pdf(string rootFileOpenOffice, string fullPathFileSource)
        {
            string dirRoot = GetDicRoot(fullPathFileSource);
            var processStartInfo = new ProcessStartInfo();
            processStartInfo.Arguments = string.Format("--convert-to pdf:writer_pdf_Export --outdir {0} {1}", dirRoot, fullPathFileSource);
            processStartInfo.FileName = rootFileOpenOffice;
            Process processExportPdf = Process.Start(processStartInfo);
            processExportPdf.WaitForExit();
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fullPathFileSource);
            return Path.Combine(dirRoot, string.Format("{0}{1}", fileNameWithoutExtension, FileExtension.Pdf));;
        }

        public static string ConverImages2Pdf(string fullPathFileSource)
        {
            string dirRoot = GetDicRoot(fullPathFileSource);
            iTextSharp.text.Rectangle pageSize = null;
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fullPathFileSource);
            string filePdfConvert = Path.Combine(dirRoot, string.Format("{0}{1}", fileNameWithoutExtension, FileExtension.Pdf));
            using (var srcImage = new Bitmap(fullPathFileSource))
            {
                pageSize = new iTextSharp.text.Rectangle(0, 0, srcImage.Width, srcImage.Height);
            }
            using (var ms = new MemoryStream())
            {
                var document = new iTextSharp.text.Document(pageSize, 0, 0, 0, 0);
                iTextSharp.text.pdf.PdfWriter.GetInstance(document, ms).SetFullCompression();
                document.Open();
                var image = iTextSharp.text.Image.GetInstance(fullPathFileSource);
                document.Add(image);
                document.Close();

                File.WriteAllBytes(filePdfConvert, ms.ToArray());
            }

            return filePdfConvert;
        }

        public static string GetDicRoot(string fullPathFile)
        {
            FileInfo fInfo = new FileInfo(fullPathFile);
            return fInfo.Directory.FullName;
        }

        public static string SaveFileBase64ToFile(string base64, string rootPathFile, string extension) 
        {
            string fileName = string.Format("{0}.{1}", DateTime.Now.ToString("ddMMyyyyHHmmss"), extension);
            string fullPathFile = Path.Combine(rootPathFile, fileName);
            try
            {
                File.WriteAllBytes(fullPathFile, Convert.FromBase64String(base64));
            }
            catch (Exception ex)
            {

                throw new BusinessLogicException(ResultCode.FileNotFound, ex.Message);
            }

            return fileName;
        }
    }
}
