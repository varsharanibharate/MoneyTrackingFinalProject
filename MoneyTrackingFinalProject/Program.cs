using System.Diagnostics.Contracts;
using System.Security.Principal;

ProcessTransactionDetails processTransactionDetails = new ProcessTransactionDetails();

while (true)
{
    Console.Write("Enter 'F' to fetch, 'E' for Editing list, 'Q' for Quit: ");
    var input = Console.ReadLine();

    // Fetched data and printed list according to user input using conditional statements. 

    if (input.ToString().ToLower().Equals("f"))
    {
        Console.Write("Type 'Income/Expenses' and enter to filter and print data or enter for all: ");
        var filter = Console.ReadLine();

        processTransactionDetails.printAccountList(filter);
    }
    else if (input.ToString().ToLower().Equals("e"))
    {
        Console.WriteLine("Displaying all data for edition.");
        List<Account> accountList = processTransactionDetails.printAccountList("");


        Console.Write("Enter NEW/EDIT/DELETE to be add new/edit existing/removeexisting entry: ");
        var editAction = Console.ReadLine();


        if (editAction.ToLower().Equals("edit"))
        {
            Console.Write("Enter ID to be edited: ");
            var id = Console.ReadLine();

            Account editAccount = new Account();
            accountList.ForEach(accountData =>
            {
                if (accountData.Id == int.Parse(id))
                {
                    editAccount = accountData;
                }

            });


            Console.Write("Enter I for Income and E for expenses type: ");
            var type = Console.ReadLine();
            if (type.ToString().ToLower().Equals("i"))
                editAccount.Type = "MonthlyIncome";

            if (type.ToString().ToLower().Equals("e"))
                editAccount.Type = "MonthlyExpenses";

            Console.Write("Enter Title entry type: ");
            var title = Console.ReadLine();
            editAccount.Title = title.ToString();


            Console.Write("Enter amount: ");
            var amount = Console.ReadLine();
            editAccount.Amount = int.Parse(amount);

            Console.Write("Enter transaction date in 'YYYY-MM-DD' format: ");
            var date = Console.ReadLine();
            editAccount.Date = Convert.ToDateTime(date);

            processTransactionDetails.editList(accountList, editAccount);
            Console.WriteLine("Updated list after modification in data for id: " + id);
            processTransactionDetails.printAccountList("");
        }
        else if (editAction.ToLower().Equals("delete"))
        {
            Console.Write("Enter ID to be deleted: ");
            var id = Console.ReadLine();

            List<Account> newAccountList = new List<Account>();

            accountList.ForEach(accountData =>
            {
                if (!(accountData.Id == int.Parse(id)))
                {
                    newAccountList.Add(accountData);
                }

            });

            processTransactionDetails.writeDataToFile(newAccountList);
            Console.WriteLine("Updated list after deleting id: " + id);
            processTransactionDetails.printAccountList("");
        }
        else if (editAction.ToLower().Equals("new"))
        {
            Account newAccount = new Account();
            Console.WriteLine("Adding new entry");
            Console.Write("Enter I for Income and E for expenses type: ");
            var type = Console.ReadLine();
            if (type.ToString().ToLower().Equals("i"))
                newAccount.Type = "MonthlyIncome";

            if (type.ToString().ToLower().Equals("e"))
                newAccount.Type = "MonthlyExpenses";

            Console.Write("Enter Title entry type: ");
            var title = Console.ReadLine();
            newAccount.Title = title.ToString();


            Console.Write("Enter amount: ");
            var amount = Console.ReadLine();
            newAccount.Amount = int.Parse(amount);

            Console.Write("Enter transaction date in 'YYYY-MM-DD' format: ");
            var date = Console.ReadLine();
            newAccount.Date = Convert.ToDateTime(date);

            int maxSerialNo = 0;
            if (accountList.Count() > 0)
            {
                maxSerialNo = accountList.Max(acc => acc.Id);
            }

            newAccount.Id = maxSerialNo + 1;
            accountList.Add(newAccount);


            processTransactionDetails.writeDataToFile(accountList);

            Console.WriteLine("Updated list after adding new entry.");
            processTransactionDetails.printAccountList("");
        }

    }
    else if (input.ToString().ToLower().Equals("q"))
    {
        break;
    }
}

// Created one base class and two child classes.
class Account
{

    public int Id { get; set; }
    public string Type { get; set; }
    public string Title { get; set; }
    public int Amount { get; set; }
    public DateTime Date { get; set; }
}

class Income : Account
{

    public Income(int id, string type, string title, int amount, DateTime date)
    {
        Id = id;
        Type = type;
        Title = title;
        Amount = amount;
        Date = date;
    }
}

class Expenses : Account
{
    public Expenses(int id, string type, string title, int amount, DateTime date)
    {
        Id = id;
        Type = type;
        Title = title;
        Amount = amount;
        Date = date;
    }
}

// Created one class which contains different methods.
class ProcessTransactionDetails
{
    const string fileName = "MoneyTracking1.txt";

    // Method to read data from file
    public List<Account> readDataFromFile()
    {
        List<Account> accountList = new List<Account>();

        using (var reader = new StreamReader(fileName))
        {
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                String[] values = line.Split(',');

                Account account = new Account();

                account.Id = int.Parse(values[0]);
                account.Type = values[1];
                account.Title = values[2];
                account.Amount = int.Parse(values[3]);
                account.Date = DateTime.Parse(values[4]);
                accountList.Add(account);
            }

            reader.Close();
        }
        return accountList;

    }

    // Method to write data to file
    public void writeDataToFile(List<Account> accountList)
    {

        using (var writer = new StreamWriter(fileName))
        {
            foreach (var account in accountList)
            {

                writer.WriteLine(account.Id.ToString() + ',' + account.Type + ',' + account.Title + ',' + account.Amount + ',' + account.Date);
            }

            writer.Close();
        }

    }

    // Method for editing the account, adding new account and deleting existing account in accountlist

    public void editList(List<Account> accountList, Account editAccount)
    {
        accountList.ForEach(account => {
            if (account.Id == editAccount.Id)
            {
                account.Id = editAccount.Id;
                account.Type = editAccount.Type;
                account.Title = editAccount.Title;
                account.Amount = editAccount.Amount;
                account.Date = editAccount.Date;
            }
        });
        writeDataToFile(accountList);
    }

    public List<Account> printAccountList(String input)
    {
        List<Account> accountList = readDataFromFile();

        Console.WriteLine();

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Id".PadRight(15) + "Type".PadRight(20) + "Title".PadRight(15) + "Amount".PadRight(15) + "Date");
        Console.ResetColor();

        Console.WriteLine();

        // Fetched data using loop over accountList based on user input
        foreach (var account in accountList)
        {
            if (input.ToLower().Trim() == "expenses")
            {
                if (account.Type == "MonthlyExpenses")
                {

                    Console.WriteLine(account.Id.ToString().PadRight(15) + account.Type.PadRight(20) + account.Title.PadRight(15) + account.Amount.ToString().PadRight(15) + account.Date.Month);

                }
            }
            else if (input.ToLower().Trim() == "income")
            {
                if (account.Type == "MonthlyIncome")
                {
                    Console.WriteLine(account.Id.ToString().PadRight(15) + account.Type.PadRight(20) + account.Title.PadRight(15) + account.Amount.ToString().PadRight(15) + account.Date.Month);
                }
            }
            else
            {
                Console.WriteLine(account.Id.ToString().PadRight(15) + account.Type.PadRight(20) + account.Title.PadRight(15) + account.Amount.ToString().PadRight(15) + account.Date.Month);
            }
        }
        return accountList;

    }
}
