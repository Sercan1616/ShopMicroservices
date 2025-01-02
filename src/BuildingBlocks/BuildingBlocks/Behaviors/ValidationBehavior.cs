using BuildingBlocks.CQRS;
using FluentValidation;
using MediatR;

namespace BuildingBlocks.Behaviors;

// IPipelineBehavior MediatR özelliği. Bu yapıda işlemden önce kullanılıyor.
// Yani araya giriyor. Bu kodda ValidationBehavior, komut (istek) next() ile bir sonraki
// işleme geçmeden önce tüm doğrulama işlemlerini çalıştırıyor.
// Böylece, herhangi bir doğrulama hatası varsa, asıl işleme geçmeden
// ValidationException fırlatılıyor ve işlem kesiliyor.

public class ValidationBehavior<TRequest, TResponse>
    (IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommand<TResponse> // Bu kısım sadece CRUD operasyonları için geçerli. Requestlerde çalışmaz
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        // Tüm validationların tamamlanmasını bekler.
        var validationResults = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        // Eğer bir validation hatası varsa diye bakar.
        var failuers = validationResults
            .Where(r => r.Errors.Any())
            .SelectMany(r => r.Errors)
            .ToList();

        if (failuers.Any())
        {
            throw new ValidationException(failuers);
        }

        return await next();
    }
}
