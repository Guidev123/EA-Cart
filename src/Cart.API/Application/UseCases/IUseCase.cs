using Cart.API.Application.Response;

namespace Cart.API.Application.UseCases
{
    public interface IUseCase<Inp, Out>
    {
        Task<Response<Out>> HandleAsync(Inp input);
    }
}
