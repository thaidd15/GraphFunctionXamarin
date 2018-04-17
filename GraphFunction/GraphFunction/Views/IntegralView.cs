using Android.Content;
using Android.Views;
using Android.Graphics;

using GraphFunction.Model;

namespace GraphFunction.Views
{
    //Integral view
    public class IntegralView : View
    {
        private string lower, upper, iteration, expression;
        private double result;
        private Paint p;
        public IntegralView(Context context, string lower_input, string upper_input, string iteraction_input, string expression_input) : base(context)
        {
            lower = lower_input;
            upper = upper_input;
            iteration = iteraction_input;
            expression = expression_input;
            p = new Paint();
            p.StrokeWidth = 2;
            p.TextSize = 30f;
        }
        protected override void OnDraw(Canvas canvas)
        {
            Integral Inte = new Integral();
            result = Inte.Calculate_Integral(lower, upper, iteration, expression);
            canvas.DrawARGB(255, 255, 255, 192);
            canvas.DrawText("Integral of Function", 120, 70, p);
            canvas.DrawText("I = " + result.ToString(), 120, 130, p);
        }
    }
}