using System.Threading.Tasks;
using Library.Application.Mailing.Models;

namespace Library.Application.Interfaces;

public interface IMailingService
{
    Task SendEmailAsync(Message message);
}