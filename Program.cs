using System.Xml.Linq;

namespace Lab1VentasAutos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            XDocument doc = XDocument.Load("VentasAutos.xml");
            var ventas = doc.Descendants("venta");

            
            string nombreConcesionario = doc.Root.Attribute("nombre").Value;

            int ancho = 40;
            int espacioIzq = (ancho - 2 - nombreConcesionario.Length) / 2;
            int espacioDer = ancho - 2 - nombreConcesionario.Length - espacioIzq;
            string linea = new string('*', ancho);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(linea);
            Console.WriteLine("*" + new string(' ', espacioIzq) + nombreConcesionario + new string(' ', espacioDer) + "*");
            Console.WriteLine(linea);
            Console.ResetColor();
            Console.WriteLine();

            
            string textoTotal = "Total de ventas registradas:";
            string cantidad = ventas.Count().ToString();

            Console.WriteLine(new string(' ', (ancho - textoTotal.Length) / 2) + textoTotal);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(new string(' ', (ancho - cantidad.Length) / 2) + cantidad);
            Console.ResetColor();
            Console.WriteLine();

            
            MostrarTitulo("1. Vendedores");
            var vendedores = ventas.Select(v => v.Element("vendedor").Value).Distinct();

            foreach (var vendedor in vendedores)
            {
                MostrarDato(vendedor);
            }

            Pausa();

            
            MostrarTitulo("2. Ventas de Mora, Andrea");
            var ventasAndrea = ventas.Where(v => v.Element("vendedor").Value == "Mora, Andrea");

            foreach (var venta in ventasAndrea)
            {
                MostrarDato(venta.Attribute("id").Value + " - " +
                            venta.Element("marca").Value + " " +
                            venta.Element("modelo").Value + " - $" +
                            venta.Element("precio").Value);
            }

            Pausa();

            
            MostrarTitulo("3. Automóvil con el precio más alto");
            var masCaro = ventas.OrderByDescending(v => (decimal)v.Element("precio")).First();

            MostrarDato(masCaro.Element("marca").Value + " " +
                        masCaro.Element("modelo").Value + " (" +
                        masCaro.Element("anio").Value + ") - $" +
                        masCaro.Element("precio").Value);

            Pausa();

            
            MostrarTitulo("4. Automóvil con el precio más bajo");
            var masBarato = ventas.OrderBy(v => (decimal)v.Element("precio")).First();

            MostrarDato(masBarato.Element("marca").Value + " " +
                        masBarato.Element("modelo").Value + " (" +
                        masBarato.Element("anio").Value + ") - $" +
                        masBarato.Element("precio").Value);

            Pausa();

            
            MostrarTitulo("5. Automóviles con año posterior a 2020");
            var autosRecientes = ventas.Where(v => (int)v.Element("anio") > 2020)
                                       .OrderBy(v => (int)v.Element("anio"));

            foreach (var venta in autosRecientes)
            {
                MostrarDato(venta.Attribute("id").Value + " - " +
                            venta.Element("marca").Value + " " +
                            venta.Element("modelo").Value + " (" +
                            venta.Element("anio").Value + ") - $" +
                            venta.Element("precio").Value);
            }
        }

        static void MostrarTitulo(string titulo)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(">> " + titulo + " <<");
            Console.ResetColor();
            Console.WriteLine();
        }

        static void MostrarDato(string texto)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("   " + texto);
            Console.ResetColor();
        }

        static void Pausa()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ResetColor();
            Console.ReadKey();
            Console.Clear();
        }
    }
}