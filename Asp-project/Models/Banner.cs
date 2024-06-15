namespace Asp_project.Models
{
    public class Banner : BaseEntity
    {
        public string Image { get; set; }
        public bool IsMain { get; set; } = false;
    }
}
