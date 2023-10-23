namespace Cobra.Server.Attributes
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
