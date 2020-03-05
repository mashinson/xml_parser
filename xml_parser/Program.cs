using System;
using System.Xml;
using System.Xml.Linq;

namespace xml_parser
{
    class Program
    {
        static void Main(string[] args)
        {
            //XmlReader r = XmlReader.Create("books.xml");
            //while (r.NodeType != XmlNodeType.Element)
            //    r.Read();
            //XElement e = XElement.Load(r);

            string text = System.IO.File.ReadAllText("books.xml");
            Tree tree = new Tree();
            int f = 0;
            tree.RootNode = AddNodes(text, ref f);
            Console.ReadLine();
        }

        private static Node AddNodes(string text, ref int startNodeMainIndex)
        {
            //нашли тег 
            char startTagSymbol = '<';
            char EndTagSymbol = '>';
            char Slash = '/';
            int startIndex = text.IndexOf(startTagSymbol, startNodeMainIndex);
            if (startIndex == -1)
            {
                return null;
            }
            int endIndex = text.IndexOf(EndTagSymbol, startIndex);

            var tag = text.Substring(startIndex + 1, endIndex - startIndex - 1);

            var elements = tag.Split(' ');

            var node = new Node()
            {
                Title = elements[0],
                Properties = new System.Collections.Generic.Dictionary<string, string>(),
                Nodes = new System.Collections.Generic.List<Node>()
            };

            for (int i = 1; i < elements.Length; i++)
            {
                var values = elements[i].Split('=');
                node.Properties.Add(values[0], values[1]);
            }

            startNodeMainIndex = text.IndexOf(startTagSymbol, endIndex + 1);
            node.Value = text.Substring(endIndex + 1, startNodeMainIndex - endIndex - 2);

            while (text[startNodeMainIndex + 1] != Slash)
            {
                node.Nodes.Add(AddNodes(text, ref startNodeMainIndex));
            }
            startNodeMainIndex = text.IndexOf(EndTagSymbol, startNodeMainIndex);


            return node;
        }


    }
}
