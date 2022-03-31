using DataLayer.Models;
using System.Threading.Tasks;

namespace BusinessLayer.IHubs
{
    public interface IChatHub
    {
        Task AllNotificationMessages();
        Task AllUser();
        Task SendTextMessage(Message message);


    }
}
