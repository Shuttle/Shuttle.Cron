using System.Text.RegularExpressions;

namespace Shuttle.Cron;

public partial class CronMonth : CronField
{
    private static readonly Regex MonthExpression = CreateMonthExpression();

    public CronMonth(string value, ISpecificationFactory? specificationFactory = null)
        : base(MonthExpression.Replace(value,
            match =>
            {
                return match.Value.ToLower() switch
                {
                    "jan" => "1",
                    "feb" => "2",
                    "mar" => "3",
                    "apr" => "4",
                    "may" => "5",
                    "jun" => "6",
                    "jul" => "7",
                    "aug" => "8",
                    "sep" => "9",
                    "oct" => "10",
                    "nov" => "11",
                    _ => "12"
                };
            }), specificationFactory)
    {
        DefaultParsing(FieldName.Month, 1, 12);
    }

    public override DateTime GetNext(DateTime date)
    {
        while (!IsSatisfiedBy(new(FieldName.Month, Expression, date)))
        {
            date = date.AddMonths(1);
        }

        return date;
    }

    public override DateTime GetPrevious(DateTime date)
    {
        while (!IsSatisfiedBy(new(FieldName.Month, Expression, date)))
        {
            date = date.AddMonths(-1);
        }

        return date;
    }

    [GeneratedRegex("jan|feb|mar|apr|may|jun|jul|aug|sep|oct|nov|dec", RegexOptions.IgnoreCase, "en-ZA")]
    private static partial Regex CreateMonthExpression();
}