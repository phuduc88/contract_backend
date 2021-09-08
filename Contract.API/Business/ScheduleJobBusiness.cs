using Contract.API.Helper;
using Contract.Business.BL;
using Contract.Business.Constants;
using Contract.Business.Models;
using Contract.Common;
using Contract.Data.DBAccessor;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System;
using Contract.Business.Email;
using System.Net.Mail;
using Contract.Business;
namespace Contract.API.Business
{
    public class ScheduleJobBusiness : BaseBusiness
    {
        private static readonly Logger logger = new Logger();
        private IMyCompanyBO myCompanyBO;
        public static readonly ScheduleJobBusiness Instance = new ScheduleJobBusiness();

        public ScheduleJobBusiness()
        {
            var boFactory = new BOFactory(DbContextManager.GetContext());

         
            //var printConfig = GetPrintConfig();
           
            var uploadImageConfig = new UpdateloadImageConfig
            {
                MaxSizeImage = Config.ApplicationSetting.Instance.MaxSizeImage,
                RootFolderUpload = System.Web.Hosting.HostingEnvironment.MapPath(Config.ApplicationSetting.Instance.FolderAssetOfCompany)
            };

            this.myCompanyBO = boFactory.GetBO<IMyCompanyBO>(uploadImageConfig);
        }

        public void ClearRecuringJob()
        {
            BackgroundJobHelper.ClearRecuringJob(ScheduleJobInfo.DailyCheckUserLoginJobId);
        }

        public void RecuringSendEmailReleaeInvoice()
        {
            //BackgroundJobHelper.RecurringMinutely(() => SendEmailNoticeReleaseInvoice(), Config.ApplicationSetting.Instance.NumberDayScanSendEmail);
        }

        public void RecuringExportInvoicePdf()
        {
            //BackgroundJobHelper.RecurringMinutely(() => CreateFileInvoicePdf(), Config.ApplicationSetting.Instance.NumberSecondScanExportInvoce);
        }
         
    }
}