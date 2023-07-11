namespace CrossCutting.DTOs
{
	public class EventCreationDto
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime StartDateTime { get; set; }
		public DateTime EndtDateTime { get; set; }
		public string Location { get; set; }
	}
}
