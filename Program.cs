using System;

namespace IntegralCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the expression in reverse polish notation separated by spaces: ");
            string[] expression = Console.ReadLine().Split();

            Console.WriteLine("Enter the lower bound of the integral: ");
            double lowerBound = double.Parse(Console.ReadLine());
            Console.WriteLine("Enter the upper bound of the integral: ");
            double upperBound = double.Parse(Console.ReadLine());
            int numSubintervals = 100;

            // Calculate the integral using the Trapezoidal method
            double trapezoidalResult = TrapezoidalMethod(expression, lowerBound, upperBound, numSubintervals);
            Console.WriteLine("The result of the integral using the Trapezoidal method is: " + trapezoidalResult);

            // Calculate the integral using Simpson's method
            double simpsonResult = SimpsonMethod(expression, lowerBound, upperBound, numSubintervals);
            Console.WriteLine("The result of the integral using Simpson's method is: " + simpsonResult);
        }

        static double TrapezoidalMethod(string[] expression, double lowerBound, double upperBound, int numSubintervals)
        {
            double h = (upperBound - lowerBound) / numSubintervals;
            double sum = 0;

            for (int i = 0; i <= numSubintervals - 1; i++)
            {
                double x_i = lowerBound + i * h;
                double x_i_plus_1 = x_i + h;
                sum += EvaluateExpression(expression, x_i) + EvaluateExpression(expression, x_i_plus_1);
            }

            return h * sum / 2;
        }

        static double SimpsonMethod(string[] expression, double lowerBound, double upperBound, int numSubintervals)
        {
            double h = (upperBound - lowerBound) / (2 * numSubintervals);
            double sum = 0;

            for (int i = 0; i <= numSubintervals - 1; i++)
            {
                double xi = lowerBound + (2 * i * h);
                sum += EvaluateExpression(expression, xi) + 4 * EvaluateExpression(expression, xi + h) + EvaluateExpression(expression, xi + 2 * h);
            }

            return sum * h / 3;
        }

        static double EvaluateExpression(string[] expression, double x)
        {
            Stack<double> operands = new Stack<double>();
            Stack<string> operators = new Stack<string>();

            foreach (string token in expression)
            {
                if (token == "+" || token == "-")
                {
                    while (operators.Count > 0 && (operators.Peek() == "+" || operators.Peek() == "-" || operators.Peek() == "*" || operators.Peek() == "/" || operators.Peek() == "^"))
                    {
                        Evaluate(operands, operators);
                    }
                    operators.Push(token);
                }
                else if (token == "*" || token == "/")
                {
                    while (operators.Count > 0 && (operators.Peek() == "*" || operators.Peek() == "/" || operators.Peek() == "^"))
                    {
                        Evaluate(operands, operators);
                    }
                    operators.Push(token);
                }
                else if (token == "^")
                {
                    while (operators.Count > 0 && operators.Peek() == "^")
                    {
                        Evaluate(operands, operators);
                    }
                    operators.Push(token);
                }
                else if (token == "sin")
                {
                    operators.Push(token);
                }
                else if (token == "cos")
                {
                    operators.Push(token);
                }
                else if (token == "tan")
                {
                    operators.Push(token);
                }
                else if (token == "ln")
                {
                    operators.Push(token);
                }
                else if (token == "x")
                {
                    operands.Push(x);
                }
                else
                {
                    operands.Push(double.Parse(token));
                }
            }

            while (operators.Count > 0)
            {
                Evaluate(operands, operators);
            }

            return operands.Pop();
        }
        private static void Evaluate(Stack<double> operands, Stack<string> operators)
        {
            double rightOperand = operands.Pop();
            double leftOperand = operands.Pop();

            switch (operators.Pop())
            {
                case "+":
                    operands.Push(leftOperand + rightOperand);
                    break;
                case "-":
                    operands.Push(leftOperand - rightOperand);
                    break;
                case "*":
                    operands.Push(leftOperand * rightOperand);
                    break;
                case "/":
                    operands.Push(leftOperand / rightOperand);
                    break;
                case "^":
                    operands.Push(Math.Pow(leftOperand, rightOperand));
                    break;
                case "sin":
                    operands.Push(Math.Sin(rightOperand));
                    break;
                case "cos":
                    operands.Push(Math.Cos(rightOperand));
                    break;
                case "tan":
                    operands.Push(Math.Tan(rightOperand));
                    break;
                case "ln":
                    operands.Push(Math.Log(rightOperand));
                    break;
            }
        }
    }
}