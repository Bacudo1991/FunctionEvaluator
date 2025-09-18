using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evaluator.Logic
{
    public class Stack<T>
    {
        private T[] _stack;
        private int _top;

        public Stack(int n)
        {
            N = n;
            _stack = new T[N];
        }

        public int N { get; }

        public bool IsEmpty => _top == 0;

        public bool IsFull => _top == N;

        public void Push(T item)
        {
            if (IsFull)
            {
                throw new Exception("Stack Full");
            }

            _stack[_top] = item;
            _top++;
        }

        public T Pop() 
        {
            if (IsEmpty)
            {
                throw new Exception("Stack Empty");
            }

            var item = _stack[_top];
            _top--;
            return item;
        }
    }
}
