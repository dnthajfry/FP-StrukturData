using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FP_SD
{
    public class Stack<T>
    {
        public class Node
        {
            public int Data { get; }
            public DateTime Tanggal { get; }
            public Node Next { get; set; }

            public Node(int data, DateTime tanggal)
            {
                Data = data;
                Tanggal = tanggal;
                Next = null;
            }
        }
        private Node top;

        // Metode untuk menambahkan elemen ke dalam stack (Push)
        public void Push(int data, DateTime tanggal)
        {
            Node newNode = new Node(data, tanggal);
            newNode.Next = top;
            top = newNode;
        }


        // Metode untuk mendapatkan semua elemen dalam stack sebagai array
        public Node[] GetAllItems()
        {
            Node[] result = new Node[Count()];
            Node current = top;
            int index = 0;
            while (current != null)
            {
                result[index++] = current;
                current = current.Next;
            }
            return result;
        }

        // Metode untuk menghitung jumlah elemen dalam stack
        public int Count()
        {
            int count = 0;
            Node current = top;
            while (current != null)
            {
                count++;
                current = current.Next;
            }
            return count;
        }
    }

}
