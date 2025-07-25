#region using
using Microsoft.Crm.Sdk.Messages;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System.Linq.Expressions;
#endregion

namespace PowerBiEmbedder.Repository
{
    public abstract class DataverseDataMappingRepository<T> : IDataMappingRepository<T> where T : Entity
    {
        protected ServiceClient ServiceClientDataverse { get; set; }
        protected CrmServiceClient CrmServiceClientDataverse { get; set; }

        protected DataverseDataMappingRepository(IContextServiceDataMappingPowerPlatform context)
        {
            ServiceClientDataverse = context.ServiceClientDataverse;
            CrmServiceClientDataverse = context.CrmServiceClientDataverse;
        }
        public virtual void Associate(T source, Relationship relationShip, IEnumerable<Entity> records)
        {
            throw new NotImplementedException();
        }

        public virtual Guid Create(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual SetStateResponse Disable(SetStateRequest setStateRequest)
        {
            throw new NotImplementedException();
        }

        public virtual void Execute(OrganizationRequestCollection collection)
        {
            throw new NotImplementedException();
        }

        public virtual T Find(Guid id)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<T> FindAll()
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<T>? FindAll(QueryExpression qe)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<T> FindAllForDictionary(QueryExpression qe)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<T> FindAllWithFilter(params object[] args)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<T> FindByFieldStringType(string attributeName, string value)
        {
            throw new NotImplementedException();
        }

        public virtual T FindByName(string attributeName, string value)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<T> FindByNameAndStatus(string attributeName, int statecode, int statuscode)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<T> FindFor(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<T> FindFor(QueryBase query)
        {
            throw new NotImplementedException();
        }

        public virtual T FindWithFilter(params object[] args)
        {
            throw new NotImplementedException();
        }

        public virtual string GetOptionSetValueLabel(string fieldName, int optionSetValue)
        {
            throw new NotImplementedException();
        }

        public virtual void LoadProperty(T entity, string relationshipSchemaName)
        {
            throw new NotImplementedException();
        }

        public virtual void Save(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual void SaveWithOrganizationService(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
