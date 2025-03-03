using LagoVista.Client.Core;
using LagoVista.Core.Models.UIMetaData;
using LagoVista.MediaServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientAPI
{
    public interface IMediaExamples
    {
        Task<ListResponse<MediaLibrarySummary>> GetMediaLibrariesAsync();
        Task<ListResponse<MediaResourceSummary>> GetMediaResources(string libraryId);
        Task<ListResponse<MediaLibrarySummary>> GetMediaForLibraryAsync(string libraryId);
    }

    public class MediaExamples : IMediaExamples
    {
        private IRestClient _restClient;

        public MediaExamples(IRestClient restClient)
        {
            _restClient = restClient ?? throw new ArgumentNullException(nameof(restClient));
        }

        public async Task<ListResponse<MediaLibrarySummary>> GetMediaLibrariesAsync()
        {
            return await _restClient.GetListResponseAsync<MediaLibrarySummary>("/api/customer/media/libraries");
        }

        public async Task<ListResponse<MediaLibrarySummary>> GetMediaForLibraryAsync(string libraryId)
        {
            return await _restClient.GetListResponseAsync<MediaLibrarySummary>("/api/customer/media/libraries");
        }

        public async Task<ListResponse<MediaResourceSummary>> GetMediaResources(string libraryId)
        {
            return await _restClient.GetListResponseAsync<MediaResourceSummary>($"/api/media/library/{libraryId}/resources");
        }
    }
}
