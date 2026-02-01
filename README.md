# Shuttle.Core.Cron

Provides [cron](https://en.wikipedia.org/wiki/Cron) expression parsing:

## Installation

```bash
dotnet add package Shuttle.Core.Cron
```

## Usage

```
 ┌───────────── minute (0 - 59)
 │ ┌─────────── hour (0 - 23)
 │ │ ┌───────── day of the month (1 - 31)
 │ │ │ ┌─────── month (1 - 12)
 │ │ │ │ ┌───── day of the week (1 - 7): Sunday to Saturday 
 │ │ │ │ │
 │ │ │ │ │
 * * * * *
```

```c#
using Shuttle.Core.Cron;

// Create a cron expression for every 5 minutes
var cron = new CronExpression("0/5 * * * *");

// Get next occurrence
var nextRun = cron.NextOccurrence();

// Get next occurrence from a specific date
var specificDate = new DateTime(2025, 1, 1, 10, 30, 0);
var nextFromSpecific = cron.GetNextOccurrence(specificDate);

// Get previous occurrence
var previousRun = cron.PreviousOccurrence();

// Example: Run at 2 AM on the last day of every month
var endOfMonthCron = new CronExpression("0 2 L * *");
var endOfMonthRun = endOfMonthCron.NextOccurrence();
```

## CronExpression

``` c#
public CronExpression(string expression, ISpecificationFactory specificationFactory = null) : this(expression, DateTime.Now, specificationFactory);
public CronExpression(string expression, DateTime date, ISpecificationFactory specificationFactory = null);
```

Creates a `CronExpression` instance and parses the given `expression`.  The `date` specifies to root date from which to determine either the next or previous occurrence.

``` c#
public DateTime NextOccurrence();
public DateTime NextOccurrence(DateTime date);
```

Returns the next date that would follow the given `date`.  This is accomplished by adding 1 minute to the relevant date.  If no date is provided the root date will be used.  This method also sets the root date to the result.

``` c#
public DateTime GetNextOccurrence(DateTime date);
```

Returns the next date that would follow the given `date`.  If the given `date` satisfies the required specification(s) then the `date` is returned as-is.

``` c#
public DateTime PreviousOccurrence();
public DateTime PreviousOccurrence(DateTime date);
```

Returns the previous date that would precede the given `date`.  This is accomplished by subtracting 1 minute from the relevant date.  If no date is provided the root date will be used.  This method also sets the root date to the result.

``` c#
public DateTime GetPreviousOccurrence(DateTime date);
```

Returns the previous date that would precede the given `date`.  If the given `date` satisfies the required specification(s) then the `date` is returned as-is.

## Cron Samples

Format is {minute} {hour} {day-of-month} {month} {day-of-week}

| Field | Options |
| --- | --- |
| `minutes` | 0-59 , - * / |
| `hours` | 0-23 , - * / |
| `day-of-month` | 1-31 , - * ? / L W |
| `month` | 1-12 or JAN-DEC	, - * / |
| `day-of-week` | 1-7 or SUN-SAT , - * ? / L # |

If `day-of-month` is specified then `day-of-week` should be `?` and vice-versa.

Examples:
```
* * * * * - is every minute of every hour of every day of every month
5,10-12,17/5 * * * * - minute 5, 10, 11, 12, and every 5th minute after that
```

## Specifications

Specifications need to implement `ISpecification<CronField.Candidate>`.

You may pass an implementation of the `ISpecificationFactory` as a parameter to the `CronExpression`.  There is a `DefaultSpecificationFactory` that accepts a function callback in the constructor for scenarios where an explicit `ISpecificationFactory` implementation may not be warranted, e.g.:

``` c#
var factory = new DefaultSpecificationFactory(parameters =>
{
    return !parameters.Expression.Equals("H", StringComparison.InvariantCultureIgnoreCase) 
        ? null 
        : new Specification<CronField.Candidate>(candidate => candidate.Date.Day % 2 == 0);
});
```
