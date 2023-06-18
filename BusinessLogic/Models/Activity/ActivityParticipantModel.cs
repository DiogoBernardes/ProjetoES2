namespace BusinessLogic.Models.Activity;

public class ActivityParticipantModel
{
    public Guid ID { get; set; }
    public Guid Activity_ID { get; set; }
    public string Activity_Name { get; set; }
    public Guid Participant_ID { get; set; }
    public string Participant_Name { get; set; }
}