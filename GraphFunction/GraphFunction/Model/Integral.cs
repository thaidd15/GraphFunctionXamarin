using System;

namespace GraphFunction.Model
{
    class Integral
    {
        double a, b, S, I, h, x;
        int n;
        public double Calculate_Integral(string lower_bound, string upper_bound, string iteration, string expression_text)
        {
            Expression exp = new Expression(expression_text);
            a = b = 0;
            n = int.Parse(iteration);

            //Lower bound
            char[] ch1 = lower_bound.ToCharArray();
            if (ch1[0] == '-')
            {
                if (exp.IsNumber(ch1[1]) == true)
                {
                    a = double.Parse(lower_bound);
                }
                if (exp.IsCharacter(ch1[1]) == true)
                {
                    if (lower_bound.Equals("-e") == true)
                    {
                        a = -Math.E;
                    }
                    else
                    {
                        a = -Math.PI;
                    }
                }
            }
            else if (exp.IsCharacter(ch1[0]) == true)
            {
                if (lower_bound.Equals("e") == true)
                {
                    a = Math.E;
                }
                else
                {
                    a = Math.PI;
                }
            }
            else
            {
                a = double.Parse(lower_bound);
            }

            //Upper bound
            char[] ch2 = upper_bound.ToCharArray();
            if (ch2[0] == '-')
            {
                if (exp.IsNumber(ch2[1]) == true)
                {
                    b = double.Parse(upper_bound);
                }
                if (exp.IsCharacter(ch2[1]) == true)
                {
                    if (upper_bound.Equals("-e") == true)
                    {
                        b = -Math.E;
                    }
                    else
                    {
                        b = -Math.PI;
                    }
                }
            }
            else if (exp.IsCharacter(ch2[0]) == true)
            {
                if (upper_bound.Equals("e") == true)
                {
                    b = Math.E;
                }
                else
                {
                    b = Math.PI;
                }
            }
            else
            {
                b = double.Parse(upper_bound);
            }

            if (exp.Convert_To_CharArray() == false)
            {
                return Double.NaN;
            }
            if (exp.Convert_Infix_To_Postfix() == false)
            {
                return Double.NaN;
            }
            
            x = a;
            S = (exp.Evaluate_Postfix(a) + exp.Evaluate_Postfix(b)) / 2;
            h = (b - a) / n;
            for (int i = 1; i <= n; i++)
            {
                x += h;
                S += exp.Evaluate_Postfix(x);
            }
            I = S * h;
            return Math.Round(I, 5, MidpointRounding.AwayFromZero);
        }
    }
}