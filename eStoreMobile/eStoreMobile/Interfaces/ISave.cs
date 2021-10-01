using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace eStoreMobile.Interfaces
{
    public interface ISave
    {
        void Save(string filename, string contentType, MemoryStream stream);
    }

    ////move to android
    //public class SaveAndroid : ISave
    //{
    //    public void Save(string filename, string contentType, MemoryStream stream)
    //    {
    //        string exception = string.Empty;
    //        string root = null;
    //        if ( Android.OS.Environment.IsExternalStorageEmulated )
    //        {
    //            root = Android.OS.Environment.ExternalStorageDirectory.ToString ();
    //        }
    //        else
    //            root = System.Environment.GetFolderPath (System.Environment.SpecialFolder.MyDocuments);

    //        Java.IO.File myDir = new Java.IO.File (root + "/Syncfusion");
    //        myDir.Mkdir ();
    //        Java.IO.File file = new Java.IO.File (myDir, filename);
    //        if ( file.Exists () )
    //            file.Delete ();
    //        try
    //        {
    //            FileOutputStream outs = new FileOutputStream (file);
    //            outs.Write (stream.ToArray ());

    //            outs.Flush ();
    //            outs.Close ();
    //        }
    //        catch ( Exception e )
    //        {
    //            exception = e.ToString ();
    //        }
    //        if ( file.Exists () && contentType != "application/html" )
    //        {
    //            Android.Net.Uri path = Android.Net.Uri.FromFile (file);
    //            string extension = Android.Webkit.MimeTypeMap.GetFileExtensionFromUrl (Android.Net.Uri.FromFile (file).ToString ());
    //            string mimeType = Android.Webkit.MimeTypeMap.Singleton.GetMimeTypeFromExtension (extension);
    //            Intent intent = new Intent (Intent.ActionView);
    //            intent.SetDataAndType (path, mimeType);
    //            Forms.Context.StartActivity (Intent.CreateChooser (intent, "Choose App"));

    //        }
    //    }
    //}
    // //Move to ios
    //public class SaveIOS : ISave
    //{
    //    void ISave.Save(string filename, string contentType, MemoryStream stream)
    //    {
    //        string exception = string.Empty;
    //        string path = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
    //        string filePath = Path.Combine (path, filename);
    //        try
    //        {
    //            FileStream fileStream = File.Open (filePath, FileMode.Create);
    //            stream.Position = 0;
    //            stream.CopyTo (fileStream);
    //            fileStream.Flush ();
    //            fileStream.Close ();
    //        }
    //        catch ( Exception e )
    //        {
    //            exception = e.ToString ();
    //        }
    //        if ( contentType == "application/html" || exception != string.Empty )
    //            return;
    //        UIViewController currentController = UIApplication.SharedApplication.KeyWindow.RootViewController;
    //        while ( currentController.PresentedViewController != null )
    //            currentController = currentController.PresentedViewController;
    //        UIView currentView = currentController.View;
    //        QLPreviewController qlPreview = new QLPreviewController ();
    //        QLPreviewItem item = new QLPreviewItemBundle (filename, filePath);
    //        qlPreview.DataSource = new PreviewControllerDS (item);
    //        currentController.PresentViewController ((UIViewController) qlPreview, true, (Action) null);
    //    }
    //}

}

