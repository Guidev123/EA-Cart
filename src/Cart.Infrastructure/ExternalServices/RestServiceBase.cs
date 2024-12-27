using Cart.Application.Interfaces.Services;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Cart.Infrastructure.ExternalServices
{
    public abstract class RestServiceBase(HttpClient httpClient, IUserService userService)
    {
        protected readonly HttpClient _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        private readonly IUserService _userService = userService ?? throw new ArgumentNullException(nameof(userService));

        protected bool SetAuthorizationHeaders()
        {
            var token = _userService.GetToken();
            if (string.IsNullOrWhiteSpace(token)) return false; 

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return true;
        }

        protected static async Task<T?> DeserializeResponse<T>(HttpResponseMessage responseMessage)
        {
            ArgumentNullException.ThrowIfNull(responseMessage);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var content = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonSerializer.Deserialize<T>(content, options);
        }
    }

}
