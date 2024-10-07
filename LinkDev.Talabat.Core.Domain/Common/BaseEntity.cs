namespace LinkDev.Talabat.Core.Domain.Common
{
    public class BaseEntity<Tkey> where Tkey : IEquatable<Tkey>
    {
        public required Tkey Id { get; set; }

        public required string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public required string LastModifiedBy { get; set; }

        public DateTime? LastModifiedOn { get; set; } = DateTime.Now;

    }
}
