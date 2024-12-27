using Cart.Application.Response;
using Cart.Application.Services;
using Cart.Core.ValueObjects;
using Cart.Infrastructure.ExternalServices.Configurations;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Cart.Infrastructure.ExternalServices
{
    public class VoucherRestService : IVoucherRestService
    {
        private readonly HttpClient _httpClient;
        private readonly IUserService _userService;

        public VoucherRestService(HttpClient httpClient, IOptions<VoucherRestServiceConfig> settings, IUserService userService)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.Uri);
            _userService = userService;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _userService.GetUsetToken());
        }

        public async Task<Response<Voucher>> GetVoucherByCodeAsync(string code)
        {
            var httpResponseMessage = await _httpClient.GetAsync($"/vouchers/{code}");

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await DeserializeResponse<Voucher>(httpResponseMessage);

            return new Response<Voucher>(response, 200);
        }

        private static async Task<T?> DeserializeResponse<T>(HttpResponseMessage responseMessage)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var content = await responseMessage.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(content, options);
        }
    }
}