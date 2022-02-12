using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace DSPAlgorithms.Algorithms
{
    public class DCT : Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }
        double sum;
        double summation;
        double finalResult;
        float pi;
        public override void Run()
        {
            List<float> list = new List<float>();
            OutputSignal = new Signal(list, false);
            pi = (float)(Math.PI);
            for (int k = 0; k < InputSignal.Samples.Count(); k++) 
            {
                summation = 0;
                for (int n = 0; n < InputSignal.Samples.Count(); n++)
                {
                    sum = (((2 * n) + 1) * k * (Math.PI)) / (2 * InputSignal.Samples.Count());
                    summation += (InputSignal.Samples[n] * (Math.Cos(sum)));
                }
                if (k == 0) 
                    finalResult = (Math.Sqrt(1.0f / InputSignal.Samples.Count())) * summation;
                else
                    finalResult = (Math.Sqrt(2.0f / InputSignal.Samples.Count())) * summation;
                Console.WriteLine(finalResult);
                OutputSignal.Samples.Add((float)finalResult);
            }
            int M = 5;
            StreamWriter WriteInFile = new StreamWriter("DCT.txt");
            for (int i = 0; i < M; i++)
            {
                WriteInFile.WriteLine(i + ": " + OutputSignal.Samples[i]);
            }
            WriteInFile.WriteLine("----------------------------------------------------------------");
            WriteInFile.Close();
        }
    }
}