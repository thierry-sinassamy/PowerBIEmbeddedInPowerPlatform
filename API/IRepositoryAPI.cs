
namespace PowerBiEmbedder.API
{
    public interface IRepositoryAPI<T> where T : class
    {
        T? GetById(string id);
        T? GetById(string id, string id2);
        IEnumerable<T>? GetAll();
        IEnumerable<T>? GetAll(string id);
        IEnumerable<T>? GetAll(string id, string id2);
    }
}
