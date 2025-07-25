#region using
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System.Linq.Expressions;
#endregion

namespace PowerBiEmbedder.Repository
{
    public interface IDataMappingRepository<T> where T : Entity
    {
        T Find(Guid id);

        IEnumerable<T> FindAllWithFilter(params object[] args);
        T FindWithFilter(params object[] args);

        IEnumerable<T> FindFor(Expression<Func<T, bool>> predicate);

        IEnumerable<T> FindFor(QueryBase query);

        IEnumerable<T> FindAll();

        IEnumerable<T> FindAll(QueryExpression qe);

        IEnumerable<T> FindAllForDictionary(QueryExpression qe);

        T FindByName(string attributeName, string value);

        IEnumerable<T> FindByFieldStringType(string attributeName, string value);

        IEnumerable<T> FindByNameAndStatus(string attributeName, int statecode, int statuscode);

        void Save(T entity);

        void SaveWithOrganizationService(T entity);

        void Execute(OrganizationRequestCollection collection);

        void LoadProperty(T entity, string relationshipSchemaName);

        Guid Create(T entity);

        void Associate(T source, Relationship relationShip, IEnumerable<Entity> records);

        void Delete(T entity);

        SetStateResponse Disable(SetStateRequest setStateRequest);

        string GetOptionSetValueLabel(string fieldName, int optionSetValue);
    }
}
