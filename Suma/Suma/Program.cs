using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suma
{
    class Program
    {
        class Numeros
        {
             public int numero1{get;set;}
             public int numero2 { get; set; }      
         }
        static void Main(string[] args)
        {
            int a = 5;
            int b = 6;
            Numeros num = new Numeros();
            num.numero1 = a;
            num.numero2 = b;
            Task resultado = null;
            try
            {

                Task<int> dato1 = Task.Factory.StartNew((parametro) =>
                 {
                     Numeros x = (Numeros)parametro;
                     x.numero1 += 5;
                     x.numero2 -= 2;
                     int z = x.numero2 + x.numero1;
                     return z;
                 }, num);

                Task<int> dato2 = Task.Factory.StartNew((parametro) =>
                {
                    int z = (int)parametro - (int)parametro;

                    return z;
                }, dato1.Result);

                 resultado = Task.Factory.ContinueWhenAll(new Task<int>[] { dato1, dato2 }, (predecesores) =>
                  {
                      Console.WriteLine("la divicion es " + predecesores[0].Result / (predecesores[1].Result+1));

                  });
                Task.WaitAll(dato1, dato2, resultado);
            }catch(AggregateException ae)
            {
                Console.WriteLine(" la tarea finalizo con estado " + resultado.Status);
            }
                Console.WriteLine("presione entrer para continuar");
            Console.ReadKey();


        

           
        }
    }
}
