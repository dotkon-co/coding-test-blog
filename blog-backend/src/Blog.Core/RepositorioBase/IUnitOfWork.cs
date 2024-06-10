namespace Blog.Core.RepositorioBase
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
