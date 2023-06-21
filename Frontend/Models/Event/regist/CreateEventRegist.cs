namespace Frontend.Models.Event.regist; 

public class CreateEventRegist {

    public Guid event_ID { get; set; }
    public Guid participant_ID { get; set; }
    public Guid state_ID { get; set; }
    public Guid ticket_type_ID { get; set; }

}