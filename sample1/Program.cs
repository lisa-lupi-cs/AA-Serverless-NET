namespace sample1;

class Program
{
    static void Main(string[] args)
    {
        Person person = new Person("John", 30);
        person.Hello(true);
        person.Hello(false);
    }
}

class Person
{
    public string Name { get; set; }
    public int Age { get; set; }

    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public void Hello(bool isLowercase)
    {
        string output = $"Hello {Name}, your are {Age}";
        if (isLowercase)
        {
            Console.WriteLine(output.ToLower());
        }
        else
        {
            Console.WriteLine(output.ToUpper());
        }
    }
}