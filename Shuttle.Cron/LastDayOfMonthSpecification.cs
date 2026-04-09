using Shuttle.Contract;
using Shuttle.Specification;

namespace Shuttle.Cron;

public class LastDayOfMonthSpecification : ISpecification<CronField.Candidate>
{
    public bool IsSatisfiedBy(CronField.Candidate item)
    {
        Guard.AgainstNull(item);

        return item.Date.Day == DateTime.DaysInMonth(item.Date.Year, item.Date.Month);
    }
}