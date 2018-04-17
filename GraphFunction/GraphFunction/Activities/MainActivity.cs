using System;
using System.IO;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Graphics;

using GraphFunction.Views;

namespace GraphFunction.Activities
{
    //Activity main
    [Activity(Label = "GraphFunction", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private LinearLayout MainLayout;
        private GraphView GV;
        private IntegralView IV;
        private EditText edittext;
        private String str1, str2, Lowertxt, Uppertxt, Iterationtxt;
        private bool check = false; /*check graphview*/
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            MainLayout = FindViewById<LinearLayout>(Resource.Id.mainlayout);
            Button BtnDrawGraph = FindViewById<Button>(Resource.Id.buttondraw);
            Button BtnIntegral = FindViewById<Button>(Resource.Id.buttonintegral);
            BtnDrawGraph.Click += Draw;
            BtnIntegral.Click += Integral;
        }
        //Create option menu
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.OptionMenu, menu);
            return true;
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.savegraph:
                    SaveGraph();
                    return true;
                case Resource.Id.about:
                    About();
                    return true;
                default:
                    return false;
            }
        }
        private void Draw(object sender, EventArgs e)
        {
            str1 = FindViewById<EditText>(Resource.Id.function).Text;
            if (str1.Equals("") == true)
            {
                Toast.MakeText(this, "The Field y=f(x) Not Null", ToastLength.Short).Show();
                check = false;
            }
            else
            {
                str2 = str1.ToLower();
                MainLayout.RemoveView(IV);
                MainLayout.RemoveView(GV);
                GV = new GraphView(this, str2);
                MainLayout.AddView(GV);
                check = true;
            }
        }
        private void Integral(object sender, EventArgs e)
        {
            str1 = FindViewById<EditText>(Resource.Id.function).Text;
            Lowertxt = FindViewById<EditText>(Resource.Id.lower).Text;
            Uppertxt = FindViewById<EditText>(Resource.Id.upper).Text;
            Iterationtxt = FindViewById<EditText>(Resource.Id.iteration).Text;
            if (str1.Equals("") == true)
            {
                Toast.MakeText(this, "The Field y=f(x) Not Null", ToastLength.Short).Show();
            }
            else if (Lowertxt.Equals("") == true)
            {
                Toast.MakeText(this, "The Field Lower Not Null", ToastLength.Short).Show();
            }
            else if (Uppertxt.Equals("") == true)
            {
                Toast.MakeText(this, "The Field Upper Not Null", ToastLength.Short).Show();
            }
            else if (Iterationtxt.Equals("") == true)
            {
                Toast.MakeText(this, "The Field Iteration Not Null", ToastLength.Short).Show();
            }
            else
            {
                str2 = str1.ToLower();
                MainLayout.RemoveView(GV);
                MainLayout.RemoveView(IV);
                IV = new IntegralView(this, Lowertxt, Uppertxt, Iterationtxt, str2);
                MainLayout.AddView(IV);
                check = false;
            }
        }
        //Save graph to image
        void SaveGraph()
        {
            if (check == true)
            {
                LayoutInflater LI = LayoutInflater.From(this);
                View InputFileName = LI.Inflate(Resource.Layout.InputFileName, null);
                AlertDialog.Builder alertdialog = new AlertDialog.Builder(this);
                alertdialog.SetTitle("Save Graph");
                alertdialog.SetMessage("Image name");
                alertdialog.SetView(InputFileName);
                alertdialog.SetPositiveButton("Save", (senderalert, args) =>
                {
                    if (edittext.Text.Equals("") == true)
                    {
                        Toast.MakeText(this, "Enter Image Name", ToastLength.Short).Show();
                    }
                    else
                    {
                        Bitmap bitmap = Bitmap.CreateBitmap(GV.Width, GV.Height, Bitmap.Config.Argb8888);
                        Canvas canvas = new Canvas(bitmap);
                        GV.Draw(canvas);
                        string FileName = edittext.Text + ".png";
                        var SDCardPath = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
                        var FilePath = System.IO.Path.Combine(SDCardPath, FileName);
                        using (FileStream Stream = new FileStream(FilePath, FileMode.Create))
                        {
                            bitmap.Compress(Bitmap.CompressFormat.Png, 100, Stream);
                        }
                        Toast.MakeText(this, "Image Saved", ToastLength.Short).Show();
                    }
                });
                alertdialog.SetNegativeButton("Cancel", (senderalert, args) =>
                {
                    alertdialog.Dispose();
                });
                Dialog dialog = alertdialog.Create();
                dialog.Show();
                edittext = InputFileName.FindViewById<EditText>(Resource.Id.inputfilename);
            }
            else
            {
                Toast.MakeText(this, "Not Image To Save", ToastLength.Short).Show();
            }
        }
        void About()
        {
            Intent about = new Intent(this, typeof(AboutActivity));
            StartActivity(about);
        }
    }
}