using Contract.Business.Constants;
using Contract.Business.Models;
using Contract.Common.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Business.Config
{
   public class PrintConfig
    {
       
        public string FullFolderAssetOfCompany { get; private set; }
        public string FullPathFolderExportDeclaration { get; set; }
        public string FullPathFileNameExport { get; set; }
        public string FullPathLogo { get; private set; }
        public string FullPathFileAsset { get; private set; }
        public string FullPathFolderDocumennTemplate { get; set; }

        public string CultureInfo { get; private set; }

        public PrintConfig(string fullPathFileAsset)
        {
            FullPathFileAsset = fullPathFileAsset;
        }

        public void BuildAssetByCompany(CompanyInfo company)
        {
            if (company == null)
            {
                return;
            }

            FullFolderAssetOfCompany = Path.Combine(FullPathFileAsset, company.Id.ToString());
            FullPathFolderExportDeclaration = Path.Combine(FullFolderAssetOfCompany, AssetData.Declaration);
            FullPathFolderDocumennTemplate = Path.Combine(FullPathFileAsset, AssetData.DocumentTemplates);
            if (!company.Logo.IsNullOrEmpty())
            {
                FullPathLogo = Path.Combine(FullFolderAssetOfCompany, company.Logo);
            }
            else
            {
                FullPathLogo = string.Empty;
            }

        }

    }
}
