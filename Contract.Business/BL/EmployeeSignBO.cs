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
    public class EmployeeSignBO : IEmployeeSignBO
    {
        #region Fields, Properties

        private readonly IEmployeeSignRepository employeeSignRepository;
        private readonly IEmployeeSignDetailRepository employeeSignDetailRepository;
        #endregion

        #region Contructor

        public EmployeeSignBO(IRepositoryFactory repoFactory)
        {
            Ensure.Argument.NotNull(repoFactory, "repoFactory");
            this.employeeSignRepository = repoFactory.GetRepository<IEmployeeSignRepository>();
            this.employeeSignDetailRepository = repoFactory.GetRepository<IEmployeeSignDetailRepository>();
        }

        #endregion
       
        #region Methods
        public EmployeeSignInfo GetDetail(int id)
        {
            var employeeSign = this.employeeSignRepository.GetDetail(id);
            return new EmployeeSignInfo(employeeSign);
        }

        public EmployeeSignInfo Create(EmployeeSignInfo fileSign)
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

            EmployeeSign employeeSignNew = new EmployeeSign();
            employeeSignNew.CopyData(fileSign);
            this.employeeSignRepository.Insert(employeeSignNew);
            return new EmployeeSignInfo(employeeSignNew);
        }

        public EmployeeSignInfo Update(int id, EmployeeSignInfo employeeSign)
        {
            if (employeeSign == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }

            ResultCode errorCode;
            string errorMessage;
            if (!employeeSign.IsValid(out errorCode, out errorMessage))
            {
                throw new BusinessLogicException(errorCode, errorMessage);
            }

            EmployeeSign currentEmployeeSign = GetEmployeeSign(id);
            currentEmployeeSign.CopyData(employeeSign);
            this.employeeSignRepository.Update(currentEmployeeSign);
            return new EmployeeSignInfo(currentEmployeeSign);
        }

        public ResultCode Delete(int id)
        {
            EmployeeSign currentFileSign = GetEmployeeSign(id);
            bool result = this.employeeSignRepository.Delete(currentFileSign);
            return result ? ResultCode.NoError : ResultCode.UnknownError;
        }

        public void DeleteEployeeSing(int documentId, List<int> employeeWillBeUpdate)
        {
            List<int> eployeeSignDelete = new List<int>();
            var employeeSigns = this.employeeSignRepository.Filter(documentId);
            var employeeSignsDelete = employeeSigns.Where(p => !employeeWillBeUpdate.Contains(p.Id)).ToList();
            foreach (var item in employeeSignsDelete)
            {
                this.employeeSignRepository.Delete(item);
                DeleteEmployeeSingDetail(item.Id);
            }

        }
        #endregion

        private EmployeeSign GetEmployeeSign(int id)
        {
            EmployeeSign currentEmployeeSign = this.employeeSignRepository.GetDetail(id);
            if (currentEmployeeSign == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, "Get FileSign fail width id");
            }

            return currentEmployeeSign;
        }

        private void DeleteEmployeeSingDetail(int employeeSingId)
        {
            var employeeSingsDetail = this.employeeSignDetailRepository.FilterByEmployeeSing(employeeSingId).ToList();
            foreach (var item in employeeSingsDetail)
            {
                this.employeeSignDetailRepository.Delete(item);
            }
        }

    }
}