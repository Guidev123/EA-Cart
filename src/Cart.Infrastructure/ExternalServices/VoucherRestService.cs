using Cart.Application.Interfaces.ExternalServices;
using Cart.Application.Interfaces.Services;
using Cart.Application.Response;
using Cart.Core.ValueObjects;

namespace Cart.Infrastructure.ExternalServices
{
    public class VoucherRestService(HttpClient httpClient, IUserService userService)
               : RestServiceBase(httpClient, userService), IVoucherRestService
    {
        public async Task<Response<Voucher>> GetVoucherByCodeAsync(string code)
        {
            var isAuthorized = SetAuthorizationHeaders();
            if (!isAuthorized) return new(null, 401, "Authentication failed: token is missing or invalid.");

            HttpResponseMessage httpResponseMessage;
            try
            {
                httpResponseMessage = await _httpClient.GetAsync($"/vouchers/{code}").ConfigureAwait(false);
                httpResponseMessage.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                return new(null, 500, $"Request failed: {ex.Message}");
            }

            var response = await DeserializeResponse<Voucher>(httpResponseMessage).ConfigureAwait(false);
            return new(response, 200);
        }
    }
}
