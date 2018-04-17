using Android.App;
using Android.OS;

namespace GraphFunction.Activities
{
    //Activity about
    [Activity(Label = "About", Icon = "@drawable/icon")]
    public class AboutActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.About);
        }
    }
}