namespace Frontend.Models.Event;

public class CreateEventModel
{
    public Guid Organizer_ID { get; set; }
    public Guid Category_ID { get; set; }
    public string Name { get; set; }
    public DateTime Date_Hour { get; set; }
    public string Localization { get; set; }
    public string Description { get; set; }
    public int Capacity { get; set; } 
   
}    


