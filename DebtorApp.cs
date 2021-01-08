using System;

namespace Debtor
{
    public class DebtorApp
    {
        public Database Database { get; set; } = new Database();

        public void IntroduceDebtorApp()
        {
            Console.WriteLine("Aplikacja dłużnik gminy Pleśna");
        }
        public void AddBorrower()
        {
            Console.Clear();

            Console.WriteLine("Podaj nazwę dłużnika, którego chcesz dodać do listy");
            var userName = Console.ReadLine();

            Console.WriteLine("Podaj nazwisko dłużnika");
            var userSurname = Console.ReadLine();

            Console.WriteLine("Podaj adress dłużnika");
            var address = Console.ReadLine();

            Console.WriteLine("Podaj kwotę długu");

            var userAmount = Console.ReadLine();

            var amountInDecimal = default(decimal);

            while (!decimal.TryParse(userAmount, out amountInDecimal))
            {
                Console.WriteLine("Podano niepoprawną kwotę");

                Console.WriteLine("Podaj kwotę długu");

                userAmount = Console.ReadLine();
            }

            Database.AddBorower(userName, userSurname, address, amountInDecimal);
        }

        public void DeleteBorrower()
        {
            Console.Clear();
            ListAllBorrowers();
            Console.WriteLine("Podaj ID dłużnika, którego chcesz usunąć z listy");

            var id = Console.ReadLine();

            var idInInt = default(int);

            while (!int.TryParse(id, out idInInt))
            {
                Console.WriteLine("Podano niepoprawne ID");

                Console.WriteLine("Podaj ID dłużnika");

                id = Console.ReadLine();
            }

            Database.DeleteBorrower(idInInt);
        }

        public void ListAllBorrowers()
        {
            Console.WriteLine("\n\tOto lista Twoich dłużników: ");

            Database.ListAllBorrowers();

            Console.WriteLine("\n");
        }

        public int GetBorrowerId()
        {
            Console.WriteLine("\tPodaj ID dłużnika");

            var id = Console.ReadLine();

            var idInInt = default(int);

            while (!int.TryParse(id, out idInInt))
            {
                Console.WriteLine("Podano niepoprawne ID");

                Console.WriteLine("Podaj ID dłużnika");

                id = Console.ReadLine();
            }

            return idInInt;
        }
        public void UpdateBorrowersData()
        {
            Console.Clear();
            ListAllBorrowers();

            Console.WriteLine("  Co chciałbyś zaaktualizować");
            Console.WriteLine("  name\t-   zmiana imienia dłużnika");
            Console.WriteLine("  sname\t-   zmiana nazwiska dłużnika");
            Console.WriteLine("  adrr\t-   zmiana adresu dłużnika");
            Console.WriteLine("  amt\t-   zmiana kwoty długu dłużnika\n");
            Console.WriteLine("  exit\t-   wyjście z edycji\n");

            var userInput = Console.ReadLine().ToLower();

            var idInInt = GetBorrowerId();

            var databaseType = default(string);
            var databaseValue = default(string);

            switch (userInput)
            {
                case "name":
                    {
                        databaseType = "Name";

                        Console.WriteLine("Podaj nowe imie dłużnika");

                        databaseValue = Console.ReadLine();
                    }
                    break;
                case "sname":
                    {
                        databaseType = "Surname";

                        Console.WriteLine("Podaj nowe nazwisko dłużnika");

                        databaseValue = Console.ReadLine();
                    }
                    break;
                case "adrr":
                    {
                        databaseType = "Address";

                        Console.WriteLine("Podaj nowy adres dłużnika");

                        databaseValue = Console.ReadLine();
                    }
                    break;
                case "amt":
                    {
                        Console.WriteLine("Podaj kwotę");

                        var amount = Console.ReadLine();

                        var amountInDecimal = default(decimal);

                        while (!decimal.TryParse(amount, out amountInDecimal))
                        {
                            Console.WriteLine("Podano niepoprawną kwotę");

                            Console.WriteLine("Podaj kwotę");

                            amount = Console.ReadLine();
                        }

                        Database.UpdateBorrowedMoney(idInInt, amountInDecimal);
                    }
                    break;
                case "exit":
                    return;
                default:
                    Console.WriteLine("Podano złą wartość");
                    break;
            }

            Database.UpdateBorrowerData(idInInt, databaseType, databaseValue);

            ListAllBorrowers();
        }

        public void AskForAction()
        {
            Console.WriteLine("\n\tPodaj czynność, którą chcesz wykonać\n");

            var userInput = default(string);

            while (userInput != "exit")
            {
                Console.WriteLine("  add\t-   dodawanie dłużnika");
                Console.WriteLine("  del\t-   usuwanie dłużnika");
                Console.WriteLine("  list\t-   wypisywanie listy dłużników");
                Console.WriteLine("  upd\t-   zaaktualizowanie kwoty długu");
                Console.WriteLine("  exit\t-   wyjście z programu\n");

                userInput = Console.ReadLine().ToLower();

                switch (userInput)
                {
                    case "add":
                        AddBorrower();
                        break;
                    case "del":
                        DeleteBorrower();
                        break;
                    case "list":
                        Console.Clear();
                        ListAllBorrowers();
                        break;
                    case "upd":
                        UpdateBorrowersData();
                        break;
                    case "exit":
                        return;
                    default:
                        Console.WriteLine("Podano złą wartość");
                        break;
                }
            }
        }
    }
}
