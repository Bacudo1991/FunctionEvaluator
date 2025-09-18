
namespace Evaluator.Logic
{
    public class MyEvaluator
    {
        public static double Evaluate(string infix)
        {
            var postfix = ToPostfix(infix);
            Console.WriteLine(postfix); 
            return Calculate(postfix);
        }

        private static double Calculate(string postfix)
        {
            return 0;
        }  
        private static string ToPostfix(string infix)
        {
            var stack = new Stack<char>(100);
                var postfix = string.Empty;
            for (int i = 0; i < infix.Length; i++)
            {
                if (IsOperator(infix[i]))
                {
                    if (stack.IsEmpty)
                    {
                        stack.Push(infix[i]);
                    }
                    else
                    {
                        if (infix[i] == ')')
                        {
                            do
                            {
                                postfix += stack.Pop(); 
                            } while (stack.GetItemInTop() != '(');
                            stack.Pop();
                        }
                        else 
                        {
                            if (PriorityInExpresion(infix[i]) > PriorityInStack(stack.GetItemInTop()))
                            {
                                stack.Push(infix[i]);
                            }
                            else
                            {
                                postfix += stack.Pop();
                                stack.Push(infix[i]);
                            }
                        }
                    }
                }   
                else
                {
                    postfix += infix[i];
                }
            }
            while (!stack.IsEmpty)
            {
                postfix += stack.Pop();
            }
            return postfix;
        }

        private static bool IsOperator(char item)
        {
            if (item == '(' || item == ')' || item == '^' || item == '*' || item == '/' || item == '+' || item == '-')
            {
                return true;
            }
            return false;
        }

        private static int PriorityInStack(char @operator)
        {
            switch (@operator)
            {
                case '^': return 3;
                case '*': return 2;
                case '/': return 2;
                case '+': return 1;
                case '-': return 1;
                case '(': return 0;
                default: throw new Exception("Invalid Operator");
            }
        }
        private static int PriorityInExpresion(char @operator)
        {
            switch (@operator)
            {
                case '^': return 4;
                case '*': return 2;
                case '/': return 2;
                case '+': return 1;
                case '-': return 1;
                case '(': return 5;
                default: throw new Exception("Invalid Operator");
            }
        }
    }
}
