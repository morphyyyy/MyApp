using BlazorBootstrap;
using FrontEnd.Services;
using FrontEnd.Services.Contracts;
using Microsoft.AspNetCore.Components;
using Models.DTOs;
using System.Drawing;
using System.Linq;

namespace FrontEnd.Pages.Transactions
{
    public class TransactionsBase : ComponentBase
    {
        [Inject]
        ITransactionService TransactionService { get; set; }
        [Inject]
        Services.ToastService ToastService { get; set; }
        public List<TransactionDTO> Transactions { get; set; } = new List<TransactionDTO>();
        public List<KeyValuePair<string, double?>> monthlyTransactionsByRange;
        public List<KeyValuePair<string, double?>> projectedMonthlyBalanceByRange;
        public TransactionDTO newTransactionDTO = new TransactionDTO { Date = DateOnly.FromDateTime(DateTime.Now) };

        public List<KeyValuePair<string, double?>> GetMonthlyTransactionsByRange(DateOnly startDate, DateOnly endDate)
        {
            double expenses = Math.Abs(Math.Round(Transactions.Where(t => (t.Date >= startDate && t.Date <= endDate) && t.Amount < 0).Sum(t => t.Amount), 0));
            double earnings = Math.Round(Transactions.Where(t => (t.Date >= startDate && t.Date <= endDate) && t.Amount > 0).Sum(t => t.Amount), 0);

            List<KeyValuePair<string, double?>> transactionsByRange = new List<KeyValuePair<string, double?>>
            {
                new KeyValuePair<string, double?>
                (
                    "Expenses",
                    expenses
                ),
                new KeyValuePair<string, double?>
                (
                    "Earnings",
                    earnings
                )
            };

            return transactionsByRange;
        }

        public List<KeyValuePair<string, double?>> GetProjectedMonthlyBalanceByRange(DateOnly startDate, DateOnly endDate)
        {
            TransactionDTO lastPaycheck = Transactions.First(t => t.Description == "Paycheck");
            double? currentBalance = Transactions.First().Balance;
            List<TransactionDTO> projections = new List<TransactionDTO>();
            double expenses = Math.Round(Transactions.Where(t => t.Date.Month == startDate.AddMonths(-1).Month && t.Date.Year == startDate.Year && t.Amount < 0).Sum(t => t.Amount), 0);
            int biweekly = (endDate.DayNumber - startDate.DayNumber) / 14;
            int months = GetMonthsDifference(startDate, endDate);

            projections.AddRange(Enumerable.Range(1, biweekly + 1)
                .Select(i => new TransactionDTO
                {
                    Date = lastPaycheck.Date.AddDays(i * 14),
                    Amount = lastPaycheck.Amount,
                    Description = "Projected Paycheck"
                }
            ));
            projections.AddRange(Enumerable.Range(1, months)
                .Select(i => new TransactionDTO
                {
                    Date = lastPaycheck.Date.AddMonths(i),
                    Amount = expenses,
                    Description = "Projected Expenses"
                }
            ));

            List<double?> balance = Enumerable.Range(1, months).Select(i =>
                projections.Where(t =>
                    t.Date.Month == DateTime.Now.AddMonths(i).Month &&
                    t.Date.Year == DateTime.Now.AddMonths(i).Year)
                .Sum(t => t.Amount))
                .Cast<double?>().ToList();

            for (int i = balance.Count(); i > 0; i--)
            {
                double? sum = balance.GetRange(0, i).Sum();
                balance[i - 1] = sum + currentBalance;
            }

            List<KeyValuePair<string, double?>> projection = Enumerable.Range(1, months)
                .Select(i => {
                    string month = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(i).ToString("MMMM");
                    double? amount = balance[i - 1];
                    return new KeyValuePair<string, double?>(month, amount);
                }).ToList();

            return projection;
        }

        public int GetMonthsDifference(DateOnly startDate, DateOnly endDate)
        {
            // Calculate the number of months between the two dates
            int months = (endDate.Year - startDate.Year) * 12 + endDate.Month - startDate.Month;

            return months;
        }

        public List<string> GetMonthNamesInRange(DateOnly startDate, DateOnly endDate)
        {
            List<string> monthNames = new List<string>();

            // Ensure that the start date is before the end date
            if (startDate > endDate)
            {
                throw new ArgumentException("Start date must be earlier than the end date.");
            }

            // Iterate from the start month to the end month
            DateOnly current = DateOnly.FromDateTime(new DateTime(startDate.Year, startDate.Month, 1));
            while (current <= endDate)
            {
                // Add the month name to the list (e.g., January, etc.)
                monthNames.Add(current.ToString("MMMM"));

                // Move to the next month
                current = current.AddMonths(1);
            }

            return monthNames;
        }

        public async Task Create(TransactionDTO transactionDTO)
        {
            newTransactionDTO.Balance = Transactions.Where(t => t.Date <= newTransactionDTO.Date).First().Balance + newTransactionDTO.Amount;
            newTransactionDTO.CreatedDate = DateTime.Now;
            Transactions.Add(await TransactionService.Create(transactionDTO));
            List<TransactionDTO> transactionCorrections = Transactions.Where(t => t.Date >= newTransactionDTO.Date)
                .OrderBy(t => t.Date)
                .ThenBy(t => t.Id)
                .ToList();
            for (int i = 1; i < transactionCorrections.Count(); i++)
            {
                transactionCorrections[i].Balance = transactionCorrections[i - 1].Balance + transactionCorrections[i].Amount;
                await TransactionService.Update(transactionCorrections[i]);
            }
            Transactions = (await TransactionService.List()).OrderByDescending(t => t.Date).ThenByDescending(t => t.Id).ToList();
            newTransactionDTO = new TransactionDTO { Date = DateOnly.FromDateTime(DateTime.Now) };

            ToastService.ShowToast("Success", "Transaction Added!", ToastLevel.Success);
        }

        public async Task Delete(int Id)
        {
            await TransactionService.Delete(Id);
            Transactions = (await TransactionService.List()).OrderByDescending(t => t.Date).ThenByDescending(t => t.Id).ToList();

            ToastService.ShowToast("Success", "Transaction Deleted!", ToastLevel.Success);
        }
    }
}