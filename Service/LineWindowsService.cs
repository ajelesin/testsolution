namespace Service
{
    using System;
    using System.ServiceModel;
    using System.ServiceProcess;

    public class LineWindowsService : ServiceBase
    {
        private ServiceHost _serviceHost;

        public LineWindowsService()
        {
            ServiceName = "LineWindowsService";
        }

        protected override void OnStart(string[] args)
        {
            _serviceHost?.Close();

            _serviceHost = new ServiceHost(typeof(LineService));
            _serviceHost.Open();
        }

        protected override void OnStop()
        {
            _serviceHost?.Close();
            _serviceHost = null;
        }

        public static void Main(string[] args)
        {
            var service = new LineWindowsService();
            if (Environment.UserInteractive)
            {
                service.OnStart(args);
                Console.WriteLine("Press any key to stop the service...");
                Console.ReadKey();
                service.OnStop();

            }
            else
            {
                Run(new LineWindowsService());
            }
        }
    }
}
