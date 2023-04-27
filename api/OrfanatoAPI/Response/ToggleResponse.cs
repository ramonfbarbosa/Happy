namespace OrfanatoAPI.Response;

public class ToggleResponse<T>
{
    public bool Success { get; }
    public int ResourceId { get; }
    public IReadOnlyList<string> Errors { get; }

    private ToggleResponse(int resourceId, List<string> errors)
    {
        Success = errors.Count == 0;
        ResourceId = resourceId;
        Errors = errors.AsReadOnly();
    }

    public static ToggleResponse<T> Valid(int resourceId) =>
       new(resourceId, new List<string>());

    public static ToggleResponse<T> Invalid(List<string> errors) =>
        errors.Count != 0
            ? new ToggleResponse<T>(0, errors)
            : throw new ArgumentException($"{nameof(errors)} não deveria estar vazio");
}


