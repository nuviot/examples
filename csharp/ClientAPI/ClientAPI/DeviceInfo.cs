using LagoVista.Core.IOC;
using LagoVista.Core.PlatformSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientAPI
{
    public class DeviceInfo : IDeviceInfo
    {
        public string DeviceUniqueId { get; private set; }

        public string DeviceType { get; private set; }

        public static void Register(string uniqueId)
        {
            var deviceInfo = new DeviceInfo();

            deviceInfo.DeviceType = "Windows - UWP";

            deviceInfo.DeviceUniqueId = uniqueId;

            SLWIOC.Register<IDeviceInfo>(deviceInfo);
        }
    }
}
