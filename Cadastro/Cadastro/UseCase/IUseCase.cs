public interface IUseCaseAsync<TRequest, TResponse>
{
    public Task<TResponse> ExecuteAsync(TRequest request);
}

public interface IUseCase<TRequest, TResponse>
{
    public TResponse Execute(TRequest request);
}