using MainBook.Infrastructure.CommonData;
using MainBook.Infrastructure.DataManagers.LocalDbManager.Repositories.Implementations;
using MainBook.Infrastructure.DataManagers.LocalDbManager.Repositories.Interfaces;
using MainBook.Infrastructure.DependencyService;
using MainBook.Services;
using MainBook.Views.MasterDetailPage;
using Microsoft.Practices.Unity;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.WindowsSpecific;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MainBook
{
    public partial class App : Application
    {
        
        public static UnityContainer Container { get; set; }
        public static double DeviceHeight, DeviceWidth;
        public App()
        {
            InitializeComponent();
            ResolveDependency();
            MainPage = new MainPage();
            MainPage.On<Xamarin.Forms.PlatformConfiguration.Windows>().SetToolbarPlacement(ToolbarPlacement.Top);
        }

        private static void ResolveDependency()
        {
            Container = new UnityContainer();
            Container.RegisterType<IFactRepository, FactRepository>();
            Container.RegisterType<ISettingsRepository, SettingsRepository>();
            Container.RegisterType<IFactService, FactService>();
            var data = Container.Resolve(typeof(CommonData), "commonData") as CommonData;
        }
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
