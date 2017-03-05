using System;
using System.Security.Cryptography;
using System.Text;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace pwd444
{
    [Activity(Label = "Password Generator", MainLauncher = true, Icon = "@drawable/safe")]
    public class MainActivity : Activity
    {
        EditText masterPwd;
        EditText domain;
        TextView result;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            masterPwd = FindViewById<EditText>(Resource.Id.txtMaster);
            domain = FindViewById<EditText>(Resource.Id.txtDomain);
            result = FindViewById<EditText>(Resource.Id.txtResult);

            inputChange();
        }

        protected override void OnPause()
        {
            base.OnPause();

            masterPwd.Text = "";
            domain.Text = ""; 
            result.Text = "";
        }

        protected override void OnStop()
        {
            base.OnStop();

            masterPwd.Text = "";
            domain.Text = "";
            result.Text = "";
        }
        protected override void OnRestart()
        {
            base.OnRestart();

            masterPwd.Text = "";
            domain.Text = "";
            result.Text = "";
        }

        void inputChange()
        {
            masterPwd.TextChanged += delegate
            {
                calculateMd5();
            };

            domain.TextChanged += delegate
            {
                calculateMd5();
            };
        }

        void calculateMd5()
        {
            string total = masterPwd.Text + ":" + domain.Text;
            total = CalculateMD5Hash(total);
            result.Text = total.Substring(0, 8).ToLower();
        }

        public string CalculateMD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString();
        }
    }
}

