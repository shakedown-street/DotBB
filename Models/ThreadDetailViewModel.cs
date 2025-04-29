using DotBB.Data;

namespace DotBB.Models;

public class ThreadDetailsViewModel
{
    public Data.Thread Thread { get; set; }
    public User User { get; set; }
    public Subcategory Subcategory { get; set; }
}
