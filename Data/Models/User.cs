namespace DotBB.Data;

public class User
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string Username { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public bool IsSuperuser { get; set; }
    public DateTime LastLogin { get; set; } = DateTime.UtcNow;
    public ICollection<Thread> Threads { get; set; }
    public ICollection<Post> Posts { get; set; }
    public int PostCount => Threads?.Count ?? 0 + Posts?.Count ?? 0;
}
