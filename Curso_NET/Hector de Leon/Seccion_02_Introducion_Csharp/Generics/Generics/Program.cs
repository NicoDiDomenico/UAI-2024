//// 11. Generics, por como yo lo entiendo, es pasar un tipo de dato como parámetro al momento de instanciar. 
//// Ese tipo de dato se puede usar dentro de la clase, ya sea para definir métodos o propiedades, 
//// y se resuelve en tiempo de compilación.

// Basico:
Caja<string> miCaja = new Caja<string>();
miCaja.Contenido = "Juguete";
Console.WriteLine(miCaja.Contenido);

Console.WriteLine();

// Con arreglos:
var numbers = new MyList<int>(5);
var names = new MyList<string>(5);
var beers = new MyList<Beer>(5);

numbers.Add(1);
numbers.Add(2);
numbers.Add(3);
numbers.Add(4);
numbers.Add(5);
numbers.Add(6);
Console.WriteLine(numbers.GetContent());

names.Add("Juan");
names.Add("Nico");
names.Add("Pepe");
names.Add("Ana");
names.Add("Alan");
Console.WriteLine(names.GetContent());

beers.Add(new Beer()
{
    Name = "Quilmes",
    Price = 12
});
beers.Add(new Beer()
{
    Name = "Corona",
    Price = 15
});
beers.Add(new Beer()
{
    Name = "Santa Fe",
    Price = 10
});
Console.WriteLine(beers.GetContent());

// T -> Tipo de Dato 
public class Caja<T>
{
    public T Contenido { get; set; }
}

public class MyList<T>
{
    private List<T> _list; 
    private int _limit;

    public MyList(int limite) {
        _limit = limite;
        _list = new List<T>();
    }

    public void Add(T item)
    {
        if (_list.Count < _limit)
        {
            _list.Add(item);
        }
    }

    public string GetContent()
    {
        string content = "";
        foreach (var item in _list)
            content += item + " ";
        return content;
    }
}

public class Beer
{
    public double Price { get; set; }
    public string Name { get; set; }

    // Todos los tipos de datos heredan de Object, y object tiene el metodo ToString() -> lo sobreescribimos.
    public override string ToString()
    {
        return Name + " " + Price + ".";
    }
}

/*
// Nota:
🔹 Los genéricos eliminan la necesidad de empaquetar y desempaquetar tipos de valor porque el compilador ya sabe qué tipo estás usando, y genera código específico para ese tipo.
🔹 Esto hace que tu código sea más eficiente, más seguro y más limpio.

✅ Tip: otras clases genéricas que ya existen
List<T>
Dictionary<TKey, TValue>
Queue<T>
Stack<T>
Nullable<T> (usado con int?, bool?, etc.)
Task<T> (en programación asíncrona)
*/

