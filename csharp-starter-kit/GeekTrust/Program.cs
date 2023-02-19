using System;
using System.IO;
namespace GeekTrust
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                string[] inputData = File.ReadAllLines(args[0]);
                foreach (var item in inputData)
                {
                    Command cmd = CommandFactory.Get(item);
                    Console.WriteLine(cmd.Execute());
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
    }
    
    
    

}
