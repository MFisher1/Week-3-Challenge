using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week3ch
{

    public class Collatz
    {
        public int number;
        public int hops;

        public Collatz(int number, int hops)
        {
            this.hops = hops;
            this.number = number;
        }
    }

    class Program
    {
        static bool isPrime(long number, List<int> pnums)
        {
            bool yep = true;
            int canDiv = 0;
            if (number <= 2) return true;
            else
            {
                int counter = 1;
                while (yep)
                {
                    if (counter < pnums.Count-1)
                    {
                        if (number % pnums[counter] == 0) canDiv++;
                        counter++;
                        if (canDiv>0) yep = false;
                    }
                    else
                        yep = false;
                }  
            }
            return canDiv < 1;
        }

        static bool isEven(long number)
        {
            return number % 2 == 0;
        }

        static Collatz LongestCollatzSequence()
        {
          List<Collatz> chains = new List<Collatz>();
            long number = 0;
            int hop = 0;
            for (int count = 1; count < 1000000; count+=2)
            {
                number = count;
                hop = 0;
                while (number != 1)
                {                  
                    if (isEven(number))
                    {
                     number = number / 2;
                     
                     var n = chains.Where(x => x.number == number);
                     if (n.Any())
                     {
                         hop = hop + n.First().hops;
                         break;
                     }

                    }
                      else 
                        number = 3 * number + 1;               
                    hop++;
                }
             chains.Add(new Collatz(count, hop));
            }
            Console.WriteLine("Average: " + chains.Select(x => x.hops).Average());
            return chains.OrderBy(x=>x.hops).Last();
        }

        static void FindNPrimes(int maxPrime)
        {
            List<int> pnum = new List<int>();
            pnum.Add(1);
            pnum.Add(2);
            int pCount = 1;
            int counter = 3;
            while (pCount < maxPrime)
            {
                counter += 2;
                if (isPrime(counter, pnum))
                {
                    pnum.Add(counter);
                    pCount++;
                }
            }
            Console.WriteLine(maxPrime + " prime number is " + counter);
        }

        static void EvenFibonacciSequencer(int maxValue)
        {
            List<int> fib = new List<int>();
            fib.Add(1);
            fib.Add(2);
            int count = 2;
            while (count < maxValue)
            {
                fib.Add(fib[count - 1] + fib[count - 2]);
                count++;
            }
            Console.WriteLine("Fibonacci sequence of " + maxValue +" numbers:" + string.Join(", ", fib));
        }



        static void Main(string[] args)
        {
            Collatz cl = LongestCollatzSequence();
            EvenFibonacciSequencer(10);
            FindNPrimes(10001);
           
            Console.WriteLine("Longest Collatz Sequence is generated from number " + cl.number + " and has " + cl.hops + " elements");
            Console.ReadLine();
        }
    }
}
