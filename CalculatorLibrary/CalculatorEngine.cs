using System;
using System.Collections.Generic;

namespace CalculatorLibrary
{
    public class CalculatorEngine
    {
        public double Evaluate(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
                throw new ArgumentException("пустое выражение");

            expression = expression.Replace(" ", "");
            int pos = 0;
            double result = ParseExpression(expression, ref pos);

            if (pos < expression.Length)
                throw new InvalidOperationException("ошибка в выражении");

            return result;
        }
        public double Calculate(double a, double b, string operation)
        {
            switch (operation)
            {
                case "+": return a + b;
                case "-": return a - b;
                case "*": return a * b;
                case "/":
                    if (b == 0)
                        throw new DivideByZeroException("деление на ноль невозможно!");
                    return a / b;
                case "^": return Math.Pow(a, b);
                default:
                    throw new InvalidOperationException("неизвестная операция!");
            }
        }

        // сложение и вычитание 
        private double ParseExpression(string s, ref int pos)
        {
            double left = ParseTerm(s, ref pos);

            while (pos < s.Length && (s[pos] == '+' || s[pos] == '-'))
            {
                char op = s[pos];
                pos++;
                double right = ParseTerm(s, ref pos);
                left = op == '+' ? left + right : left - right;
            }

            return left;
        }

        // умножение и деление
        private double ParseTerm(string s, ref int pos)
        {
            double left = ParsePower(s, ref pos);

            while (pos < s.Length && (s[pos] == '*' || s[pos] == '/'))
            {
                char op = s[pos];
                pos++;
                double right = ParsePower(s, ref pos);

                if (op == '/' && right == 0)
                    throw new DivideByZeroException("деление на ноль невозможно!");

                left = op == '*' ? left * right : left / right;
            }

            return left;
        }

        // возведение в степень
        private double ParsePower(string s, ref int pos)
        {
            double left = ParseUnary(s, ref pos);

            if (pos < s.Length && s[pos] == '^')
            {
                pos++;
                double right = ParsePower(s, ref pos); // рекурсия 
                left = Math.Pow(left, right);
            }

            return left;
        }

        // минус и плюс, скобки, числа
        private double ParseUnary(string s, ref int pos)
        {
            if (pos < s.Length && s[pos] == '-')
            {
                pos++;
                return -ParseUnary(s, ref pos);
            }

            if (pos < s.Length && s[pos] == '+')
            {
                pos++;
                return ParseUnary(s, ref pos);
            }

            return ParseAtom(s, ref pos);
        }

        // скобки и числа
        private double ParseAtom(string s, ref int pos)
        {
            if (pos >= s.Length)
                throw new InvalidOperationException("конец выражения");

            // скобка
            if (s[pos] == '(')
            {
                pos++;
                double value = ParseExpression(s, ref pos);

                if (pos >= s.Length || s[pos] != ')')
                    throw new InvalidOperationException("не закрыта скобка");

                pos++;
                return value;
            }

            // число
            int start = pos;

            while (pos < s.Length && (char.IsDigit(s[pos]) || s[pos] == '.'))
                pos++;

            if (start == pos)
                throw new InvalidOperationException($"ошибка на позиции {pos}");

            string number = s.Substring(start, pos - start);
            return double.Parse(number, System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}