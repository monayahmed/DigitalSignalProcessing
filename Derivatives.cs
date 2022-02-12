using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPAlgorithms.Algorithms
{
    public class Derivatives : Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal FirstDerivative { get; set; }
        public Signal SecondDerivative { get; set; }
        public List<float> FirstDerivativeList = new List<float>();
        public List<float> SecondDerivativeList = new List<float>();
        public float FirstDerv;
        public float SecondDerv;
        int j;
        public override void Run()
        {
            for (int i = 1; i < InputSignal.Samples.Count(); i++)
            {
                FirstDerv = InputSignal.Samples[i] - InputSignal.Samples[i-1];
                FirstDerivativeList.Add(FirstDerv);
            }
            Console.WriteLine(FirstDerivativeList.Count);
            for (j = 0; j < InputSignal.Samples.Count()-1; j++)//5 4
            {   //0 1 2 3 4 5
                //1 2 3 4 5 6 count == 6;
                if(j == 0)
                    SecondDerv = InputSignal.Samples[j + 1] - (2 * InputSignal.Samples[j]);
                else
                    SecondDerv = InputSignal.Samples[j+1] - (2*InputSignal.Samples[j]) + InputSignal.Samples[j-1] ;
                SecondDerivativeList.Add(SecondDerv);
            }
            FirstDerivative = new Signal(FirstDerivativeList, false);
            SecondDerivative = new Signal(SecondDerivativeList, false);
        }
    }
}