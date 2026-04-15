using Shuttle.Specification;

namespace Shuttle.Cron;

public interface ISpecificationFactory
{
    ISpecification<CronField.Candidate>? Create(SpecificationParameters parameters);
}