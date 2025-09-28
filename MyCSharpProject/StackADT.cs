using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCSharpProject
{
    public class StackADT
    {
        private Node top;
        private int size;

        public int Count
        {
            get { return size; }
        }

        public StackADT()
        {
            top = null;
            size = 0;
        }

        public bool IsEmpty()
        {
            return top == null;
        }

        public void Push(Bill value)
        {
            Node temp = new Node(value);
            temp.Next = top;
            top = temp;
            size++;
        }

        public Bill Pop()
        {
            if (top == null)
            {
                throw new Exception("Stack rỗng");
            }
            Node current = top;
            top = current.Next;
            Bill temp = current.Data;
            size--;
            return temp;
        }

        public Bill Peek()
        {
            if (IsEmpty())
            {
                throw new Exception("Stack rỗng");
            }
            return top.Data;
        }
    }
}
