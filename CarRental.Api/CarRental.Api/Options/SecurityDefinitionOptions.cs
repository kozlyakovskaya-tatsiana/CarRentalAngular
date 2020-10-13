namespace CarRental.Api.Options
{
    public class SecurityDefinitionOptions
    {
        public const string SectionName = "SecurityDefinitionOptions";

        public string SecurityDefinitionName { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }

        public string Scheme { get; set; }
    }
}
