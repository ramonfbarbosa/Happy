using FluentValidation.Results;

namespace OrfanatoAPI.Response;

public class ValidationResponse<T> where T : class
{
    public ValidationResult Resultado { get; }
    public bool Sucesso => Resultado.IsValid;
    public IReadOnlyCollection<string> Errors { get; }
    public T Entity { get; }

    private ValidationResponse(ValidationResult result, T entity)
    {
        Resultado = result;
        Errors = result.Errors
            .Select(x => x.ErrorMessage)
            .ToList()
            .AsReadOnly();
        Entity = entity;
    }

    public static ValidationResponse<T> Valid(ValidationResult result, T entity) =>
        result.IsValid
            ? new ValidationResponse<T>(result, entity)
            : throw new ArgumentException("Resultado da validação precisa ser valido!");

    public static ValidationResponse<T> Invalid(ValidationResult result) =>
        result.IsValid
            ? throw new ArgumentException("Resultado da validação precisa ser invalido!")
            : new ValidationResponse<T>(result, null);
}
