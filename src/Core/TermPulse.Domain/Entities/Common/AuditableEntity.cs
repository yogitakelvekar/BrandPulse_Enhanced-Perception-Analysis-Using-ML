namespace TermPulse.Domain.Entities.Common
{
    public class AuditableEntity
    {
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
