﻿using Android.Content;
using ClaroClient2.Droid;
using ClaroClient2.Helpers;
using Xamarin.Forms;

[assembly: Dependency(typeof(PhoneCaller))]
namespace ClaroClient2.Droid
{
    public class PhoneCaller : IServiceCaller
    {
        //pin fran 2232
        public void MakeCall(string phone, string monto,string pin)
        {
            try
            {
                string Combinacion = $"*321*1*{phone}*{monto}*{pin}*1#";
                var uri = string.Empty;
                var ussd = string.Format("tel:{0}", Combinacion);
                foreach (char c in ussd.ToCharArray())
                {
                    if (c == '#')
                    {
                        uri += Android.Net.Uri.Encode("#");
                    }
                    else
                    {
                        uri += c;
                    }
                }
                var Uri = Android.Net.Uri.Parse(uri);
                var intent = new Intent(Intent.ActionCall, Uri);
                intent.SetFlags(ActivityFlags.NewTask);
                intent.AddFlags(ActivityFlags.GrantReadUriPermission);
                intent.AddFlags(ActivityFlags.GrantReadUriPermission);
                Android.App.Application.Context.StartActivity(intent);

            }
            catch (System.Exception )
            {
                throw;
            }
        }
    }
}