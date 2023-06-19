namespace Frontend.Models.Event;

public class EventRegistModel
{
    public Guid ID { get; set; }
    public Guid Event_ID { get; set; }
    public string Event_Name { get; set; }
    public Guid Participant_ID { get; set; }
    public string Participant_Name { get; set; }
    public Guid State_ID { get; set; }
    public string State_Name { get; set; }
    public Guid Ticket_Type_ID { get; set; }
    public string Ticket_Type_Name { get; set; }
    public DateTime Regist_Date { get; set; }

    
}