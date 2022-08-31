namespace WebApplication1
{
    public class File
    {
        public Guid FileId { get; set; }
        public string FileName { get; set; }
        public Boolean IsPublic { get; set; } = false;
        public string ContentType { get; set; }
        public string CreatedOn { get; set; }
        public string ModifiedOn { get; set; }
    }
}
