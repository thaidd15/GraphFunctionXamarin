using System;

using Android.Content;
using Android.Views;
using Android.Graphics;

using GraphFunction.Model;

namespace GraphFunction.Views
{
    //Graph view
    public class GraphView : View
    {
        private int m = 1;
        private int k = 30;
        private int x0, y0, xmax, ymax, min, max, x1, y1, x2, y2;
        private double x, dx, fx1, fx2;
        private string expression;
        private Paint p1, p2;
        public GraphView(Context context, string str) : base(context)
        {
            p1 = new Paint();
            p1.StrokeWidth = 2;
            p2 = new Paint();
            p2.StrokeWidth = 2;
            expression = str;
        }
        protected override void OnDraw(Canvas canvas)
        {
            canvas.DrawARGB(255, 255, 255, 192);
            xmax = canvas.Width;
            ymax = canvas.Height;
            x0 = xmax / 2;
            y0 = ymax / 2;
            min = -x0 / k;
            max = x0 / k;
            // Draw OXY
            p1.Color = Color.Black;
            canvas.DrawLine(x0, 0, x0, ymax, p1);
            canvas.DrawLine(0, y0, xmax, y0, p1);
            p1.Color = Color.Red;
            canvas.DrawText("O", x0 + 5, y0 + 15, p1);
            canvas.DrawText("X", xmax - 13, y0 + 15, p1);
            canvas.DrawText("Y", x0 + 5, 15, p1);
            p1.Color = Color.Gray;
            p2.Color = Color.Black;
            m = 1;
            for (int i = x0 + k; i < xmax; i += k)
            {
                canvas.DrawLine(i, 0, i, ymax, p1);
                canvas.DrawText(m.ToString(), i, y0 + 15, p2);
                m++;
            }
            m = 1;
            for (int i = x0 - k; i > 0; i -= k)
            {
                canvas.DrawLine(i, 0, i, ymax, p1);
                canvas.DrawText("-" + m.ToString(), i, y0 + 15, p2);
                m++;
            }
            m = 1;
            for (int i = y0 - k; i > 0; i -= k)
            {
                canvas.DrawLine(0, i, xmax, i, p1);
                canvas.DrawText(m.ToString(), x0 - 18, i, p2);
                m++;
            }
            m = 1;
            for (int i = y0 + k; i < ymax; i += k)
            {
                canvas.DrawLine(0, i, xmax, i, p1);
                canvas.DrawText("-" + m.ToString(), x0 - 22, i, p2);
                m++;
            }
            // Draw Graph
            p1.Color = Color.Blue;
            x = min;
            dx = 1.0f / k;
            fx1 = f(x);
            x1 = x0 + (int)(x * k);
            y1 = y0 - (int)(fx1 * k);
            while (x < max)
            {
                x += dx;
                fx2 = f(x);
                x2 = x0 + (int)(x * k);
                y2 = y0 - (int)(fx2 * k);
                try
                {
                    if (!((fx1 * fx2 < 0) && (Math.Abs((int)(fx1 - fx2)) > k)))
                    {
                        canvas.DrawLine(x1, y1, x2, y2, p1);
                    }
                }
                catch { }
                x1 = x2;
                y1 = y2;
                fx1 = fx2;
            }
        }
        public double f(double x)
        {
            double result;
            Expression ex = new Expression(expression);
            if (ex.Convert_To_CharArray() == false)
            {
                return Double.NaN;
            }
            if (ex.Convert_Infix_To_Postfix() == false)
            {
                return Double.NaN;
            }
            result = ex.Evaluate_Postfix(x);
            return result;
        }
    }
}