namespace Skaar.MockDependencyInjection.Exceptions
{
    public class DuplicateParameterSetupException(string name) : IOException(
        $"There are multiple setups of the same parameter ({name})");
}