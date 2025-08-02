namespace Core.Options;

public class ConnectionStringOptions
{
    public const string sectionName = "ConnectionStrings";

    public string DefaultConnection { get; set; } = null!;
}
