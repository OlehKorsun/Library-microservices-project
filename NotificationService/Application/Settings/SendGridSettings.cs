namespace Application.Settings;

public class SendGridSettings
{
    public static string SectionName = "MongoDbSettings";
    
    public string ApiKey { get; set; } = string.Empty;
    public string FromEmail { get; set; } = string.Empty;
    public string FromName { get; set; } = string.Empty;
}