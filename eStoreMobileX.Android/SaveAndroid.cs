using Android.Content;
using AndroidX.Core.Content;
using eStoreMobileX.Droid;

//using Android.Support.V4.Content;
using Java.IO;
using System;
using System.IO;
using Xamarin.Forms;

//using Android.OS;
//using Android.Runtime;

[assembly: Dependency (typeof (SaveAndroid))]

namespace eStoreMobileX.Droid
{
    /// <summary>
    ///  A dependency service to save a exported file in Android
    /// </summary>
    //[SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1400:AccessModifierMustBeDeclared", Justification = "Reviewed.")]
    internal class SaveAndroid : ISave
    {
        /// <summary>
        /// Used to save a Exporting grid to Excel and PDF file in Android devices from Interface of ISAVE
        /// </summary>
        /// <param name="fileName">string type of filename parameter</param>
        /// <param name="contentType">string type of contentType parameter</param>
        /// <param name="stream">MemoryStream type of stream parameter</param>
        public void Save(string fileName, string contentType, MemoryStream stream)
        {
            string exception = string.Empty;
            string root = null;
            if ( Android.OS.Environment.IsExternalStorageEmulated )
            {
                root = Android.OS.Environment.ExternalStorageDirectory.ToString ();
            }
            else
            {
                root = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);
            }

            Java.IO.File myDir = new Java.IO.File (root + "/Syncfusion");
            myDir.Mkdir ();

            Java.IO.File file = new Java.IO.File (myDir, fileName);

            if ( file.Exists () )
            {
                file.Delete ();
            }

            try
            {
                FileOutputStream outs = new FileOutputStream (file);
                outs.Write (stream.ToArray ());

                outs.Flush ();
                outs.Close ();
            }
            catch ( Exception e )
            {
                exception = e.ToString ();
            }

            if ( file.Exists () && contentType != "application/html" )
            {
                string extension = Android.Webkit.MimeTypeMap.GetFileExtensionFromUrl (Android.Net.Uri.FromFile (file).ToString ());
                string mimeType = Android.Webkit.MimeTypeMap.Singleton.GetMimeTypeFromExtension (extension);
                Intent intent = new Intent (Intent.ActionView);
                intent.SetFlags (ActivityFlags.ClearTop | ActivityFlags.NewTask);

                //// Forms.Context is obsolete, Hence used local context

                Android.Net.Uri path = FileProvider.GetUriForFile (Android.App.Application.Context, Android.App.Application.Context.PackageName + ".provider", file);
                intent.SetDataAndType (path, mimeType);
                intent.AddFlags (ActivityFlags.GrantReadUriPermission);
                Intent chooserIntent = Intent.CreateChooser (intent, "Open With");
                chooserIntent.AddFlags (ActivityFlags.NewTask);
                Android.App.Application.Context.StartActivity (chooserIntent);
            }
        }
    }
}