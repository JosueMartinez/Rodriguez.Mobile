using System;
using System.IO;
using Rodriguez.Mobile.Droid;
using Rodriguez.Mobile.Services.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace Rodriguez.Mobile.Droid
{
    public class FileHelper : IFileHelper
    {
        public FileHelper()
        {
        }

        public string GetLocalFilePath(string filename)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}