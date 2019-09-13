using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_Delegate_20190906
{
    /* Класс, описывающий счет в банке */
    class Account
    {
        int total; // Переменная для хранения суммы

        public Account(int sum) => total = sum;
        public int CurrentSum { get { return total; } }
        public void Put(int sum) => total += sum;
        public void Withdraw(int sum)
        {
            if (sum <= total)
            {
                total -= sum;
                
                // проверяем существует ли ссылка на делегат, если да, то записываем в делегат стоку
                if (delegateLink != null)
                delegateLink($"Сумма {sum} снята со счета");
            }
            else
            {
                // проверяем существует ли ссылка на делегат, если да, то записываем в делегат стоку
                if (delegateLink != null)
                delegateLink("Недостаточно денег на счете");  
            }
        }

        // Объявляем делегат принимающий строку 
        public delegate void AccountStateHandler(string message);

        // Создаем переменную-ссылку на делегата - переменная хранит адрес на вызов делегата, делегат принимает и харанит строку
        AccountStateHandler delegateLink;

        // Регистрируем делегат - записываем в делегат ссылку на статический метод 
        public void RegisterHandler(AccountStateHandler ShowMessageLink) => delegateLink = ShowMessageLink;
    }
    class Program
    {
        // Создаем статический метод для вывода сгенерированного сообщения по событию с объектом класса Account
        private static void ShowMessage(string message) => Console.WriteLine(message);
        static void Main()
        {
            // создаем банковский счет
            Account account = new Account(200);

            // Добавляем в делегат ссылку на метод ShowMessage
            // а сам делегат передается в качестве параметра метода RegisterHandler
            account.RegisterHandler(new Account.AccountStateHandler(ShowMessage));

            // Два раза подряд пытаемся снять деньги
            account.Withdraw(100);
            Console.WriteLine(account.CurrentSum);
            account.Withdraw(150);

            Console.ReadLine();
        }
    }
}


