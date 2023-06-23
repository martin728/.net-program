using System;
using System.Collections;
using System.Collections.Generic;
using Tasks.DoNotChange;

namespace Tasks
{
    public class DoublyLinkedList<T> : IDoublyLinkedList<T>
    {
        private Node<T> head;
        private Node<T> tail;
        public int Length { get; set; }

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

            Length++;
        }

        public void AddAt(int index, T e)
        {
            if (index < 0 || index > Length)
            {
                throw new IndexOutOfRangeException("Invalid position");
            }

            if (index == 0)
            {
                AddFirst(e);
            }
            else if (index == Length)
            {
                AddLast(e);
            }
            else
            {
                Node<T> newNode = new Node<T>(e);
                Node<T> current = head;

                for (int i = 0; i < index - 1; i++)
                {
                    current = current.Next;
                }

                newNode.Next = current.Next;
                newNode.Previous = current;
                current.Next.Previous = newNode;
                current.Next = newNode;

                Length++;
            }
        }

        public T ElementAt(int index)
        {
            Node<T> current = head;

            if (index < 0 || index >= Length)
            {
                throw new IndexOutOfRangeException("Invalid position");
            }

            for (int i = 0; i < index; i++)
            {
                current = current.Next;
            }

            return current.Data;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new DoubleListEnumerator(head);
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public void Remove(T item)
        {
            Node<T> currentNode = head;

            while (currentNode != null)
            {
                if (EqualityComparer<T>.Default.Equals(currentNode.Data, item))
                {
                    if (currentNode == head)
                    {
                        head = currentNode.Next;
                        if (head != null)
                        {
                            head.Previous = null;
                        }
                        else
                        {
                            tail = null;
                        }
                    }
                    else if (currentNode == tail)
                    {
                        tail = currentNode.Previous;
                        tail.Next = null;
                    }
                    else
                    {
                        currentNode.Previous.Next = currentNode.Next;
                        currentNode.Next.Previous = currentNode.Previous;
                    }

                    Length--;
                    return;
                }

                currentNode = currentNode.Next;
            }
        }

        public T RemoveAt(int index)
        {
            if (index < 0 || index >= Length)
            {
                throw new IndexOutOfRangeException("Invalid position");
            }

            Node<T> current = head;

            for (int i = 0; i < index; i++)
            {
                current = current.Next;
            }

            if (current == head)
            {
                head = current.Next;
                if (head != null)
                {
                    head.Previous = null;
                }
                else
                {
                    tail = null;
                }
            }
            else if (current == tail)
            {
                tail = current.Previous;
                tail.Next = null;
            }
            else
            {
                current.Previous.Next = current.Next;
                current.Next.Previous = current.Previous;
            }

            Length--;
            return current.Data;
        }
        private class Node<T>
        {
            public T Data { get; }
            public Node<T> Next { get; set; }
            public Node<T> Previous { get; set; }

            public Node(T data)
            {
                Data = data;
                Next = null;
                Previous = null;
            }
        }
        public void AddFirst(T value)
        {
            Node<T> newNode = new Node<T>(value);

            if (head == null)
            {
                head = newNode;
                tail = newNode;
            }
            else
            {
                newNode.Next = head;
                head.Previous = newNode;
                head = newNode;
            }

            Length++;
        }

        public void AddLast(T value)
        {
            Node<T> newNode = new Node<T>(value);

            if (tail == null)
            {
                head = newNode;
                tail = newNode;
            }
            else
            {
                newNode.Previous = tail;
                tail.Next = newNode;
                tail = newNode;
            }

            Length++;
        }
        private class DoubleListEnumerator : IEnumerator<T>
        {
            private Node<T> current;
            private Node<T> head;

            public DoubleListEnumerator(Node<T> head)
            {
                this.head = head;
                current = null;
            }

            public T Current => current.Data;

            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                if (current == null)
                {
                    current = head;
                }
                else
                {
                    current = current.Next;
                }

                return current != null;
            }

            public void Reset()
            {
                current = null;
            }

            public void Dispose()
            {
                // No resources to dispose
            }
        }
    }
    
}
