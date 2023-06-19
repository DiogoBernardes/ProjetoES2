namespace BusinessLogic.Models.Event.ticket;

public class EventTicketModel
{
    public Guid ID { get; set; }
    public Guid TicketType_ID { get; set; }
    public TicketTypeModel TicketType { get; set; }
    public Guid Event_ID { get; set; }
    public string Event_name { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }

}