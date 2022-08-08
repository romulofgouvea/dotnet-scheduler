using Flunt.Notifications;

namespace Extractor.Domain.Entities
{
    public abstract class BaseEntity : Notifiable<Notification>
    {
        public BaseEntity()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; internal set; }
        public bool? Enabled { get; internal set; }
        public DateTime? CreatedAt { get; internal set; }
        public DateTime? UpdatedAt { get; internal set; }
        public string CreatedBy { get; internal set; }
        public string UpdatedBy { get; internal set; }
    }
}
