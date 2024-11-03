using Microsoft.Extensions.Configuration;

namespace Solimus.Application.Models.Options;

public class EmailOptions(IConfiguration configuration)
{
    public string MailServer { get; } = configuration["EmailOptions:MailServer"]!;
    public string FromEmail { get; } = configuration["EmailOptions:FromEmail"]!;
    public string PrivateKey { get; } = configuration["EmailOptions:ApiKey"]!;
    public int Port { get; } = int.Parse(configuration["EmailOptions:Port"]!);
}
