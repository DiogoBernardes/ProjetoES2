using Frontend.Models.Event.Ticket;
using Frontend.Models.User;

namespace Frontend.Models.Event
{
    public class EventModel
    {
        public Guid ID { get; set; }
        public Guid Organizer_ID { get; set; }
        public Guid Category_ID { get; set; }
        public string Name { get; set; }
        public DateTime Date_Hour { get; set; }
        public string Localization { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public EventCategoryModel Category { get; set; }
        public string Organizer_Name { get; set; }
        public List<EventTicketModel> Tickets { get; set; }
        public UserModel organizer { get; set; }
       

    }
}
