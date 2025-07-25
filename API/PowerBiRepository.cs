using PowerBiEmbedder.Repository;

namespace PowerBiEmbedder.API
{
    public abstract class PowerBiRepository<T> : IRepositoryAPI<T> where T : class
    {
        protected IContextServicePowerBI PowerBiService { get; set; }
        protected PowerBiRepository(IContextServicePowerBI powerBiService) 
        {
            PowerBiService = powerBiService;
        }

        public virtual T? GetById(string id)
        {
            throw new NotImplementedException();
        }

        public virtual T? GetById(string id, string id2)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<T>? GetAll()
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<T>? GetAll(string id)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<T>? GetAll(string id, string id2)
        {
            throw new NotImplementedException();
        }
    }
}
