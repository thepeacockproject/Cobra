namespace Cobra.Server.Edm.Models.Base
{
    public class EdmAssociation
    {
        public string Name { get; set; }
        public List<EdmEndRole> Ends { get; set; }
    }
}
