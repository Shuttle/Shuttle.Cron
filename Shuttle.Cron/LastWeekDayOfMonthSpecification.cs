using Shuttle.Contract;
using Shuttle.Specification;

namespace Shuttle.Cron;

public class LastWeekDayOfMonthSpecification : ISpecification<CronField.Candidate>
{
    public bool IsSatisfiedBy(CronField.Candidate item)
    {
        Guard.AgainstNull(item);

        var compare = item.Date.AddDays(item.Date.Day * -1);

        for (var day = DateTime.DaysInMonth(item.Date.Year, item.Date.Month); day > 0; day--)
        {
            var dayOfWeek = compare.AddDays(day).DayOfWeek;

            if (dayOfWeek != DayOfWeek.Saturday && dayOfWeek != DayOfWeek.Sunday)
            {
                return item.Date.Day == day;
            }
        }

        return false;
    }
}