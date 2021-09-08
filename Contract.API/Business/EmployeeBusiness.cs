using Contract.Business.BL;
using Contract.Business.Config;
using Contract.Business.Constants;
using Contract.Business.Models;
using Contract.Common;
using System.Collections.Generic;
using System.Linq;

namespace Contract.API.Business
{
    public class EmployeeBusiness : BaseBusiness
    {
        #region Fields, Properties

        private IEmployeeeBO employeeBO;
        private PrintConfig printConfig;
        #endregion Fields, Properties

        #region Contructor

        public EmployeeBusiness(IBOFactory boFactory)
        {
            printConfig = GetPrintConfig();
            this.employeeBO = boFactory.GetBO<IEmployeeeBO>(printConfig);
        }

        #endregion Contructor

        #region Methods

        public IEnumerable<EmployeeInfo> Filter(out int totalRecords, string orderType = null, string orderby = null, int skip = 0, int take = int.MaxValue)
        {
            ConditionSearchEmployeer condition = new ConditionSearchEmployeer(this.CurrentUser, string.Empty, string.Empty, orderType, orderby);
            totalRecords = this.employeeBO.CountFillter(condition);
            return this.employeeBO.Filter(condition, skip, take);
        }

        public List<EmployeeInfo> GetEmployeeTree()
        {
            List<EmployeeInfo> result = new List<EmployeeInfo>();
            ConditionSearchEmployeer condition = new ConditionSearchEmployeer(this.CurrentUser, string.Empty, string.Empty, string.Empty, string.Empty);
            var source = this.employeeBO.FilterTree(condition).ToList();
            result = BuilTreeEmployee(this.CurrentUser.Id, source);
            return result;
        }

        public EmployeeInfo GetById(int id)
        {
            return this.employeeBO.GetEmployeeInfo(id);
        }

        public EmployeeInfo Create(EmployeeInfo employeeInfo)
        {
            if (employeeInfo == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }
            employeeInfo.CompanyId = GetCompanyIdOfUser();
            employeeInfo.EmployeeId = this.CurrentUser.Id;
            return this.employeeBO.Create(employeeInfo);
        }

        public EmployeeInfo Update(int id, EmployeeInfo employeeInfo)
        {
            if (employeeInfo == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }
            employeeInfo.CompanyId = GetCompanyIdOfUser();
            employeeInfo.EmployeeId = this.CurrentUser.Id;
            return this.employeeBO.Update(id, employeeInfo);
        }


        public ResultCode Delete(int id)
        {
            return this.employeeBO.Delete(id);
        }


        private List<EmployeeInfo> BuilTreeEmployee(int employeeId, List<EmployeeInfo> source)
        {
            List<EmployeeInfo> result = new List<EmployeeInfo>();
            List<EmployeeInfo> childrenOfEmployee = source.Where(p => p.EmployeeId == employeeId).ToList();
            foreach (var item in childrenOfEmployee)
            {
                item.ChildrenEmployee = new List<EmployeeInfo>();
                item.ChildrenEmployee.AddRange(BuilTreeEmployee(item.Id, source));
                result.Add(item);
            }

            return result;
        }

        #endregion
    }
}