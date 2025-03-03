using LagoVista.Client.Core;
using LagoVista.Core.Models.UIMetaData;

namespace ClientAPI
{
    interface IDataExamples
    {
        Task<ListResponse<string[]>> GetDeviceArchiveHistoryAsync(string deviceRepoId, string deviceId);
    }

    public class DataExamples : IDataExamples
    {
        private IRestClient _restClient;

        public DataExamples(IRestClient restClient)
        {
            _restClient = restClient ?? throw new ArgumentNullException(nameof(restClient));
        }

        public Task<ListResponse<string[]>> GetDeviceArchiveHistoryAsync(string deviceRepoId, string deviceId)
        {
            return _restClient.GetListResponseAsync<string[]>($"/api/device/${deviceRepoId}/archives/{deviceId}");
        }
    }
}
