using Blog.Core.EntidadeBase;

namespace Blog.Core.RepositorioBase
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
