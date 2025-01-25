using Cart.Application.DTOs;
using Cart.Application.Mappers;
using Cart.Application.Response;
using Cart.Application.Services.AuthServices;
using Cart.Application.Services.ExternalServices;
using Cart.Core.ValueObjects;
using Cart.Infrastructure.ExternalServices.Configurations;
using Microsoft.Extensions.Options;

namespace Cart.Infrastructure.ExternalServices
{
    public class VoucherRestService(HttpClient httpClient, IUserService userService, IOptions<VoucherRestServiceConfig> settings)
               : RestServiceBase(httpClient, userService), IVoucherRestService
    {
        private readonly VoucherRestServiceConfig _settings = settings.Value;
        public async Task<Response<Voucher>> GetVoucherByCodeAsync(string code)
        {
            var isAuthorized = SetAuthorizationHeaders();
            if (!isAuthorized) return new(false, 401, null, "Authentication failed: token is missing or invalid.");

            HttpResponseMessage httpResponseMessage;
            try
            {
                httpResponseMessage = await _httpClient.GetAsync($"{_settings.Uri}/vouchers/{code}").ConfigureAwait(false);
                httpResponseMessage.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                return new(false, 500, null, $"Request failed: {ex.Message}");
            }

            var response = await DeserializeResponse<ApiResponseDTO<VoucherDTO>>(httpResponseMessage).ConfigureAwait(false);
            if (response is null || response.Data is null)
                return new(false, 404, null, "Voucher not found");

            return new(true, 200, response.Data.MapFromResponseToVoucher());
        }
    }
}
