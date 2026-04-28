using System;
using System.Collections.Generic;

namespace CalculadoraAvanzada
{
    // Delegado para las operaciones matemáticas
    public delegate T OperacionMatematica<T>(T a, T b);

    // Clase genérica
    public class CalculadoraGenerica<T>
    {
        private List<T> numeros = new List<T>();

        public void AgregarNumero(T numero)
        {
            numeros.Add(numero);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n[+] Número {numero} agregado a la lista exitosamente.");
            Console.ResetColor();
        }

        public int Cantidad => numeros.Count;

        public T EjecutarOperacion(OperacionMatematica<T> operacion)
        {
            if (numeros.Count < 2)
            {
                throw new InvalidOperationException("Se requieren al menos dos números en la lista para operar.");
            }

            T resultado = numeros[0];
            for (int i = 1; i < numeros.Count; i++)
            {
                resultado = operacion(resultado, numeros[i]);
            }
            return resultado;
        }

        public void LimpiarLista()
        {
            numeros.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n[i] La lista ha sido limpiada.");
            Console.ResetColor();
        }
    }

    class Program
    {
        static T Sumar<T>(T a, T b) => (dynamic)a + (dynamic)b;
        static T Restar<T>(T a, T b) => (dynamic)a - (dynamic)b;
        static T Multiplicar<T>(T a, T b) => (dynamic)a * (dynamic)b;
        static T Dividir<T>(T a, T b)
        {
            if ((dynamic)b == 0)
            {
                throw new DivideByZeroException("No se puede dividir por cero.");
            }
            return (dynamic)a / (dynamic)b;
        }

        static void Main(string[] args)
        {
            CalculadoraGenerica<double> calculadora = new CalculadoraGenerica<double>();
            bool salir = false;

            while (!salir)
            {
                // Limpiamos la consola en cada ciclo para dar efecto de "pantalla"
                Console.Clear();

                // Título principal con diseño simple
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("========================================");
                Console.WriteLine("       CALCULADORA AVANZADA - ITLA      ");
                Console.WriteLine("========================================");
                Console.ResetColor();

                Console.WriteLine($"\n  -> Números actuales en memoria: {calculadora.Cantidad}\n");

                Console.WriteLine("  [1] Agregar número");
                Console.WriteLine("  [2] Sumar todos");
                Console.WriteLine("  [3] Restar secuencialmente");
                Console.WriteLine("  [4] Multiplicar todos");
                Console.WriteLine("  [5] Dividir secuencialmente");
                Console.WriteLine("  [6] Limpiar lista");
                Console.WriteLine("  [7] Salir\n");

                Console.Write("  Elige una opción: ");
                string opcion = Console.ReadLine();

                Console.WriteLine(); // Espacio visual

                try
                {
                    switch (opcion)
                    {
                        case "1":
                            Console.Write("  Ingresa un número: ");
                            double num = double.Parse(Console.ReadLine());
                            calculadora.AgregarNumero(num);
                            break;
                        case "2":
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"  [=] Resultado de la Suma: {calculadora.EjecutarOperacion(Sumar)}");
                            Console.ResetColor();
                            break;
                        case "3":
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"  [=] Resultado de la Resta: {calculadora.EjecutarOperacion(Restar)}");
                            Console.ResetColor();
                            break;
                        case "4":
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"  [=] Resultado de la Multiplicación: {calculadora.EjecutarOperacion(Multiplicar)}");
                            Console.ResetColor();
                            break;
                        case "5":
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"  [=] Resultado de la División: {calculadora.EjecutarOperacion(Dividir)}");
                            Console.ResetColor();
                            break;
                        case "6":
                            calculadora.LimpiarLista();
                            break;
                        case "7":
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("  Saliendo del programa... ¡Hasta luego!");
                            Console.ResetColor();
                            salir = true;
                            continue; // Salta la pausa final si el usuario quiere salir
                        default:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("  [!] Opción no válida. Intenta de nuevo.");
                            Console.ResetColor();
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("  [X] Error de Formato: Debes ingresar un valor numérico válido.");
                    Console.ResetColor();
                }
                catch (InvalidOperationException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"  [X] Error: {ex.Message}");
                    Console.ResetColor();
                }
                catch (DivideByZeroException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"  [X] Error: {ex.Message}");
                    Console.ResetColor();
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"  [X] Ocurrió un error inesperado: {ex.Message}");
                    Console.ResetColor();
                }

                // Pausa para que el usuario vea el resultado de lo que acaba de hacer
                Console.WriteLine("\n  Presiona cualquier tecla para continuar...");
                Console.ReadKey();
            }
        }
    }
}