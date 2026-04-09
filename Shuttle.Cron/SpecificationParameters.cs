using Shuttle.Contract;

namespace Shuttle.Cron;

public class SpecificationParameters(FieldName fieldName, string expression)
{
    public string Expression { get; } = Guard.AgainstEmpty(expression);
    public FieldName FieldName { get; } = Guard.AgainstUndefinedEnum<FieldName>(fieldName);
}