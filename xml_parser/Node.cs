using System;
using System.Collections.Generic;
using System.Text;

namespace xml_parser
{
    public class Node
    {
        public string Title { get; set; }
        public string Value { get; set; }
        public Dictionary<string, string> Properties { get; set; }
        public List<Node> Nodes { get; set; }
    }
}
