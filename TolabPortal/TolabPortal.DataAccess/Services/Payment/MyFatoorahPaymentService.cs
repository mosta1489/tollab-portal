using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tolab.Common;
using TolabPortal.DataAccess.Models.Payment;

namespace TolabPortal.DataAccess.Services.Payment
{
    public class MyFatoorahPaymentService : IMyFatoorahPaymentService
    {
        private readonly IMyFatoorahClient _client;
        private readonly ILogger<MyFatoorahPaymentService> logger;
        private readonly HttpClient _httpClient;
        
        public MyFatoorahPaymentService(IMyFatoorahClient client, IOptions<ApplicationConfig> options, ILogger<MyFatoorahPaymentService>logger)
        {
            _client = client;
            this.logger = logger;
            var config = options.Value;
            _httpClient = new HttpClient { BaseAddress = new Uri(config.ApiUrl) };
        }
        public async Task<GenericResponse<InitiatePaymentResponse>> InitiatePayment(InitiatePaymentRequest intiatePaymentRequest)
        {

            var intitateRequestJSON = JsonConvert.SerializeObject(intiatePaymentRequest);
            var response = await _client.PerformRequest(intitateRequestJSON, endPoint: "InitiatePayment").ConfigureAwait(false);
            return JsonConvert.DeserializeObject<GenericResponse<InitiatePaymentResponse>>(response);
        }
        public async Task<GenericResponse<SendPaymentResponse>> SendPayment(SendPaymentRequest request)
        {
            var sendPaymentRequestJSON = JsonConvert.SerializeObject(request);
            var response = await _client.PerformRequest(sendPaymentRequestJSON, endPoint: "SendPayment").ConfigureAwait(false);
            return JsonConvert.DeserializeObject<GenericResponse<SendPaymentResponse>>(response);
        }
        public async Task<GenericResponse<ExecutePaymentResponse>> ExecutePayment(ExecutePaymentRequest executePaymentRequest)
        {

            var executeRequestJSON = JsonConvert.SerializeObject(executePaymentRequest);
            var response = await _client.PerformRequest(executeRequestJSON, endPoint: "ExecutePayment").ConfigureAwait(false);
            return JsonConvert.DeserializeObject<GenericResponse<ExecutePaymentResponse>>(response);

        }
        public async Task<GenericResponse<DirectPaymentResponse>> DirectPayment(DirectPaymentRequest directPaymentRequest)
        {
            var directPaymentRequestJSON = JsonConvert.SerializeObject(directPaymentRequest);
            var response = await _client.PerformRequest(directPaymentRequestJSON, url: directPaymentRequest.PaymentURL).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<GenericResponse<DirectPaymentResponse>>(response);
        }
        public async Task<GenericResponse<bool>> CancelToken(string paymentToken)
        {
            string url = $"/v2/CancelToken?token={paymentToken}";
            var response = await _client.PerformRequest(url).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<GenericResponse<bool>>(response);

        }
        public async Task<GenericResponse<bool>> CancelRecurringPayment(string recurringId)
        {
            string url = $"/v2/CancelRecurringPayment?recurringId={recurringId}";
            var response = await _client.PerformRequest(url).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<GenericResponse<bool>>(response);

        }
        public async Task<GenericResponse<GetPaymentStatusResponse>> GetPaymentStatus(GetPaymentStatusRequest getPaymentStatusRequest)
        {

            var GetPaymentStatusRequestJSON = JsonConvert.SerializeObject(getPaymentStatusRequest);
            var response = await _client.PerformRequest(GetPaymentStatusRequestJSON, endPoint: "GetPaymentStatus").ConfigureAwait(false);
            return JsonConvert.DeserializeObject<GenericResponse<GetPaymentStatusResponse>>(response);

        }


        public async Task<GenericResponse<GetPaymentStatusResponse>> LogTransaction(GetPaymentStatusRequest getPaymentStatusRequest)
        {

            var GetPaymentStatusRequestJSON = JsonConvert.SerializeObject(getPaymentStatusRequest);
            var response = await _client.PerformRequest(GetPaymentStatusRequestJSON, endPoint: "GetPaymentStatus").ConfigureAwait(false);
            var content = new StringContent(response, Encoding.UTF8, "application/json");
            var transactionResponse = await _httpClient.PostAsync($"/api/LogTransaction", content).ConfigureAwait(false);
            if (!transactionResponse.IsSuccessStatusCode)
                logger.LogError($"logTransaction :{response}");
            return JsonConvert.DeserializeObject<GenericResponse<GetPaymentStatusResponse>>(response);

        }

    }
}
