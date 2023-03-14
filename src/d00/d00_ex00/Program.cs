// See https://aka.ms/new-console-template for more information

if (args.Length != 3 || !double.TryParse(args[0], out var sum) ||
    !double.TryParse(args[1], out var rate) ||
    !int.TryParse(args[2], out var term))
{
    Console.WriteLine("Something went wrong. Check your input and retry.");
    return;
}

var i = rate / 12.0 / 100.0;
var p = Math.Pow((1+i), term);
var payment = (sum * i * p) / (p-1);

var currentDate = DateTime.Today;
// var currentDate = new DateTime(2021, 5, 1);
// var currentDate = new DateTime(2021, 5, 1);
var debt = sum;

for (var countMonths = 1; countMonths <= term; countMonths++)
{
    var period = (int) (currentDate.AddMonths(countMonths) - currentDate.AddMonths(countMonths - 1)).TotalDays;
    var interest = (debt * rate * period) / (100 * (Convert.ToInt16(DateTime.IsLeapYear(currentDate.Year)) + 365));
    var principalDebt = payment - interest;
    debt -= principalDebt;
    if (countMonths == term)
    {
        payment += debt;
        principalDebt = payment - interest;
        debt = 0;
    }
    Console.WriteLine($"{countMonths}\t{currentDate.AddMonths(countMonths):d}\t{payment:N2}\t{principalDebt:N2}\t{interest, -8:N2}\t{debt:N2}");
}

