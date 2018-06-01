using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using BiblioWeb.Services;

namespace BiblioWeb.Services
{
    public static class EmailSenderExtensions
    {
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendEmailAsync(email, "Confirme seu email",
                $"Por favor confirme sua conta clicando no link: <a href='{HtmlEncoder.Default.Encode(link)}'>Confirmar</a>");
        }
    }
}
