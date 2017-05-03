using Xamarin.Forms;

namespace MainBook.Infrastructure.Resourses
{
    public static class MediaResoursesHelper
    {
        public static string GetMediaPath(string fileName)
        {
            if (Device.OS == TargetPlatform.WinPhone || Device.OS == TargetPlatform.Windows)
            {
                return $"Assets/{fileName}";
            }
            return fileName;
        }
    }
}
