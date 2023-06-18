namespace BusinessLogic.Models.Activity;

public class ActivityModel
{
    public Guid ID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid Event_ID { get; set; }
    public string Event_Name { get; set; }
}