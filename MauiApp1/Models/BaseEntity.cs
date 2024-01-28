using SQLite;

namespace MauiApp1;

public class BaseEntity
{
    [PrimaryKey]
    [AutoIncrement]
    [Column("id")]
    public int Id { get; set; }
}
