using System;

namespace Trackwane.Framework.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository GetRepository();

        void Commit();
    }
}