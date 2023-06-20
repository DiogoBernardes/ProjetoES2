namespace BusinessLogic.Models.Event.ticket;

public class EditEventTicketModel
{
    public Guid ID { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}