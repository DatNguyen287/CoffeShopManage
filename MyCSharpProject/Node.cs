using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCSharpProject
{
    public class Node
    {
        public Bill Data;
        public Node Next;

        public Node(Bill value)
        {
            Data = value;
            Next = null;
        }
    }
}
