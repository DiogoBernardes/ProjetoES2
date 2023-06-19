using BusinessLogic.Models.Event.ticket;
using BusinessLogic.Models.User;

namespace BusinessLogic.Models.Event;

public class EventModel
{
    public Guid ID { get; set; }
    public string Name { get; set; }
    public DateTime Date_Hour { get; set; }
    public string Localization { get; set; }
    public string Description { get; set; }
    public int Capacity { get; set; }
    public EventCategoryModel Category { get; set; }
    public UserModel Organizer { get; set; }

    public List<EventTicketModel> Tickets { get; set; }
}