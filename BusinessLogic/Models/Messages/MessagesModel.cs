namespace BusinessLogic.Models.Messages;

public class MessagesModel
{
    public Guid ID { get; set; }
    public Guid Organizer_ID { get; set; }
    public string Organizer_Name { get; set; }
    public Guid Participant_ID { get; set; }
    public string Participant_Name { get; set; }
    public Guid Event_ID { get; set; }
    public string Event_Name { get; set; }
    public string Message_Content { get; set; }

}