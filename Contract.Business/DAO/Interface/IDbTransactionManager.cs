namespace Contract.Business.DAO
{
    public interface IDbTransactionManager
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}
