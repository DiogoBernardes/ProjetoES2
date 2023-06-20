namespace Frontend.Models.Event.Ticket;

public class EditEventTicketModel
{
    public Guid ID { get; set; }
    public EventModel Event { get; set;  }
    public TicketTypeModel TicketType { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}