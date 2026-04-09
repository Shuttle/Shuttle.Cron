using Shuttle.Contract;
using Shuttle.Specification;

namespace Shuttle.Cron;

public class SpecificationFactory : ISpecificationFactory
{
    private readonly Func<SpecificationParameters, ISpecification<CronField.Candidate>?>? _factory;

    public SpecificationFactory(Func<SpecificationParameters, ISpecification<CronField.Candidate>?> factory)
    {
        _factory = Guard.AgainstNull(factory);
    }

    public SpecificationFactory()
    {
    }

    public ISpecification<CronField.Candidate>? Create(SpecificationParameters parameters)
    {
        Guard.AgainstNull(parameters);

        return _factory == null 
            ? throw new CronException(string.Format(Resources.InvalidDefaultSpecificationFactoryConfiguration, parameters.Expression, parameters.FieldName)) 
            : _factory.Invoke(parameters);
    }
}