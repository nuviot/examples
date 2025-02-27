using LagoVista.Client.Core;
using LagoVista.Core.Models.UIMetaData;
using LagoVista.Core.Validation;
using LagoVista.IoT.Billing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientAPI
{
    public interface ICustomerExamples
    {
        Task<ListResponse<CustomerEntitySummary>> GetCustomersAsync();
        Task<InvokeResult> SetCustomerAsync(string customerId);
        Task<CustomerEntity> GetCurrentCustomer();
        Task<ListResponse<CustomerLocation>> GetCurrentCustomerLocationsAsync();
    }

    public class CustomerExamples : ICustomerExamples
    {
        private IRestClient _restClient;

        public CustomerExamples(IRestClient restClient)
        {
            _restClient = restClient ?? throw new ArgumentNullException(nameof(restClient));
        }

        public async Task<CustomerEntity> GetCurrentCustomer()
        {
            var response = await _restClient.GetAsync<CustomerEntity>("/api/customer/current");
            return response.Result;
        }

        public async Task<ListResponse<CustomerEntitySummary>> GetCustomersAsync()
        {
            var response = await _restClient.GetListResponseAsync<CustomerEntitySummary>("/api/customers");
            if (response.Successful)
            {
                foreach (var customer in response.Model)
                {
                    Console.WriteLine(customer.Id + " " + customer.Name);
                }
            }

            return response;
        }

        public async Task<InvokeResult> SetCustomerAsync(string customerId)
        {
            var result = await _restClient.GetAsync($"/api/auth/customer/{customerId}");
            if (result.Success)
                return InvokeResult.Success;

            return InvokeResult.FromError(result.ErrorMessage);
        }

        public async Task<ListResponse<CustomerLocation>> GetCurrentCustomerLocationsAsync()
        {
            return await _restClient.GetListResponseAsync<CustomerLocation>("/api/customer/locations");
        }
    }
}
