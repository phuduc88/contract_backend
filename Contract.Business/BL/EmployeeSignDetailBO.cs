using Contract.Business.Constants;
using Contract.Business.DAO;
using Contract.Business.Models;
using Contract.Common;
using Contract.Data.DBAccessor;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Contract.Business.BL
{
    public class EmployeeSignDetailBO : IEmployeeSignDetailBO
    {
        #region Fields, Properties

        private readonly IEmployeeSignDetailRepository employeeSignDetailRepository;
        #endregion

        #region Contructor

        public EmployeeSignDetailBO(IRepositoryFactory repoFactory)
        {
            Ensure.Argument.NotNull(repoFactory, "repoFactory");
            this.employeeSignDetailRepository = repoFactory.GetRepository<IEmployeeSignDetailRepository>();
        }

        #endregion
       
        #region Methods
        public EmployeeSignDetailInfo GetDetail(int id)
        {
            var employeeSignDetail = this.employeeSignDetailRepository.GetDetail(id);
            return new EmployeeSignDetailInfo(employeeSignDetail);
        }

        public EmployeeSignDetailInfo Create(EmployeeSignDetailInfo fileSign)
        {
            if (fileSign == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }

            ResultCode errorCode;
            string errorMessage;
            if (!fileSign.IsValid(out errorCode, out errorMessage))
            {
                throw new BusinessLogicException(errorCode, errorMessage);
            }

            EmployeeSignDetail employeeSignDetailNew = new EmployeeSignDetail();
            employeeSignDetailNew.CopyData(fileSign);
            this.employeeSignDetailRepository.Insert(employeeSignDetailNew);
            return new EmployeeSignDetailInfo(employeeSignDetailNew);
        }

        public EmployeeSignDetailInfo Update(int id, EmployeeSignDetailInfo fileSign)
        {
            if (fileSign == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }

            ResultCode errorCode;
            string errorMessage;
            if (!fileSign.IsValid(out errorCode, out errorMessage))
            {
                throw new BusinessLogicException(errorCode, errorMessage);
            }

            EmployeeSignDetail currentEmployeeSign = GetEmployeeSignDetail(id);
            currentEmployeeSign.CopyData(fileSign);
            this.employeeSignDetailRepository.Update(currentEmployeeSign);
            return new EmployeeSignDetailInfo(currentEmployeeSign);
        }

        public ResultCode Delete(int id)
        {
            EmployeeSignDetail currentFileSign = GetEmployeeSignDetail(id);
            bool result = this.employeeSignDetailRepository.Delete(currentFileSign);
            return result ? ResultCode.NoError : ResultCode.UnknownError;
        }

        public ResultCode DeleteByDocumentId(int documentId)
        {
            var employeeSignDetail = this.employeeSignDetailRepository.Filter(documentId).ToList();
            foreach (var item in employeeSignDetail)
            {
                this.employeeSignDetailRepository.Delete(item);
            }

            return ResultCode.NoError;
        }

        #endregion

        private EmployeeSignDetail GetEmployeeSignDetail(int id)
        {
            EmployeeSignDetail currentEmployeeSignDetail = this.employeeSignDetailRepository.GetDetail(id);
            if (currentEmployeeSignDetail == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, "Get FileSign fail width id");
            }

            return currentEmployeeSignDetail;
        }
         
    }
}