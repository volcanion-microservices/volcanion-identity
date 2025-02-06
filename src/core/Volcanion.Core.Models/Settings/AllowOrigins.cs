namespace Volcanion.Core.Models.Settings;

public class AllowOrigins
{
    public string Environment { get; set; } = "Development";

    public AllowOriginData Data { get; set; } = new();
}

public class AllowOriginData
{
    public string Origins { get; set; } = "*";

    public string Methods { get; set; } = "*";

    public string Headers { get; set; } = "*";
}
