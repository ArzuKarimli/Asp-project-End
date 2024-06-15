namespace Asp_project.Models
{
    public class Advertisment :  BaseEntity
    {
        public string Image { get; set; }
        public string? Title { get; set; }
        public string? DiscountInfo { get; set; }
        public bool IsMain { get; set; } = false;
    }
}
