namespace Asp_project.Areas.Admin.ViewModels.Advertisment
{
    public class AdvertismentVM
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string DiscountInfo { get; set; }
        public bool IsMain { get; set; }=false;
    }
}
