namespace sample1;
using Newtonsoft.Json;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

class Program
{
    static void Main(string[] args)
    {
        Person person = new Person("John", 30);
        // person.Hello(true);
        // person.Hello(false);

        string json = JsonConvert.SerializeObject(person);
        Console.WriteLine(json);


        // Resize image
        using (Image image = Image.Load("/workspaces/AA-Serverless-NET/sample1/input/image.png"))
        {
            image.Mutate(x => x.Resize(100, 100));
            image.Save("/workspaces/AA-Serverless-NET/sample1/output/image.png");
        }

        
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