namespace EshopModulith.Shared.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string name, object key)
            : base($"Entity  \"{name}\" - Key: {key} Was Not Found")
        {
        }
    }
}
