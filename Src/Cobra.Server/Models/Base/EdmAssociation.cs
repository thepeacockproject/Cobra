namespace Cobra.Server.Models.Base
{
    public class EdmAssociation
    {
        public string Name { get; set; }
        public List<EdmEndRole> Ends { get; set; }
    }
}
