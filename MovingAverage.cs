using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class MovingAverage : Algorithm
    {
        public Signal InputSignal { get; set; }
        public int InputWindowSize { get; set; }
        public Signal OutputAverageSignal { get; set; }
        public List<float> AvgSignal = new List<float>();
        public override void Run()
        {
            int count = InputSignal.Samples.Count();
            int count1= (InputWindowSize - 1) / 2;
            OutputAverageSignal = new Signal(AvgSignal, false);
            float result;
            for (int i = count1; i < count - count1; i++)
            {
                result = InputSignal.Samples[i];
                for (int j = 1; j <= count1; j++)
                {
                    result += (InputSignal.Samples[i - j] + InputSignal.Samples[i + j]);
                }
                result /= InputWindowSize;
                OutputAverageSignal.Samples.Add(result);
            }
        }
    }
}
