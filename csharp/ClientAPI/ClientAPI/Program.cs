using ClientAPI;
using LagoVista.Client.Core;
using LagoVista.Client.Core.Models;
using LagoVista.Client.Core.Net;
using LagoVista.Core.Authentication.Models;
using LagoVista.Core.Interfaces;
using LagoVista.Core.IOC;
using LagoVista.Core.Models.UIMetaData;
using LagoVista.Core.Models;
using LagoVista.Core.PlatformSupport;
using LagoVista.IoT.Logging.Loggers;
using LagoVista.UserAdmin.Interfaces;

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
SLWIOC.Register<ICustomerExamples, CustomerExamples>();
SLWIOC.Register<IMediaExamples, MediaExamples>();

LagoVista.Client.Core.Startup.Init(live, true);

var authClient = SLWIOC.Get<IAuthClient>();
var authManager = SLWIOC.Get<IAuthManager>();
var restClient = SLWIOC.Get<IRestClient>();

//await authManager.LoadAsync();
await authManager.LogoutAsync();
if (!authManager.IsAuthenticated || true)
{
    var emailAddress = Environment.GetEnvironmentVariable("NUVIOT_LOGIN_EMAIL");
    var password = Environment.GetEnvironmentVariable("NUVIOT_LOGIN_PASSWORD");

    if (String.IsNullOrEmpty(emailAddress) && args.Length >= 2)
        emailAddress = args[0];

    if (String.IsNullOrEmpty(password) && args.Length >= 2)
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

    var authResult = await authClient.LoginAsync(loginInfo);
    if (authResult.Successful)
    {
        authManager.AccessToken = authResult.Result.AccessToken;
        authManager.AccessTokenExpirationUTC = authResult.Result.AccessTokenExpiresUTC;
        authManager.RefreshToken = authResult.Result.RefreshToken;
        authManager.AppInstanceId = authResult.Result.AppInstanceId;
        authManager.RefreshTokenExpirationUTC = authResult.Result.RefreshTokenExpiresUTC;
        authManager.IsAuthenticated = true;

        var getUserResult = await restClient.GetAsync("/api/user", new CancellationTokenSource());
        if (getUserResult.Success)
        {
            authManager.User = getUserResult.DeserializeContent<DetailResponse<UserInfo>>().Model;
            await authManager.PersistAsync();
        }
        Console.WriteLine($"Success, Logged in as {authResult.Result.User.Text} in the {authResult.Result.Org.Text} organization");
    }
    else
    {
        Console.WriteLine($"Could not login: {authResult.ErrorMessage}");
        return;

    }
}

var customerExample = SLWIOC.Create<ICustomerExamples>();
await customerExample.GetCustomersAsync();

var mediaExample = SLWIOC.Create<IMediaExamples>();
await mediaExample.GetMediaLibrariesAsync();

