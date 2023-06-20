namespace Frontend.Models.Event.Ticket;

public class CreateEventTicketModel
{
    public Guid ticket_ID { get; set; }
    public Guid event_ID { get; set; }
    
    public int quantity { get; set; }
    public decimal Price { get; set; }
}