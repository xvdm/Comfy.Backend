namespace Comfy.Domain
{
    public class UserLog
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int LoggingActionId { get; set; }
        public Guid SubjectUserId { get; set; }

        public LoggingAction LoggingAction { get; set; } = null!;
        public User User { get; set; } = null!;
        public User SubjectUser { get; set; } = null!;
    }
}
