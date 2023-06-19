namespace Frontend.Models.Event.Ticket;

public class EventTicketModel
{
    public Guid ID { get; set; }
    public TicketTypeModel TicketType { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }

}