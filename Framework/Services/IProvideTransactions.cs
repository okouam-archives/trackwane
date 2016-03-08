namespace Trackwane.Framework.Interfaces
{
    public interface IProvideTransactions
    {
        IUnitOfWork Begin();
    }
}