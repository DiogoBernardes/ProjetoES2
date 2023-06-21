namespace BusinessLogic.Models.Event.regist; 

public class CreateEventRegistModal {

    public Guid Event_ID { get; set; }
    public Guid Participant_ID { get; set; }
    public Guid State_ID { get; set; }
    public Guid Ticket_Type_ID { get; set; }
    
    public DateTime regist_date { get; set; }

    

}