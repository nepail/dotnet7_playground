// See https://aka.ms/new-console-template for more information
using System.Reflection;

Animal animal = new Animal("cat");

var name = typeof(Animal).GetMethod("GetName", BindingFlags.NonPublic | BindingFlags.Instance)
    .Invoke(animal, Array.Empty<object?>());


Console.WriteLine(name);



public class Animal
{
    private string _name { get; set; }

    internal Animal(string name)
    {
        _name = name;
    }

    private string GetName()
    {
        return _name;
    }

    private void SetName(string name)
    {
        _name = name;
    }
    
}