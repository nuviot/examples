using LagoVista.Client.Core;
using LagoVista.Core.Models.UIMetaData;
using LagoVista.Core.Validation;
using LagoVista.IoT.DeviceManagement.Core.Models;

namespace ClientAPI
{
    public interface IDeviceExamples
    {
        Task<ListResponse<DeviceSummary>> GetDevicesForLocation(string locationId);
        Task<InvokeResult> UpdateDeviceAsync(Device device);
    }

    public class DeviceExamples
    {
        private IRestClient _restClient;

        public DeviceExamples(IRestClient restClient)
        {
            _restClient = restClient ?? throw new ArgumentNullException(nameof(restClient));
        }

        public Task<ListResponse<DeviceSummary>> GetDevicesForLocation(string locationId)
        {
            return _restClient.GetListResponseAsync<DeviceSummary>($"/api/customer/location/{locationId}/devices");
        }

        public Task<InvokeResult> UpdateDeviceAsync(Device device)
        {
            return _restClient.PutAsync($"/api/device/{device.DeviceRepository.Id}", device);
        }
    }
}
