namespace LinkDev.Talabat.Core.Domain.Common
{
    public abstract class BaseAuditableEntity<Tkey> : BaseEntity<Tkey>
        where Tkey : IEquatable<Tkey>
    {

        public required string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public required string LastModifiedBy { get; set; }

        public DateTime? LastModifiedOn { get; set; } = DateTime.Now;

    }
}
