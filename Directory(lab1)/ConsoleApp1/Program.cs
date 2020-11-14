using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Book book = new Book();
            Console.WriteLine("Для выполнения действия с телефонной книгой введите число: ");
            Console.WriteLine("1 - для создания новой записи.");
            Console.WriteLine("2 - для редактирования созданных записей.");
            Console.WriteLine("3 - для удаления созданных записей.");
            Console.WriteLine("4 - для просмотра созданных учетных записей.");
            Console.WriteLine("5 - для просмотра всех созданных учетных записей с краткой информацией.");
            Console.WriteLine("6 - для завершения работы программы.");
            bool flag = true;
            while (flag)
            {
                string input = Console.ReadLine();
                if (input.Length != 1)
                {
                    Console.WriteLine("Ошибка");
                    continue;
                }
                switch (input)
                {
                    case "1":
                        book.AddContact();
                        break;
                    case "2":
                        Console.WriteLine("Введите номер контакта, который хотите изменить");
                        string inp1 = Console.ReadLine();
                        int numb1;
                        if (int.TryParse(inp1, out numb1))
                        {
                            if (numb1 > 0 && numb1 <= book.size())
                            {
                                book.EditContact(numb1 - 1);
                                Console.WriteLine("Контакт успешно отредактирован")ж
                            }
                        }
                        else
                            Console.WriteLine("Номер контакта введен некорректно");
                        break;
                    case "3":
                        Console.WriteLine("Введите номер контакта, который хотите удалить");
                        string inp2 = Console.ReadLine();
                        int numb2;
                        if (int.TryParse(inp2, out numb2))
                        {
                            if (numb2 > 0 && numb2 <= book.size())
                            {
                                book.DeleteContact(numb2 - 1);
                                Console.WriteLine("Контакт успешно удален");
                            }
                        }
                        else
                            Console.WriteLine("Номер контакта введен некорректно");
                        break;
                    case "4":
                        Console.WriteLine("Введите номер контакта, который хотите просмотреть");
                        string inp3 = Console.ReadLine();
                        int numb3;
                        if (int.TryParse(inp3, out numb3))
                        {
                            if (numb3 > 0 && numb3 <= book.size())
                                book.ShowContact(numb3 - 1);
                        }
                        else
                            Console.WriteLine("Номер контакта введен некорректно");
                        break;
                    case "5":
                        book.ShowSmallBook();
                        break;
                    case "6":
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("Введенные данные некорректны");
                        break;
                }
            }
            Console.ReadKey();
        }
    }
    public class HumanContact
    {
        private string surname;
        private string name;
        private string patronymic;
        private string number;
        private string country;
        private string birthday;
        private string organization;
        private string position;
        private string other;
        private HumanContactIsValid valid;

        public HumanContact(
            string surname,
            string name,
            string number,
            string country,
            string patronymic = null,
            string birthay = null,
            string organization = null,
            string position = null,
            string other = null)
        {
            this.surname = surname;
            this.name = name;
            this.patronymic = patronymic;
            this.number = number;
            this.country = country;
            this.birthday = birthay;
            this.organization = organization;
            this.position = position;
            this.other = other;

            Surname = surname;
            Name = name;
            Patronymic = patronymic;
            Number = number;
            Country = country;
            Birthday = birthay;
            Organization = organization;
            Position = position;
            Other = other;
        }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string Country { get; set; }
        public string Patronymic { get; set; }
        public string Birthday { get; set; }
        public string Organization { get; set; }
        public string Position { get; set; }
        public string Other { get; set; }
    }

    public class HumanContactIsValid
    {
        public bool number;
        public bool country;
        public bool birthday;
        public HumanContactIsValid(HumanContact contact)
        {
            this.number = NumberIsValid(contact.Number);
            this.country = CountryIsValid(contact.Country);
            this.birthday = true;
            if (contact.Birthday != null)
                birthday = BirthdayIsValid(contact.Birthday);
        }
        private bool NumberIsValid(string number)
        {
            for (int i = 0; i < number.Length; ++i)
            {
                if (number[i] < '0' || number[i] > '9')
                    return false;
            }
            return true;
        }
        private bool CountryIsValid(string country)
        {
            for (int i = 0; i < country.Length; ++i)
            {
                if (!((country[i] >= 'a' && country[i] <= 'z') || (country[i] >= 'A' && country[i] <= 'Z')))
                    return false;
            }
            return true;
        }
        private bool BirthdayIsValid(string birthday)
        {
            if (birthday.Length == 10)
            {
                for (int i = 0; i < 10; ++i)
                {
                    if (i != 2 && i != 5)
                    {
                        if (birthday[i] < '0' || birthday[i] > '9')
                            return false;
                    }
                    else
                    {
                        if (birthday[i] != '.')
                            return false;
                    }
                }
            }
            else
                return false;
            return true; ;
        }
        public bool AllIsValid()
        {
            if (number && country && birthday)
                return true;
            return false;
        }
    }
    public class Book
    {
        private List<HumanContact> book = new List<HumanContact>();
        public void AddContact()
        {
            Console.WriteLine("Фамилия:");
            string surname = Console.ReadLine();
            Console.WriteLine("Имя");
            string name = Console.ReadLine();
            Console.WriteLine("Отчество:");
            string patronymic = Console.ReadLine();
            Console.WriteLine("Номер:");
            string number = Console.ReadLine();
            Console.WriteLine("Страна:");
            string country = Console.ReadLine();
            Console.WriteLine("Дата рождения:");
            string birthday = Console.ReadLine();
            Console.WriteLine("Организация:");
            string organization = Console.ReadLine();
            Console.WriteLine("Должность:");
            string position = Console.ReadLine();
            Console.WriteLine("Прочие заметки");
            string other = Console.ReadLine();
            
            if (surname == "" || name == "" || number == "" || country == "")
            {
                Console.WriteLine("Новый контакт не добавлен по причине некорректно введенных данных");
            }
            else
            {
                if (patronymic == "")
                    patronymic = null;
                if (birthday == "")
                    birthday = null;
                if (organization == "")
                    organization = null;
                if (other == "")
                    other = null;
                HumanContact contact = new HumanContact(surname, name, number, country, patronymic, birthday, organization, position, other);
                HumanContactIsValid check = new HumanContactIsValid(contact);
                if (check.AllIsValid())
                {
                    book.Add(contact);
                    Console.WriteLine("Новый контакт успешно добавлен");
                }
                else
                    Console.WriteLine("Новый контакт не добавлен по причине некорректно введенных данных");
            }
        }
        public void EditContact(int cur)
        {
            Console.WriteLine("Введите новое значение для поля, если хотите его изменить:");
            Console.WriteLine("Фамилия:");
            string surname = Console.ReadLine();
            Console.WriteLine("Имя");
            string name = Console.ReadLine();
            Console.WriteLine("Отчество:");
            string patronymic = Console.ReadLine();
            Console.WriteLine("Номер:");
            string number = Console.ReadLine();
            Console.WriteLine("Страна:");
            string country = Console.ReadLine();
            Console.WriteLine("Дата рождения:");
            string birthday = Console.ReadLine();
            Console.WriteLine("Организация:");
            string organization = Console.ReadLine();
            Console.WriteLine("Должность:");
            string position = Console.ReadLine();
            Console.WriteLine("Прочие заметки");
            string other = Console.ReadLine();
            
            HumanContact newContact = new HumanContact(surname, name, number, country, patronymic, birthday, organization, position, other);
            if (newContact.Surname == "")    newContact.Surname = book[cur].Surname; 
            if (newContact.Name == "")       newContact.Name = book[cur].Name; 
            if (newContact.Patronymic == "") newContact.Patronymic = book[cur].Patronymic; 
            if (newContact.Number == "")     newContact.Number = book[cur].Number; 
            if (newContact.Country == "")    newContact.Country = book[cur].Country; 
            if (newContact.Birthday == "")   newContact.Birthday = book[cur].Birthday; 
            if (newContact.Position == "")   newContact.Position = book[cur].Position; 
            if (newContact.Other == "")      newContact.Other = book[cur].Other; 
            
            HumanContactIsValid check = new HumanContactIsValid(newContact);
            if (check.AllIsValid())
            {
                book[cur] = newContact;
                Console.WriteLine("Контакт успешно изменен");
            }
            else
                Console.WriteLine("Контакт не изменен по причине некорректно введенных данных");
        }
        public void DeleteContact(int cur)
        {
            book.RemoveAt(cur);
        }
        public void ShowSmallBook()
        {
            for (int i = 0; i < book.Count; ++i)
            {
                ShowSmallContact(i);
            }
        }
        public void ShowContact(int cur)
        {
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("Номер контакта в телефонной книге: " + (cur + 1));
            Console.WriteLine("Фамилия: " + book[cur].Surname);
            Console.WriteLine("Имя: " + book[cur].Name);
            if (book[cur].Patronymic != null)
                Console.WriteLine("Отчество: " + book[cur].Patronymic);
            Console.WriteLine("Номер: " + book[cur].Number);
            Console.WriteLine("Страна: " + book[cur].Country);
            if (book[cur].Birthday != null)
                Console.WriteLine("Дата рождения: " + book[cur].Birthday);
            if (book[cur].Organization != null)
                Console.WriteLine("Организация: " + book[cur].Organization);
            if (book[cur].Position != null)
                Console.WriteLine("Должность: " + book[cur].Position);
            if (book[cur].Other != null)
                Console.WriteLine("Другое: " + book[cur].Other);
            Console.WriteLine("-----------------------------------------");
        }
        public void ShowSmallContact(int cur)
        {
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("Номер контакста в телефонной книге: " + (cur + 1));
            Console.WriteLine("Фамилия: " + book[cur].Surname);
            Console.WriteLine("Имя: " + book[cur].Name);
            Console.WriteLine("Номер: " + book[cur].Number);
            Console.WriteLine("-----------------------------------------");
        }
        public int size()
        {
            return book.Count();
        }
    }

}
