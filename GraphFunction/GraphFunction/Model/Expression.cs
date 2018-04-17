using System;
using System.Collections;

namespace GraphFunction.Model
{
    class Expression
    {
        private ArrayList My_Expression = new ArrayList();
        private ArrayList Postfix_Exp = new ArrayList();
        private Stack Stack_1 = new Stack();
        private Stack Stack_2 = new Stack();
        private string Exp;

        private enum type
        {
            variable,
            number,
            function,
            open_bracket,
            close_bracket,
            opera, /*operator*/
            constant
        }

        //Struct object
        private struct obj
        {
            public string name;
            public double value;
            public type t;
            public override String ToString()
            {
                return name;
            }
        }

        private string str
        {
            set
            {
                Exp = value;
            }
            get
            {
                return Exp;
            }
        }

        public Expression(string InputExpression)
        {
            Exp = InputExpression;
        }

        public bool IsNumber(char c)
        {
            if ((c >= '0') && (c <= '9'))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsCharacter(char c)
        {
            if (((c >= 'a') && (c <= 'z')) || ((c >= 'A') && (c <= 'Z')))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsOpenBracket(char c)
        {
            if ((c == '(') || (c == '[') || (c == '{'))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsCloseBracket(char c)
        {
            if ((c == ')') || (c == ']') || (c == '}'))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsOperator(char c)
        {
            if ((c == '+') || (c == '-') || (c == '*') || (c == '/') || (c == '^'))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Preferential when calculate
        private int Preferential(obj o)
        {
            switch (o.t)
            {
                case type.function:
                    return 4;
            }
            switch (o.name)
            {
                case "^":
                    return 3;
                case "*":
                    return 2;
                case "/":
                    return 2;
                case "+":
                    return 1;
                case "-":
                    return 1;
            }
            return -1;
        }

        //Calculate value of a, b and operator
        private double Calculate(double a, double b, string oper)
        {
            switch (oper)
            {
                case "^":
                    return Math.Pow(a, b);
                case "*":
                    return a * b;
                case "/":
                    {
                        if (b == 0)
                        {
                            return Double.NaN;
                        }
                        else
                        {
                            return a / b;
                        }
                    }
                case "+":
                    return a + b;
                case "-":
                    return a - b;
                default:
                    return Double.NaN;
            }
        }

        //Calculate value of a and function
        private double Funct(double a, string f)
        {
            switch (f)
            {
                case "sin":
                    return Math.Sin(a);
                case "cos":
                    return Math.Cos(a);
                case "tan":
                    {
                        if (Math.Cos(a) == 0)
                        {
                            return Double.NaN;
                        }
                        else
                        {
                            return Math.Tan(a);
                        }
                    }
                case "cot":
                    {
                        if (Math.Sin(a) == 0)
                        {
                            return Double.NaN;
                        }
                        else
                        {
                            return 1 / Math.Tan(a);
                        }
                    }
                case "sinh":
                    return Math.Sinh(a);
                case "cosh":
                    return Math.Cosh(a);
                case "tanh":
                    return Math.Tanh(a);
                case "ln":
                    {
                        if (a <= 0)
                        {
                            return Double.NaN;
                        }
                        else
                        {
                            return Math.Log(a);
                        }
                    }
                case "log":
                    {
                        if (a <= 0)
                        {
                            return Double.NaN;
                        }
                        else
                        {
                            return Math.Log10(a);
                        }
                    }
                case "abs":
                    return Math.Abs(a);
                case "exp":
                    return Math.Exp(a);
                case "sqrt":
                    {
                        if (a < 0)
                        {
                            return Double.NaN;
                        }
                        else
                        {
                            return Math.Sqrt(a);
                        }
                    }
                case "asin":
                    return Math.Asin(a);
                case "acos":
                    return Math.Acos(a);
                case "atan":
                    return Math.Atan(a);
                default:
                    return Double.NaN;
            }
        }

        //Convert input expression to char array
        public bool Convert_To_CharArray()
        {
            My_Expression.Clear();
            char[] ch = Exp.ToCharArray();
            for (int i = 0; i < ch.Length; i++)
            {
                if (IsNumber(ch[i]) == true)
                {
                    string s = ch[i].ToString();
                    i++;
                    while (i < ch.Length)
                    {
                        if ((IsNumber(ch[i]) == true) || (ch[i] == '.'))
                        {
                            s += ch[i].ToString();
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    i--;
                    obj o = new obj();
                    try
                    {
                        o.value = double.Parse(s);
                    }
                    catch (System.Exception ex)
                    {
                        My_Expression.Clear();
                        Console.WriteLine(ex);
                        return false;
                    }
                    o.name = s;
                    o.t = type.number;
                    My_Expression.Add(o);
                }
                else if (IsCharacter(ch[i]) == true)
                {
                    string s = ch[i].ToString();
                    i++;
                    while (i < ch.Length)
                    {
                        if (IsCharacter(ch[i]) == true)
                        {
                            s += ch[i].ToString();
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                    i--;
                    obj o = new obj();
                    o.name = s;
                    if (s.Length == 1)
                    {
                        if (s.Equals("x") == true)
                        {
                            o.t = type.variable;
                            o.value = 0;
                        }
                        if (s.Equals("e") == true)
                        {
                            o.t = type.constant;
                            o.value = Math.E;
                        }
                    }
                    else if (s.Length == 2)
                    {
                        if (s.Equals("ln") == true)
                        {
                            o.t = type.function;
                            o.value = 0;
                        }
                        if (s.Equals("pi") == true)
                        {
                            o.t = type.constant;
                            o.value = Math.PI;
                        }
                    }
                    else if (s.Length > 2)
                    {
                        o.t = type.function;
                        o.value = 0;
                    }
                    My_Expression.Add(o);
                }
                else if (IsOpenBracket(ch[i]) == true)
                {
                    obj o = new obj();
                    o.name = ch[i].ToString();
                    o.t = type.open_bracket;
                    o.value = 0;
                    My_Expression.Add(o);
                }
                else if (IsCloseBracket(ch[i]) == true)
                {
                    obj o = new obj();
                    o.name = ch[i].ToString();
                    o.t = type.close_bracket;
                    o.value = 0;
                    My_Expression.Add(o);
                }
                else if (IsOperator(ch[i]) == true)
                {
                    obj o = new obj();
                    o.name = ch[i].ToString();
                    o.t = type.opera;
                    o.value = 0;
                    My_Expression.Add(o);
                }
            }
            return true;
        }

        //Convert infix to postfix with Polish notation
        public bool Convert_Infix_To_Postfix()
        {
            Postfix_Exp.Clear();
            Stack_1.Clear();
            foreach (obj o in My_Expression)
            {
                if ((o.t == type.number) || (o.t == type.constant) || (o.t == type.variable))
                {
                    Postfix_Exp.Add(o);
                }
                else if (o.t == type.open_bracket)
                {
                    Stack_1.Push(o);
                }
                else if (o.t == type.close_bracket)
                {
                    if (Stack_1.Count > 0)
                    {
                        obj o1 = (obj)Stack_1.Pop();
                        while (o1.t != type.open_bracket)
                        {
                            Postfix_Exp.Add(o1);
                            if (Stack_1.Count == 0)
                            {
                                break;
                            }
                            o1 = (obj)Stack_1.Pop();
                        }
                    }
                }
                else if ((o.t == type.opera) || (o.t == type.function))
                {
                    if (Stack_1.Count > 0)
                    {
                        obj o1 = (obj)Stack_1.Pop();
                        while (Preferential(o1) >= Preferential(o))
                        {
                            Postfix_Exp.Add(o1);
                            if (Stack_1.Count == 0)
                            {
                                break;
                            }
                            o1 = (obj)Stack_1.Pop();
                        }
                        if (((o.t != type.opera) && (o.t != type.function)) || (Preferential(o1) < Preferential(o)))
                        {
                            Stack_1.Push(o1);
                        }
                    }
                    Stack_1.Push(o);
                }
            }
            if (Stack_1.Count > 0)
            {
                while (Stack_1.Count > 0)
                {
                    obj o2 = (obj)Stack_1.Pop();
                    Postfix_Exp.Add(o2);
                }
            }
            return true;
        }

        //Replace variable by value x
        private ArrayList Not_Variable(double x)
        {
            ArrayList array = new ArrayList();
            for (int i = 0; i < Postfix_Exp.Count; i++)
            {
                array.Add(Postfix_Exp[i]);
            }
            obj o1 = new obj();
            o1.name = x.ToString();
            o1.t = type.number;
            o1.value = x;
            for (int i = 0; i < array.Count; i++)
            {
                if (((obj)array[i]).t == type.variable)
                {
                    if (((obj)array[i]).name != "x")
                    {
                        return new ArrayList();
                    }
                    array[i] = o1;
                }
            }
            return array;
        }

        //Evaluate postfix expression with Polish notation
        public double Evaluate_Postfix(double x)
        {
            ArrayList Not_Var_Postfix_Exp = Not_Variable(x);
            if (Not_Var_Postfix_Exp.Count > 0)
            {
                foreach (obj o in Not_Var_Postfix_Exp)
                {
                    if ((o.t == type.number) || (o.t == type.constant))
                    {
                        Stack_2.Push(o.value);
                    }
                    else if (o.t == type.opera)
                    {
                        if (Stack_2.Count == 1)
                        {
                            double a = (double)Stack_2.Pop();
                            if (o.name.Equals("-") == true)
                            {
                                Stack_2.Push(-a);
                            }
                            else if (o.name.Equals("+") == true)
                            {
                                Stack_2.Push(a);
                            }
                        }
                        else if (Stack_2.Count >= 2)
                        {
                            double a1 = (double)Stack_2.Pop();
                            double a2 = (double)Stack_2.Pop();
                            double a3 = Calculate(a2, a1, o.name);
                            if (Double.IsNaN(a3) == false)
                            {
                                Stack_2.Push(a3);
                            }
                            else
                            {
                                return Double.NaN;
                            }
                        }
                    }
                    else if ((o.t == type.function) && (Stack_2.Count >= 1))
                    {
                        double a = (double)Stack_2.Pop();
                        double a1 = Funct(a, o.name);
                        if (Double.IsNaN(a1) == false)
                        {
                            Stack_2.Push(a1);
                        }
                        else
                        {
                            return Double.NaN;
                        }
                    }
                }
                if (Stack_2.Count == 1)
                {
                    double a = (double)Stack_2.Pop();
                    if (Double.IsNaN(a) == false)
                    {
                        return a;
                    }
                    else
                    {
                        return Double.NaN;
                    }
                }
                else
                {
                    return Double.NaN;
                }
            }
            else
            {
                return Double.NaN;
            }
        }
    }
}