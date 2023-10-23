namespace Cobra.Server.Edm.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class EdmEntityAttribute : Attribute
    {
        public string Name { get; set; }

        public EdmEntityAttribute(string name)
        {
            Name = name;
        }
    }
}
