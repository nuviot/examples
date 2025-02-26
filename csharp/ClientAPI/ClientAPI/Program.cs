using ClientAPI;
using LagoVista.Client.Core;
using LagoVista.Client.Core.Models;
using LagoVista.Client.Core.Net;
using LagoVista.Core.Authentication.Models;
using LagoVista.Core.Interfaces;
using LagoVista.Core.IOC;
using LagoVista.Core.Networking.Services;
using LagoVista.Core.PlatformSupport;
using LagoVista.IoT.Logging.Loggers;
using LagoVista.UserAdmin.Interfaces;
using RingCentral;

var appConfig = new AppConfig();

DeviceInfo.Register("uwpapp");

SLWIOC.RegisterSingleton<IStorageService>(new StorageService());

var live = new ServerInfo()
{
    RootUrl = "api.nuviot.com",
    SSL = true,
    Port = 443
};

var dev = new ServerInfo()
{
    RootUrl = "dev-api.nuviot.com",
    SSL = true,
    Port = 443
};


SLWIOC.Register<INetworkService>(new DefaultNetworkService());
SLWIOC.RegisterSingleton<ILogger>(new AdminLogger(new DebugWriter()));
SLWIOC.RegisterSingleton<IAppConfig>(appConfig);

LagoVista.Client.Core.Startup.Init(live, true);

var authClient = SLWIOC.Get<IAuthClient>();
var authManager = SLWIOC.Get<IAuthManager>();
var restClient = SLWIOC.Get<IRestClient>();

if (!authManager.IsAuthenticated)
{
    var emailAddress = Environment.GetEnvironmentVariable("NUVIOT_LOGIN_EMAIL");
    var password = Environment.GetEnvironmentVariable("NUVIOT_LOGIN_PASSWORD");

    if (String.IsNullOrEmpty(emailAddress))
        emailAddress = args[0];

    if (String.IsNullOrEmpty(password))
        password = args[1];

    if (String.IsNullOrEmpty(emailAddress) || String.IsNullOrEmpty(password))
    {
        Console.WriteLine("Usage: dotnet run <email> <password>");
        return;
    }

    var loginInfo = new AuthRequest()
    {
        AuthType = appConfig.AuthType,
        DeviceRepoId = appConfig.DeviceRepoId,
        AppId = appConfig.AppId,
        DeviceId = appConfig.DeviceId,
        ClientType = appConfig.ClientType,
        Email = emailAddress,
        Password = password,
        UserName = emailAddress,
        GrantType = "password"
    };

    var result = await authClient.LoginAsync(loginInfo);
    if (result.Successful)
    {
        Console.WriteLine($"Success, Logged in as {result.Result.User.Text}");
    }
    else
    {
        Console.WriteLine($"Could not login: {result.ErrorMessage}");

    }
}

//restClient.GetListResponseAsync<CustomerSu>

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
