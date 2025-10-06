using System;

namespace BonusBerekening
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Bonusberekening Werknemer ===\n");

            // 1️⃣ Vraag naam
            Console.Write("Naam van de werknemer: ");
            string naam = Console.ReadLine();

            // 2️⃣ Vraag aantal jaren gewerkt per afdeling
            int verkoop = VraagAantalJaren("Verkoop");
            int ondersteuning = VraagAantalJaren("Ondersteuning");
            int administratie = VraagAantalJaren("Administratie");

            // 3️⃣ Tel het aantal afdelingen waarin gewerkt is
            int aantalAfdelingen = 0;
            if (verkoop > 0) aantalAfdelingen++;
            if (ondersteuning > 0) aantalAfdelingen++;
            if (administratie > 0) aantalAfdelingen++;

            // 4️⃣ Bereken totale jaren (alleen als minstens 2 afdelingen)
            double bonusPercentage = 0;

            if (aantalAfdelingen >= 2)
            {
                int totaleJaren = verkoop + ondersteuning + administratie;
                bonusPercentage = totaleJaren * 2; // 2% per jaar
            }

            // 5️⃣ Toon resultaat
            Console.WriteLine("\n--- Resultaat ---");
            Console.WriteLine($"Werknemer: {naam}");
            Console.WriteLine($"Afdelingen gewerkt: {aantalAfdelingen}");

            if (bonusPercentage > 0)
                Console.WriteLine($"Bonuspercentage: {bonusPercentage}%");
            else
                Console.WriteLine("Geen bonus (minder dan 2 afdelingen gewerkt).");

            Console.WriteLine("\nDruk op een toets om af te sluiten...");
            Console.ReadKey();
        }

        /// <summary>
        /// Vraagt aan de gebruiker hoeveel jaren in een bepaalde afdeling gewerkt is.
        /// </summary>
        static int VraagAantalJaren(string afdelingNaam)
        {
            Console.Write($"Aantal jaren gewerkt in {afdelingNaam} (0 als niet gewerkt): ");
            string input = Console.ReadLine();

            // Probeer te converteren, standaard 0 als lege input
            if (int.TryParse(input, out int jaren))
            {
                return Math.Max(0, jaren); // geen negatieve jaren
            }
            return 0;
        }
    }
}
