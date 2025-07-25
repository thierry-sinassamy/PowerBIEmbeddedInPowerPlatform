using Microsoft.Xrm.Sdk;

namespace PowerBiEmbedder.PowerPlatformProxy
{
    public class FormProxy
    {
        public Entity Entity;
        public FormProxy(Entity entity) => this.Entity = entity;

        public override string? ToString()
        {
            return this.Entity != null ? (string)this.Entity["name"] : base.ToString();
        }
    }
}
