using System.Text.Json;

var Pepe = new Persona()
{
    Name = "Pepe",
    Edad = 30
};

Console.WriteLine("Objeto a JSON:");
string json = JsonSerializer.Serialize(Pepe);
Console.WriteLine(json);

Console.WriteLine();

Console.WriteLine("JSON a Objeto:");
string json2 = @"{ ""Name"":""Alan"",""Edad"":29}";
Persona? Alan = JsonSerializer.Deserialize<Persona>(json2);
Console.WriteLine($"Nombre: {Alan?.Name}, Edad: {Alan?.Edad}");

public class Persona
{
    public string Name { get; set; }
    public double Edad { get; set; }

    public string GetDatos() => $"Name: {Name}, Edad: {Edad}";
}
