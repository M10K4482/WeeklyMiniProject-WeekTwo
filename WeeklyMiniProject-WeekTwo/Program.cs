using System;
using System.Linq;
using System.Collections.Generic;

ListaProdukter lista = new ListaProdukter(); //Skapa objekt av klass "Listaprodukter"
lista.NyProdukt(); //Kalla på metod "NyProdukt" i klass "Listaprodukter" för att starta programmet

bool stannaKvar = true; //Bool som används efter att man tryckt Q

while (stannaKvar) //Går till denna while efter att man tryckt Q
{
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine("To enter a new product - enter 'P' | " + "To quit - enter: 'Q'");
    Console.ResetColor();

    string valdBokstav = Console.ReadLine().Trim().ToLower();

    switch (valdBokstav) //Kolla vald bokstav
    {
        case "p":
            lista.NyProdukt(); //Bokstaven p vald så kalla på "NyProdukt"
            continue;
        case "q": 
            stannaKvar = false; //Sätt "stannaKvar" till false och avsluta programmet
            break;
        default:
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Wrong input, enter 'P' or 'Q'"); //Om användaren inte tryckt q eller p skriv att det är fel input
            Console.ResetColor();
            break;
    }
}

public class ListaProdukter //Klass med metoder för produktlistan
{
    private List<Listobjekt> produkter = new List<Listobjekt>(); //List objekt av "Listobjekt" klassen

    public void NyProdukt() //Metod för att lägga till ny produkt
    {
        string skrivenText;
        string namn;
        string kategori;
        int pris;

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("To enter a new product - follow the steps | To quit - enter: 'Q'");
        Console.ResetColor();

        while (true) //Går igenom kategori, namn etc. och kollar användarens input
        {
            Console.Write("Enter a Category: ");
            kategori = KollaText(); //Metod för att kolla inputen som skrivits (metoden kommer längre ner), returen från "kollaText"
            if (kategori == "q") //Om q valts så hoppa ur while
            {
                break;
            }

            Console.Write("Enter a Product Name: ");
            namn = KollaText();
            if (namn == "q")
            {
                break;
            }

            Console.Write("Enter a Price: ");
            skrivenText = KollaText(true); //Skicka med true så att "kollaText" metoden längre ner vet att den ska kolla efter nummer
            if (skrivenText == "q")
            {
                break;
            }

            pris = Convert.ToInt32(skrivenText);
            produkter.Add(new Listobjekt{Kategori = kategori,Namn = namn,Pris = pris}); //Lägg till objekt till list med texten från alla tre fält

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("The product was successfully added!"); //Text när allt gått som det ska
            Console.ResetColor();
            Console.WriteLine("");
        }

        SkrivLista(); //Kalla på "SkrivLista" och skriv ut produktlistan

    }

    public void SkrivLista() //Metod för att skriva ut listan med produkter
    {
        if (produkter.Count != 0) //Så länge produktlista inte är tom
        {

            Console.WriteLine("");
            Console.WriteLine("Category".PadRight(30) + "Product".PadRight(30) + "Price");

            foreach (var n in produkter.OrderBy(p => p.Pris)) //Hämta från "Listobjekt" klassen med lambda och sortera efter pris
            {
                Console.WriteLine(n.Kategori.PadRight(30) + n.Namn.PadRight(30) + n.Pris.ToString());
                Console.ResetColor();
            }

            Console.WriteLine();
            Console.WriteLine("".PadRight(30) + "Total amount:".PadRight(30) + produkter.Sum(p => p.Pris)); //Hämta från "Listobjekt" klassen med lambda och summera priset
            Console.WriteLine("");
        }

    }

    public string KollaText(bool kollaNummer = false) //Metod för att kolla input med bool för att kolla efter text eller nummer
    {
        while (true)
        {
            string skrivenText = Console.ReadLine().Trim();
            if (String.IsNullOrWhiteSpace(skrivenText)) //Om ingen text skrivits
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You need to write an input");
                Console.ResetColor();
            }
            else if (skrivenText.ToLower() == "q") //Om "q" skrivs returnera "q"
            {
                return "q";
            }
            else if (kollaNummer == true) //Om true skickats så kolla att det är ett nummer som skrivits
            {
                if (int.TryParse(skrivenText, out int nummer)) //TryParse för att kolla om input är ett nummer
                {
                    return nummer.ToString(); //Om det är ett nummer så returnera som string
                }
                else //Om inte ett nummer så skriv att det är fel input
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("This input must be a number");
                    Console.ResetColor();
                }
            }
            else
            {
                return skrivenText; //Returnera string "skrivenText" om allt innan gick som det skulle i tidigare steg
            }
        }
    }

}

public class Listobjekt //Klass med properties för de olika värdena i produktlistan för kommande objekt
{
    public string Kategori { get; set; } //För "Category"
    public string Namn { get; set; } //För "Name"
    public int Pris { get; set; } //För "Price"

}
