
using Evaluator.Logic;

Console.WriteLine("Expresions Evaluator");
var expresion1 = "4*5/(4+6)";
var expresion2 = "4*(5+6-(8/2^3)-7)-1";
var expresion3 = ("9^(1/2)");
Console.WriteLine($"{expresion1} = {MyEvaluator.Evaluate(expresion1)}");
Console.WriteLine($"{expresion2} = {MyEvaluator.Evaluate(expresion2)}");
Console.WriteLine($"{expresion3} = {MyEvaluator.Evaluate(expresion3)}");
