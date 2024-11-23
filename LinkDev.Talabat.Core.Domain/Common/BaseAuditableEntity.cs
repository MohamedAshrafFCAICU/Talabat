namespace LinkDev.Talabat.Core.Domain.Common
{

    public abstract class BaseAuditableEntity<Tkey> : BaseEntity<Tkey>
        where Tkey : IEquatable<Tkey>
    {

        public string CreatedBy { get; set; } = null!;

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public string LastModifiedBy { get; set; } = null!;

        public DateTime? LastModifiedOn { get; set; } = DateTime.Now;

    }
}
