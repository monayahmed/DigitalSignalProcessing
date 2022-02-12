using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;
using DSPAlgorithms;
namespace DSPAlgorithms.Algorithms
{
   public class TimeDelay:Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public float InputSamplingPeriod { get; set; }
        public float OutputTimeDelay { get; set; }
        public List<float> correlated_value = new List<float>();
        public override void Run()
        {
            DirectCorrelation directCorrelation = new DirectCorrelation();
            directCorrelation.InputSignal1 = InputSignal1;
            directCorrelation.Run();
            for(int j=0; j<InputSignal1.Samples.Count; j++)
            {
                correlated_value.Add(directCorrelation.OutputNormalizedCorrelation[j]);
            }
            float max = Math.Abs(correlated_value[0]);
            int index = -1;
            for (int i = 1; i < InputSignal1.Samples.Count(); i++) 
            {
                if (Math.Abs(correlated_value[i]) > max)
                {
                    max = Math.Abs(correlated_value[i]);
                    index = i;
                }
                else
                    continue;
            }
            if(index == -1)
            {
                OutputTimeDelay = 0; //maximun value found at index 0. //0 * Ts.
            }    
            else
            {
                OutputTimeDelay = InputSamplingPeriod * index;
            }
        }
    }
}
