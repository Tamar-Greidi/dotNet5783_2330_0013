// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

namespace Stage0
{
    partial class Program
    {
        public static void Main(string[] args)
        {
            welcome0013();
            welcome2330();
            Console.ReadKey();
        }

        static partial void welcome2330();

        private static void welcome0013()
        {
            Console.WriteLine("Enter your name: ");
            string name = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", name);
        }
    }
}