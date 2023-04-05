namespace Module9
{
    class Program
    {
        static void Main(string[] args)
        {
            // Блок для первого задания
            Exception[] exceptions = new Exception[5]; 
            exceptions[0] = new MyException("Введены некорректные данные. Значение должно быть 1 или 2.");
            exceptions[1] = new ArgumentException("Непустой аргумент, передаваемый в метод, является недопустимым.");
            exceptions[2] = new FormatException("Значение не находится в соответствующем формате для преобразования из строки методом преобразования, например Parse .");
            exceptions[3] = new TimeoutException("Срок действия интервала времени, выделенного для операции, истек.");
            exceptions[4] = new OverflowException("Арифметическое, приведение или операция преобразования приводят к переполнению.");
            //-------------------------------------------------

            String[] users = { "Иванов", "Петров", "Сидоров", "Тарасов", "Пивной"}; // Массив пользователей
            List<string> nameList = new List<string>();
            nameList.AddRange(users); // Преобразовываю массив в список

            SortUsers sortUsers = new SortUsers(nameList); // Создаю объект класса
            sortUsers.ReadEvent += Choice; // Назначаю слушателя

             while (true)
             {
                try
                {
                    sortUsers.Read(sortUsers); // Вызов метода сортировки
                }
                catch (MyException e)
                {
                    Console.WriteLine(exceptions[0]);
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(exceptions[1]);
                }
                catch (FormatException e)
                {
                    Console.WriteLine(exceptions[2]);
                }
                catch (TimeoutException e)
                {
                    Console.WriteLine(exceptions[3]);
                }
                catch (OverflowException e)
                {
                    Console.WriteLine(exceptions[4]);
                }

             }

        }
        static void Choice(int number, SortUsers sortUsers) // Метод для определения, как будет сортироваться список
        {
            switch(number) 
            { 
                case 1: sortUsers.Sort1(); break;
                case 2: sortUsers.Sort2(); break;

            }
        }
    }

    public class SortUsers 
    {
        public delegate void ReaderDelegate(int number, SortUsers sortUsers);
        public event ReaderDelegate ReadEvent;
        List<string> People = new List<string>();

        public void Read(SortUsers sortUsers) // Метод проверки введенных данных
        {
            Console.WriteLine("Введите число 1 для сортировки пользователей А-Я, либо число 2 для сортировки Я-А");
            int number = Convert.ToInt32(Console.ReadLine());
            if (number != 1 && number != 2) throw new MyException("Введены некорректные данные. Значение должно быть 1 или 2.");
            NumberEntered(number, sortUsers);
        }

        protected virtual void NumberEntered(int number, SortUsers sortUsers)
        {
            ReadEvent?.Invoke(number, sortUsers);
        }

        public void Sort1() // Метод сортировки А-Я
        {
            People.Sort();
            foreach(string people in People)
            {
                Console.WriteLine(people);
            }
        }
        
        public void Sort2() // Метод сортировки Я-А
        {
            People.Sort();
            People.Reverse();
            foreach (string people in People)
            {
                Console.WriteLine(people);
            }
        }
        public SortUsers(List<String> Names) // Конструктор класса
        {
            foreach(string name in Names)
            {
                People.Add(name);
            }
        }

    }
    public class MyException : Exception // Класс собственного исключения
    {

        public MyException(string message)
            : base(message)
        { }
    }

}