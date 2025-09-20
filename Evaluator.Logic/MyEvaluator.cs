
using System.Globalization;

namespace Evaluator.Logic;

public class MyEvaluator
{
    public static double Evaluate(string infix)
    {
        var postfix = ToPostfix(infix);
        return Calculate(postfix);
    }
    
    private static string ToPostfix(string infix)
    {
        var stack = new Stack<char>(100);
        var postfix = string.Empty;
        var numberBuffer = string.Empty;

        for (int i = 0; i < infix.Length; i++)
        {
            if (infix[i] == ' ')
                continue;

            if (char.IsDigit(infix[i]) || infix[i] == '.')
            {
                numberBuffer += infix[i];
            }
            else
            {
                if (!string.IsNullOrEmpty(numberBuffer))
                {
                    postfix += numberBuffer + ' ';
                    numberBuffer = string.Empty;
                }

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
                            } while (stack.Peek() != '(');
                            stack.Pop();
                        }
                        else
                        {
                            if (PriorityInExpresion(infix[i]) > PriorityInStack(stack.Peek()))
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
            }
        }
        if (!string.IsNullOrEmpty(numberBuffer))
        {
            postfix += numberBuffer + " ";
        }
        while (!stack.IsEmpty)
        {
            postfix += stack.Pop() + " ";
        }
        return postfix;
    }

    private static bool IsOperator(char item) => item is '^' or '/' or '*' 
        or '%' or '+' or '-' or '(' or ')';

    private static int PriorityInStack(char op) => op switch
    {
        '^' => 3,
        '*' or '/' => 2,
        '+' or '-' => 1,
        '(' => 0,
        _ => throw new Exception("Invalid Expresion.")
    };

    private static int PriorityInExpresion(char op) => op switch
    {
        '^' => 4,
        '*' or '/' or '%' => 2,
        '+' or '-' => 1,
        '(' => 5,
        _ => throw new Exception("Invalid Expresion.")
    };

    private static double Calculate(string postfix)
    {
        var stack = new Stack<double>(100);
        var numberBuffer = string.Empty;

        for (int i = 0; i < postfix.Length; i++)
        {
            if (char.IsDigit(postfix[i]) || postfix[i] == '.')
            {
                numberBuffer += postfix[i];
            }
            else if (postfix[i] == ' ')
            {
                if (!string.IsNullOrEmpty(numberBuffer))
                {
                    stack.Push(double.Parse(numberBuffer, CultureInfo.InvariantCulture));
                    numberBuffer = string.Empty;
                }
            }
            else if (IsOperator(postfix[i]))
            {
                var number2 = stack.Pop();
                var number1 = stack.Pop();
                var result = Calculate(number1, postfix[i], number2);
                stack.Push(result);
            }
        }
        if (!string.IsNullOrEmpty(numberBuffer))
        {
            stack.Push(double.Parse(numberBuffer, CultureInfo.InvariantCulture));
        }
        return stack.Pop();
    }

    private static double Calculate(double number1, char op, double number2) => op switch
    {
        '^' => Math.Pow(number1, number2),
        '*' => number1 * number2,
        '/' => number1 / number2,
        '+' => number1 + number2,
        '-' => number1 - number2,
        _ => throw new Exception("Invalid Operator"),
    };
}