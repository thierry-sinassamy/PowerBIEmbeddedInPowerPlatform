#region using
using PowerBiEmbedder.BuildingObject;
using PowerBiEmbedder.ExecutionContext;
#endregion

namespace PowerBiEmbedder.ObjectFactory
{
    public class FactoryContextBuildingObject
    {
        public IAbstractFactory InstanciateBuildingObject(string key)
        {
            IAbstractFactory? instanciation = null;

            switch (key)
            {
                case ObjectTypeCode.account:
                    instanciation = new BuildingObjectAcc();
                    break;//etc...
                default:
                    break;
            }
            return instanciation;
        }
    }
}
