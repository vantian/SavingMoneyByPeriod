using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            LaunchApp(args);

            Console.Write("R to restart the app: ");
            string input = Console.ReadLine();
            if (input.Trim().ToUpper() == "R")
            {
                Main(args);
            }
        }

        static void LaunchApp(string[] args) {
            int totalTarget = 0;
            int totalWeeks = 0;
            int totalMaxSaving = 0;

            string inTotalTarget;
            Console.Write("Masukan jumlah target tabungan (dalam ratus ribuan): ");
            inTotalTarget = Console.ReadLine();

            string inTotalWeeks;
            Console.Write("Masukan berapa lama menabung (dalam minggu): ");
            inTotalWeeks = Console.ReadLine();

            string inMaxSaving;
            Console.Write("Masukan max jumlah tabungan: ");
            inMaxSaving = Console.ReadLine();

            totalTarget = Convert.ToInt32(inTotalTarget);
            totalWeeks = Convert.ToInt32(inTotalWeeks);
            totalMaxSaving = Convert.ToInt32(inMaxSaving);

            if ((totalMaxSaving * totalWeeks) < totalTarget)
            {
                Console.Write("max jumlah tabungan tidak mencukupi target");
            }
            else
            {

                int count = totalWeeks;
                int sum = totalTarget;
                Random g = new Random();

                int[] vals = new int[count];
                sum -= count;

                for (int i = 0; i < count - 1; ++i)
                {
                    vals[i] = g.Next(sum); //set each index to random with max sum
                }
                vals[count - 1] = sum; //set last index to total sum

                vals = vals.OrderBy(e => e).ToArray(); //reorder small to big
                for (int i = count - 1; i > 0; --i)
                {
                    vals[i] -= vals[i - 1]; //??
                }
                for (int i = 0; i < count; ++i) { ++vals[i]; } //??

                //round up
                var dictionary = vals.Select((value, index) => new { value, index })
                          .ToDictionary(pair => pair.index, pair => pair.value);
                foreach (var loopInt in dictionary)
                {
                    string strGetLastDigit = loopInt.Value.ToString();

                    var lastDigit = GetStringLastDigit(loopInt.Value);
                    if (lastDigit == 0)
                        continue;

                    //cari di array yang belum round up
                    int idxMinusValue = -1;
                    for (int i = vals.Count() - 1; i >= 0; i--)
                    {
                        int strFind = GetStringLastDigit(vals[i]);

                        if (lastDigit != 0)
                        {
                            idxMinusValue = i;
                        }
                        continue;
                    }


                    if (idxMinusValue >= 0) //if find below the value
                    {
                        //set to array
                        vals[loopInt.Key] -= lastDigit;
                        vals[idxMinusValue] += lastDigit;
                    }

                }

                //

                for (int i = 0; i < count; ++i)
                {
                    //Console.WriteLine(string.Format("{0}.000,-", vals[i].ToString("C", System.Globalization.CultureInfo.GetCultureInfo("id-ID"))));
                    Console.WriteLine(vals[i]);

                }
                Console.WriteLine("\n");
            }
        }

        static int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        static int GetStringLastDigit(int param)
        {
            string strGetLastDigit = param.ToString();
            int lastDigit = Convert.ToInt32(strGetLastDigit.Substring(strGetLastDigit.Length - 1, 1));
            return lastDigit;
        }
    }
}
