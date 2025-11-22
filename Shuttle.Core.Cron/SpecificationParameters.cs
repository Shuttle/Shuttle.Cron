using Shuttle.Core.Contract;

namespace Shuttle.Core.Cron;

public class SpecificationParameters(FieldName fieldName, string expression)
{
    public string Expression { get; } = Guard.AgainstEmpty(expression);
    public FieldName FieldName { get; } = Guard.AgainstUndefinedEnum<FieldName>(fieldName);
}