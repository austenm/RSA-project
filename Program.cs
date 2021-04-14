using System;
using System.Numerics;

namespace P4
{
    class Program
    {
        public static int gcd(int a, int b)
        {
            int c = 0;
            while(b>0)
            {
                c = a;
                a = b;
                b = c % b;
            }
            return a;
        }

        public static BigInteger divrem(BigInteger a, BigInteger b)
        {
            BigInteger r = 1;
            BigInteger q;
            while (r > 0)
            {    
                q = BigInteger.DivRem(a, b, out BigInteger remainder);
                r = remainder;
                Console.WriteLine("{0}, {1}, {2}, {3}", a, b, q, r);
                a = b;
                b = r;
                if (r == 1)
                {
                    break;
                }
            }
            return r;
        }

        public static BigInteger EEA(BigInteger E, BigInteger phiN)
        {
            BigInteger X = 0;
            BigInteger newX = 1;
            BigInteger A = E;
            BigInteger B = phiN;
            BigInteger quo = 0;
            BigInteger temp = 0;
            
            while (A != 0)
            {
                quo = BigInteger.Divide(B, A);
                temp = newX; 
                newX = X - quo * newX; 
                X = temp;
                temp = A; 
                A = B - quo * A; 
                B = temp;
            }
            return X;
        }

                static void Main(string[] args)
        {
            string[] theGoods = Environment.GetCommandLineArgs();
            BigInteger P = BigInteger.Pow(2, Int32.Parse(theGoods[1]))- Int32.Parse(theGoods[2]);
            BigInteger Q = BigInteger.Pow(2, Int32.Parse(theGoods[3]))- Int32.Parse(theGoods[4]);
            BigInteger N = P * Q;
            BigInteger phiN = (P - 1) * (Q - 1);
            BigInteger cipher = BigInteger.Parse(theGoods[5]);
            BigInteger plain = BigInteger.Parse(theGoods[6]);
            int E = 65537;

            BigInteger encrypted = BigInteger.ModPow(plain, E, N);
            
            BigInteger d = EEA(E, phiN);
            BigInteger dCheck = (E * d) % phiN;
            BigInteger decrypted = BigInteger.ModPow(cipher, d, N);
            Console.Write($"{decrypted.ToString()},");
            Console.Write($"{encrypted.ToString()}");
        }
    }
}
