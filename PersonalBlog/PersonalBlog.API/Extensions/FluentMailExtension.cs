using System.Net;
using System.Net.Mail;

namespace PersonalBlog.API.Extensions;

public static class FluentMailExtension
{
    public static void AddFluentEmail(this IServiceCollection services, IConfiguration configuration)
    {
        SmtpClient client = new SmtpClient();
        var emailSettings = configuration.GetSection("EmailSettings");
        client.Credentials = new NetworkCredential(emailSettings["DefaultFromEmail"], emailSettings["Password"]);
        client.Host = emailSettings["SMTPSetting:Host"]!;
        client.Port = 587;
        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        client.EnableSsl = true;
        client.UseDefaultCredentials = false;

        var defaultFromEmail = emailSettings["DefaultFromEmail"];
        services.AddFluentEmail(defaultFromEmail)
            .AddSmtpSender(client)
            .AddRazorRenderer();
    }
}