using Contract.Business.Config;
using Contract.Business.Constants;
using Contract.Business.DAO;
using Contract.Business.Models;
using Contract.Common;
using Contract.Common.Extensions;
using Contract.Data.DBAccessor;
using System;
using System.Collections.Generic;
using Contract.Business.Extensions;
using System.Data;
using System.Linq;
using Contract.Common.Utils;

namespace Contract.Business.BL
{
    public class EmployeersBO : IEmployeeeBO
    {
        #region Fields, Properties

        private readonly IRepositoryFactory repoFactory;
        private readonly IEmployeesRepository employeeRepository;
        private readonly IDbTransactionManager transaction;
        private readonly PrintConfig printConfig;
        #endregion

        #region Contructor

        public EmployeersBO(IRepositoryFactory repoFactory, PrintConfig printConfig)
        {
            Ensure.Argument.NotNull(repoFactory, "repoFactory");
            this.transaction = repoFactory.GetRepository<IDbTransactionManager>();
            this.repoFactory = repoFactory;
            this.employeeRepository = repoFactory.GetRepository<IEmployeesRepository>();
            this.printConfig = printConfig;
        }

        #endregion

        #region Methods

        public EmployeeInfo GetEmployeeInfo(int id)
        {
            var curentEmployee = this.employeeRepository.GetById(id);
            if (curentEmployee == null)
            {
                throw new BusinessLogicException(ResultCode.NotFoundResourceId, MsgApiResponse.ResouceIdNotFound);
            }
            return new EmployeeInfo(curentEmployee);
        }

        public IEnumerable<EmployeeInfo> Filter(ConditionSearchEmployeer condition, int skip = int.MinValue, int take = int.MaxValue)
        {
            if (condition == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }

            return FilterEmployee(condition, skip, take);
        }

        public IEnumerable<EmployeeInfo> FilterTree(ConditionSearchEmployeer condition)
        {

            if (condition == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }

            return FilterTreeEmployee(condition);
        }

        public int CountFillter(ConditionSearchEmployeer condition)
        {
            if (condition == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }
            var employees = this.employeeRepository.Filter(condition).ToList();
            return employees.Count();
        }

        public EmployeeInfo Create(EmployeeInfo employeeInfo)
        {
            if (employeeInfo == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }

            ResultCode errorCode;
            string errorMessage;
            if (!employeeInfo.IsValid(out errorCode, out errorMessage))
            {
                throw new BusinessLogicException(errorCode, errorMessage);
            }

            Employee employeeNew = new Employee();
            employeeNew.CopyData(employeeInfo);
            employeeNew.EmployeeId = employeeInfo.EmployeeId;
            this.employeeRepository.Insert(employeeNew);
            return new EmployeeInfo(employeeNew);
        }



        public EmployeeInfo Update(int id, EmployeeInfo employeeInfo)
        {
            if (employeeInfo == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }

            ResultCode errorCode;
            string errorMessage;
            if (!employeeInfo.IsValid(out errorCode, out errorMessage))
            {
                throw new BusinessLogicException(errorCode, errorMessage);
            }

            Employee currentEmployee = GetEmployee(id);
            currentEmployee.CopyData(employeeInfo);
            this.employeeRepository.Update(currentEmployee);
            return new EmployeeInfo(currentEmployee);
        }

        public ResultCode Delete(int id)
        {
            var currentEployee = GetEmployee(id);
            currentEployee.Active = false;
            this.employeeRepository.Update(currentEployee);
            return ResultCode.NoError;
        }

        #endregion

        #region Private Method

        private Employee GetEmployee(int id)
        {
            var currentEmployeer = this.employeeRepository.GetById(id);
            if (currentEmployeer == null)
            {
                throw new BusinessLogicException(ResultCode.NotFoundResourceId, MsgApiResponse.DataNotFound);
            }

            return currentEmployeer;
        }

        private IEnumerable<EmployeeInfo> FilterEmployee(ConditionSearchEmployeer condition, int skip, int take)
        {
            var employees = this.employeeRepository.Filter(condition).AsQueryable()
                .OrderBy(condition.Order_By, condition.Order_Type.Equals(OrderType.Desc)).Skip(skip).Take(take).ToList();

            return employees.Select(p => new EmployeeInfo(p));
        }


        private IEnumerable<EmployeeInfo> FilterTreeEmployee(ConditionSearchEmployeer condition)
        {
            var employees = this.employeeRepository.Filter(condition).ToList();
            return employees.Select(p => new EmployeeInfo(p));
        }
        #endregion
              
    }
}