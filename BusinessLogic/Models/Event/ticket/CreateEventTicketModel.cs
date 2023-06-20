namespace BusinessLogic.Models.Event.ticket;

public class CreateEventTicketModel
{
    public Guid ID { get; set; }
    public Guid ticket_ID { get; set; }
    public Guid event_ID { get; set; }
    
    public int quantity { get; set; }
    public decimal Price { get; set; }
}