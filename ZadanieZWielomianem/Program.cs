using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ZadanieZWielomianem
{
    class Program
    {
        static void Main(string[] args)
        {
            double[] lista1 = new double[] { 6,1,3};
            // y = a0 + a1x + a2x^2 + a3x^3 + … + anx^n
           
            double[] lista2 = new double[] { -5, -2, 5 };
            int[] lista3 = new int[] { -1, 0, 0, 0, -2 };
            int[] lista4 = new int[] { 5, 0, 4, 8 };

            double dowolnyTypNumeryczny = 3;

            Wielomian<double> wielomian1 = new Wielomian<double>(lista1);
            Wielomian<double> wielomian2 = new Wielomian<double>(lista2);
            Wielomian<int> wielomian3 = new Wielomian<int>(lista3);
            Wielomian<int> wielomian4 = new Wielomian<int>(lista3);

            Console.WriteLine("Wielomian 1 to: " + wielomian1.ToString());
            Console.WriteLine("Wielomian 2 to: " + wielomian2.ToString());
            Console.WriteLine("Wielomian 3 to: " + wielomian3.ToString());
            Console.WriteLine("Wielomian 4 to: " + wielomian4.ToString());

            Wielomian<int> wielomian5 = (wielomian4.Clone() as Wielomian<int>);
            Console.WriteLine("Skolonwany z wielomianu 4 wielomian 5 to: " + wielomian5.ToString());
            
            Console.WriteLine("Wartość wielomianu 1 dla x=4 to: " + wielomian1.ObliczenieY(4));

            Console.WriteLine("Wielomian 1 + wielomian 2 to: " + wielomian1+wielomian2.ToString());
            Console.WriteLine("Wielomian 3 - wielomian 4 to: " + (wielomian1-wielomian2).ToString());
            Console.WriteLine("Wielomian 3 * wielomian 5 to: " + (wielomian3 * wielomian5).ToString());
            Console.WriteLine("Wielomian 1 + dowolny typ numeryczny to: " + (wielomian1+dowolnyTypNumeryczny).ToString());
            Console.WriteLine("Wielomian 1 / dowolny typ numeryczny to: " + (wielomian1 /dowolnyTypNumeryczny).ToString());
            



            Console.ReadKey();
            
        }
    }


    class Wielomian<T>: ICloneable, IComparable<Wielomian<T>> where T : struct, IComparable 
        
    {
        public int stopien;
        public T[] wsp = null;
        
        public Wielomian()
        {
            stopien = 0;
            wsp = new T[1];
            wsp[0] = default (T);
        }

        public Wielomian(T[] wsp)
        {
            stopien = wsp.Length - 1;
            this.wsp = new T[wsp.Length];
            for(int i=0; i<wsp.Length; i++)
            {
                this.wsp[i] = wsp[i];
            }
        }

        public T this[int k]
        {
            get { return wsp[k]; }
            set { wsp[k] = value; }
        }

        public object Clone()
        {
            return new Wielomian<T>(wsp.ToArray());
        }

      

        public void MiejscaZerowe (Wielomian<T> wielomian)
        {
            T[] TablicaMiejscZerowych;
        }

        public void PunktyPrzecieciaWielomianow(Wielomian<T> wielomian1, Wielomian<T> wielomian2)
        {
            T[,] TablicaPunktowPrzeciecia;
            Wielomian<T> nowyWielomian = new Wielomian<T>();
            nowyWielomian = wielomian1 - wielomian2;
        }


        

        public double ObliczenieY(T x)
        {
            var u=(double)(object) x;
            
            double y=0;
            for(int i = 0; i < wsp.Length; i++)
            {
                y = y + (double.Parse(wsp[i].ToString()) * Math.Pow(double.Parse(x.ToString()),i));
            }
            return y;
        }


        public override string ToString()
        {
            StringBuilder stringbuilder = new StringBuilder("y=");
            for(int i =0; i<=stopien; i++)
            {
                
                if (wsp[i].CompareTo(default(T)) < 0)
                {
                    if (i == 0)
                    {
                        stringbuilder.Append(wsp[i]);
                    }
                    else
                        stringbuilder.Append(wsp[i] + "x^" + i);
                }
                if (wsp[i].CompareTo(default(T)) == 0)
                {
                    stringbuilder.Append("");
                }
                if (wsp[i].CompareTo(default(T)) > 0)
                {
                    if (i == 0)
                    {
                        stringbuilder.Append(wsp[i]);
                    }
                    else
                        stringbuilder.Append("+" + wsp[i] + "x^" + i);
                }
                
            }
            return stringbuilder.ToString();
        }

        public int CompareTo(Wielomian<T> other)
        {
            if (other == null) return 1;

            if (wsp.Length > other.wsp.Length) return 1;
            else if (wsp.Length< other.wsp.Length) return -1;
            else
            {
                for (int i = wsp.Length - 1; i > 0; i--)
                {
                    int result = wsp[i].CompareTo(other.wsp[i]);
                    if (result != 0) return result;
                }
            }
            return 0;
        }

        public static Wielomian<T> operator +(Wielomian<T> lewy, Wielomian<T> prawy)
        {
            int nowy_stopien;
            int mniejszy_stopien;

            if (lewy.wsp.Length > prawy.wsp.Length)
            {
                nowy_stopien = lewy.stopien;
                mniejszy_stopien = prawy.wsp.Length;
            }
            else
            {
                nowy_stopien = prawy.stopien;
                mniejszy_stopien = lewy.wsp.Length;
            }


            double[] nowy_wsp = new double[nowy_stopien + 1];
            T[] nowy_wsp_generic = new T[nowy_stopien + 1];
            

                for (int i = 0; i < mniejszy_stopien; i++)
            {
                nowy_wsp[i] = double.Parse(lewy.wsp[i].ToString()) + double.Parse(prawy.wsp[i].ToString());
                nowy_wsp_generic[i] = (T)Convert.ChangeType(nowy_wsp[i], typeof(T));
            }
            for (int j = mniejszy_stopien; j <= nowy_stopien; j++)
            {
                if (lewy.wsp.Length > prawy.wsp.Length)
                {
                    nowy_wsp[j] = double.Parse(lewy.wsp[j].ToString());
                    nowy_wsp_generic[j] = (T)Convert.ChangeType(nowy_wsp[j], typeof(T));
                }
                if (prawy.wsp.Length > lewy.wsp.Length)
                {
                    nowy_wsp[j] = double.Parse(prawy.wsp[j].ToString());
                    nowy_wsp_generic[j] = (T)Convert.ChangeType(nowy_wsp[j], typeof(T));
                }
            }
            
            return new Wielomian<T>(nowy_wsp_generic);
        }

        public static Wielomian<T> operator -(Wielomian<T> lewy, Wielomian<T> prawy)
        {
            int nowy_stopien;
            int mniejszy_stopien;

            if (lewy.wsp.Length > prawy.wsp.Length)
            {
                nowy_stopien = lewy.stopien;
                mniejszy_stopien = prawy.wsp.Length;
            }
            else
            {
                nowy_stopien = prawy.stopien;
                mniejszy_stopien = lewy.wsp.Length;
            }


            double[] nowy_wsp = new double[nowy_stopien + 1];
            T[] nowy_wsp_generic = new T[nowy_stopien + 1];


            for (int i = 0; i < mniejszy_stopien; i++)
            {
                nowy_wsp[i] = double.Parse(lewy.wsp[i].ToString()) - double.Parse(prawy.wsp[i].ToString());
                nowy_wsp_generic[i] = (T)Convert.ChangeType(nowy_wsp[i], typeof(T));
            }
            for (int j = mniejszy_stopien; j <= nowy_stopien; j++)
            {
                if (lewy.wsp.Length > prawy.wsp.Length)
                {
                    nowy_wsp[j] = -double.Parse(lewy.wsp[j].ToString());
                    nowy_wsp_generic[j] = (T)Convert.ChangeType(nowy_wsp[j], typeof(T));
                }
                if (prawy.wsp.Length > lewy.wsp.Length)
                {
                    nowy_wsp[j] = -double.Parse(prawy.wsp[j].ToString());
                    nowy_wsp_generic[j] = (T)Convert.ChangeType(nowy_wsp[j], typeof(T));
                }
            }

            return new Wielomian<T>(nowy_wsp_generic);
        }

        public static Wielomian<T> operator *(Wielomian<T> lewy, Wielomian<T> prawy)
        {
            int nowy_stopien;
            

            nowy_stopien = lewy.wsp.Length + prawy.wsp.Length;


            double[] nowy_wsp = new double[nowy_stopien + 1];
            T[] nowy_wsp_generic = new T[nowy_stopien + 1];

            for (int i = 0; i < lewy.wsp.Length; i++)
            {
                for (int j = 0; j < prawy.wsp.Length; j++)
                {
                    nowy_wsp[i + j] = nowy_wsp[i + j] + ((double.Parse(lewy.wsp[i].ToString())) * (double.Parse(prawy.wsp[j].ToString())));
                    nowy_wsp_generic[i + j] = (T)Convert.ChangeType(nowy_wsp[i + j], typeof(T));
                }
            }



            return new Wielomian<T>(nowy_wsp_generic);
        }


        public static Wielomian<T> operator +(Wielomian<T>lewy , T prawy)
        {
            Wielomian<T> wielomian = (lewy.Clone() as Wielomian<T>);
            double tymczasowa = double.Parse(lewy.wsp[0].ToString()) + double.Parse(prawy.ToString());
            wielomian[0]= (T)Convert.ChangeType(tymczasowa, typeof(T));
            return wielomian;
        }

        public static Wielomian<T> operator -(Wielomian<T> lewy, T prawy)
        {
            Wielomian<T> wielomian = (lewy.Clone() as Wielomian<T>);
            double tymczasowa = double.Parse(lewy.wsp[0].ToString()) - double.Parse(prawy.ToString());
            wielomian[0] = (T)Convert.ChangeType(tymczasowa, typeof(T));
            return wielomian;
        }

        public static Wielomian<T> operator *(Wielomian<T> lewy, T prawy)
        {
            Wielomian<T> wielomian = (lewy.Clone() as Wielomian<T>);
            T[] nowy_wsp_generic = new T[lewy.wsp.Length + 1];
            double[] nowy_wsp = new double[lewy.wsp.Length + 1];
            for (int i = 0; i < lewy.wsp.Length; i++)
            {
                    nowy_wsp[i] = ((double.Parse(lewy.wsp[i].ToString())) * (double.Parse(prawy.ToString())));
                    wielomian[i] = (T)Convert.ChangeType(nowy_wsp[i], typeof(T));
                
            }
            return wielomian;
        }

        public static Wielomian<T> operator /(Wielomian<T> lewy, T prawy)
        {
            Wielomian<T> wielomian = (lewy.Clone() as Wielomian<T>);
            T[] nowy_wsp_generic = new T[lewy.wsp.Length + 1];
            double[] nowy_wsp = new double[lewy.wsp.Length + 1];
            for (int i = 0; i < lewy.wsp.Length; i++)
            {
                nowy_wsp[i] = ((double.Parse(lewy.wsp[i].ToString())) / (double.Parse(prawy.ToString())));
                wielomian[i] = (T)Convert.ChangeType(nowy_wsp[i], typeof(T));

            }
            return wielomian;
        }
        

    }
}
