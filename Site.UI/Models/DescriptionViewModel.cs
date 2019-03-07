namespace Site.UI.Models
{
    public class DescriptionViewModel
    {
        public DescriptionViewModel(string description)
        {
            Description = description;
        }

        public string Description { get; set; }
    }
}