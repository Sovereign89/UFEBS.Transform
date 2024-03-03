using UFEBS.Transformer;


namespace CheckUFEBSDll
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Transformer transformer = new Transformer();
            string inputFileName = "D:\\original.xml";
            string outputFileName = "D:\\canon.xml";
            TransformerStatic.SaveXMLToFile(inputFileName,outputFileName);
        }

        /*static void Main(string[] args)
        {
            Transformer transformer = new Transformer();
            string inputFileName = "D:\\original.xml";
            string result = transformer.TransformXML(inputFileName);

            Console.WriteLine("Result: " + result);
            Console.ReadLine();
        }*/
    }
}
