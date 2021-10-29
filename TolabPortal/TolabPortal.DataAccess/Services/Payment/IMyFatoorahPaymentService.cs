using System.Net.Http;
using System.Threading.Tasks;
using TolabPortal.DataAccess.Models.Payment;

namespace TolabPortal.DataAccess.Services.Payment
{
    public interface IMyFatoorahPaymentService
    {
        Task<GenericResponse<bool>> CancelRecurringPayment(string recurringId);
        Task<GenericResponse<bool>> CancelToken(string paymentToken);
        Task<GenericResponse<DirectPaymentResponse>> DirectPayment(DirectPaymentRequest directPaymentRequest);
        Task<GenericResponse<ExecutePaymentResponse>> ExecutePayment(ExecutePaymentRequest executePaymentRequest);
        Task<GenericResponse<GetPaymentStatusResponse>> GetPaymentStatus(GetPaymentStatusRequest getPaymentStatusRequest);
        Task<GenericResponse<InitiatePaymentResponse>> InitiatePayment(InitiatePaymentRequest intiatePaymentRequest);
        Task<GenericResponse<SendPaymentResponse>> SendPayment(SendPaymentRequest request);
        Task<string> LogTransaction(GetPaymentStatusRequest getPaymentStatusRequest);
    }
}