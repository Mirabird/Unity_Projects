using System;

namespace LinkedListExample
{
    class DoublyLinkedList<T>
    {
        private Node<T> head;
        private Node<T> tail;

        public void Add(T data)
        {
            Node<T> newNode = new Node<T>(data);

            if (head == null)
            {
                head = newNode;
                tail = newNode;
            }
            else
            {
                tail.Next = newNode;
                newNode.Previous = tail;
                tail = newNode;
            }
        }

        public void PrintForward()
        {
            Node<T> current = head;

            Console.Write("Прямой порядок: ");
            while (current != null)
            {
                Console.Write(current.Data + " ");
                current = current.Next;
            }
            Console.WriteLine();
        }

        public void PrintReverse()
        {
            Node<T> current = tail;

            Console.Write("Обратный порядок: ");
            while (current != null)
            {
                Console.Write(current.Data + " ");
                current = current.Previous;
            }
            Console.WriteLine();
        }
    }

    class Node<T>
    {
        public T Data { get; set; }
        public Node<T> Next { get; set; }
        public Node<T> Previous { get; set; }

        public Node(T data)
        {
            Data = data;
        }
    }

    class DoublyLinkedListTask
    {
        public void TaskLoop()
        {
            Console.WriteLine("Введите от 3 до 6 элементов для создания списка:");

            int numberOfElements;
            do
            {
                Console.Write("Количество элементов: ");
                numberOfElements = Convert.ToInt32(Console.ReadLine());

                if (numberOfElements >= 3 && numberOfElements <= 6)
                    break;

                Console.WriteLine("Некорректное количество элементов. Попробуйте снова или введите 'exit' для выхода.");

                string input = Console.ReadLine();
                if (input.ToLower() == "exit")
                    return;

            } while (true);

            DoublyLinkedList<int> list = new DoublyLinkedList<int>();

            for (int i = 0; i < numberOfElements; i++)
            {
                Console.Write("Элемент " + (i + 1) + ": ");
                int element = Convert.ToInt32(Console.ReadLine());
                list.Add(element);
            }

            list.PrintForward();
            list.PrintReverse();

            Console.ReadLine();
        }
    }
}
