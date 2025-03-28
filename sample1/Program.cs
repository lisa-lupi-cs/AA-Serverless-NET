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

        // Resize images in the input folder using Parallel.ForEach
        string inputDirectory = "/workspaces/AA-Serverless-NET/sample1/input/";
        string outputDirectory = "/workspaces/AA-Serverless-NET/sample1/output/";

        var imageFiles = Directory.GetFiles(inputDirectory, "*.png");

        Parallel.ForEach(imageFiles, imageFile =>
        {
            using (Image image = Image.Load(imageFile))
            {
                image.Mutate(x => x.Resize(100, 100));
                string outputFilePath = Path.Combine(outputDirectory, Path.GetFileName(imageFile));
                image.Save(outputFilePath);
            }
        });
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