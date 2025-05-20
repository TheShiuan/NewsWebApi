namespace NewsWebAPI.Dtos
{
    public class NewsGetDto
    {
        public Guid NewsId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid InsertEnployId { get; set; }
        public Guid UpdateEnployId { get; set; }
        public DateTime InsertDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public int Click { get; set; }
    }
}
