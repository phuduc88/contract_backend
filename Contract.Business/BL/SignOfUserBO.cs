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
    public class SignOfUserBO : ISignOfUserBO
    {
        #region Fields, Properties

        private readonly ISignOfUserRepository signOfUserRepository;
        #endregion

        #region Contructor

        public SignOfUserBO(IRepositoryFactory repoFactory)
        {
            Ensure.Argument.NotNull(repoFactory, "repoFactory");
            this.signOfUserRepository = repoFactory.GetRepository<ISignOfUserRepository>();
        }

        #endregion
       
        #region Methods
        public IEnumerable<SignOfUserInfo> Filter()
        {
            var signsOfUser = this.signOfUserRepository.Filter().ToList();
            return signsOfUser.Select(p => new SignOfUserInfo(p));
        }
        public SignOfUserInfo GetDetail(int id)
        {
            var signOfUser = this.signOfUserRepository.GetDetail(id);
            return new SignOfUserInfo(signOfUser);
        }

        public ResultCode Create(SignOfUserInfo signOfUser)
        {
            if (signOfUser == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }

            ResultCode errorCode;
            string errorMessage;
            if (!signOfUser.IsValid(out errorCode, out errorMessage))
            {
                throw new BusinessLogicException(errorCode, errorMessage);
            }

            SignOfUser signOfUserNew = new SignOfUser();
            signOfUserNew.CopyData(signOfUser);
            signOfUserNew.DateCreate = DateTime.Now;
            bool result = this.signOfUserRepository.Insert(signOfUserNew);
            return result ? ResultCode.NoError : ResultCode.UnknownError;

        }

        public ResultCode Update(int id, SignOfUserInfo signOfUser)
        {
            if (signOfUser == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }

            ResultCode errorCode;
            string errorMessage;
            if (!signOfUser.IsValid(out errorCode, out errorMessage))
            {
                throw new BusinessLogicException(errorCode, errorMessage);
            }

            SignOfUser currentSignOfUser = GetSignOfUser(id);
            currentSignOfUser.CopyData(signOfUser);
            bool result = this.signOfUserRepository.Update(currentSignOfUser);
            return result ? ResultCode.NoError : ResultCode.UnknownError;
        }

        public ResultCode UpdateUseDefault(int id, int companyId)
        {
            this.UpdateUseDefault(id);
            this.ResetUseDefaul(id, companyId);
            return ResultCode.NoError;
        }

        public SignOfUserInfo GetSignOfUseDefault(int userId)
        {
            SignOfUser currentSignOfUser = this.signOfUserRepository.GetSignOfUserDefault(userId);
            return new SignOfUserInfo(currentSignOfUser);
        }

        public ResultCode Delete(int id)
        {
            SignOfUser currentSignOfUser = GetSignOfUser(id);
            bool result = this.signOfUserRepository.Delete(currentSignOfUser);
            return result ? ResultCode.NoError : ResultCode.UnknownError;
        }
        #endregion

        private SignOfUser GetSignOfUser(int id)
        {
            SignOfUser currentSignOfUser = this.signOfUserRepository.GetDetail(id);
            if (currentSignOfUser == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, "Get SignOfUser fail width id");
            }

            return currentSignOfUser;
        }

        private void UpdateUseDefault(int id)
        {
            SignOfUser currentSignOfUser = GetSignOfUser(id);
            currentSignOfUser.UseDefault = true;
            this.signOfUserRepository.Update(currentSignOfUser);
        }

        private void ResetUseDefaul(int id, int companyId)
        {
            var signOfUsers = this.signOfUserRepository.FilterUseDefault(id, companyId).ToList();
            foreach (var item in signOfUsers)
            {
                item.UseDefault = false;
                this.signOfUserRepository.Update(item);
            }
        }
         
    }
}