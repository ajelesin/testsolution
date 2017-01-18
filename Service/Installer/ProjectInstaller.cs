namespace Service.Installer
{
    using System.ComponentModel;
    using System.Configuration.Install;
    using System.ServiceProcess;

    [RunInstaller(true)]
    public class ProjectInstaller : Installer
    {
        public ProjectInstaller()
        {
            var process = new ServiceProcessInstaller {Account = ServiceAccount.LocalSystem};
            var service = new ServiceInstaller {ServiceName = "LineWindowsService"};

            Installers.Add(process);
            Installers.Add(service);
        }
    }
}
