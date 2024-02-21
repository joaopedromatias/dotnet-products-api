using System.Net;
using System.Net.Mail;
using System.Runtime.Serialization;
using System.Timers;

// idealmente esse tipo de funcionalidade seria uma função serverless cron job com execução diária

class CheckProductsDate { 
    private static TimeOnly time = new TimeOnly(0, 0, 0, 0);
    public static void Warn(Object source, ElapsedEventArgs e) {
        List<Product> products = ProductService.List();
        var today = DateOnly.FromDateTime(DateTime.Now);
        

        var productsToWarn = new List<Product>();

        foreach (Product p in products) { 
            var difference = p.FinalDate.ToDateTime(time) - today.ToDateTime(time);   
            if (difference.Days < 30) { 
                productsToWarn.Add(p);
            }
        }

        if (productsToWarn.Count > 0) { 
            Notify(productsToWarn);
        }

    }

    public static void Setup() {
        int oneDayMs = 24 * 60 * 60 * 1000;

        var timer = new System.Timers.Timer(oneDayMs);

        timer.Elapsed += Warn;
        timer.Enabled = true;
        timer.AutoReset = true;
    }

    public static void Notify(List<Product> productsToWarn) {
        string smtpHost = ""; // seriam variáveis de ambiente na vida real
        string smtpPort = ""; // seriam variáveis de ambiente na vida real
        string smtpUser = ""; // seriam variáveis de ambiente na vida real
        string smtpPass = ""; // seriam variáveis de ambiente na vida real
        string destination = ""; // seriam variáveis de ambiente na vida real
        
        string subject = "Warn about some products date";
        string body = "";

        foreach (Product p in productsToWarn) { 
            body += $"Product {p.Name} will expire in {p.FinalDate.ToDateTime(time) - DateTime.Now} days!\n";
        }

        var mailMessage = new MailMessage(smtpUser, destination, subject, body);

        var smtpClient = new SmtpClient(smtpHost, int.Parse(smtpPort))
        {
            Credentials = new NetworkCredential(smtpUser, smtpPass),
            EnableSsl = true
        };

        try { 
            smtpClient.Send(mailMessage);
        } catch (Exception e) { 
            Console.WriteLine(e.Message);
        }
    }
}