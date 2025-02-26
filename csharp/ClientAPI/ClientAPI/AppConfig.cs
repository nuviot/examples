using LagoVista.Core.Interfaces;
using LagoVista.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientAPI
{
    public class AppConfig : IAppConfig
    {
        public PlatformTypes PlatformType => PlatformTypes.WebAPI;

        public Environments Environment => Environments.Production;

        public AuthTypes AuthType => AuthTypes.User;

        public EntityHeader SystemOwnerOrg => null;

        public string WebAddress => "";

        public string CompanyName => "";

        public string CompanySiteLink => "";

        public string AppName => "Client API Example";

        public string AppId => "{6EAE9868-40BA-47D0-863B-B6A25CA45C28}";

        public string APIToken => "";

        public string AppDescription => "Example to demonstrate calling web service.";

        public string TermsAndConditionsLink => "";

        public string PrivacyStatementLink => "";

        public string ClientType => "Console Application";

        public string AppLogo => "";

        public string CompanyLogo => "";

        public string InstanceId { get => ""; set  { } }
        public string InstanceAuthKey { get => ""; set  { } }
        public string DeviceId { get => "API-EXAMLE-APP"; set  { } }
        public string DeviceRepoId { get => ""; set  { } }

        public string DefaultDeviceLabel => "";

        public string DefaultDeviceLabelPlural => "";

        public bool EmitTestingCode => false;

        public VersionInfo Version => new VersionInfo() { Major = 1, Minor = 0 };

        public string AnalyticsKey { get => ""; set { } }
    }
}
