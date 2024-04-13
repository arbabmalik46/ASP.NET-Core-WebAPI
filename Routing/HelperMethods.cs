namespace Routing
{
    public static class HelperMethods
    {
        public static string HelloWorld(HttpContext context)
        {
            return "Hello World";
        }
    }

    public class HelperClass
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
