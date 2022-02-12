using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Normalizer : Algorithm
    {
        public Signal InputSignal { get; set; }
        public float InputMinRange { get; set; }
        public float InputMaxRange { get; set; }
        public Signal OutputNormalizedSignal { get; set; }

        public override void Run()
        {
            float normalized_signals;
            float max_value = InputSignal.Samples[0];
            float min_value = InputSignal.Samples[0];
            List<float> output_signals = new List<float>();
            for (int i = 1; i < InputSignal.Samples.Count; i++)
            { 
                if(InputSignal.Samples[i] > max_value)
                    max_value = InputSignal.Samples[i];
                if (InputSignal.Samples[i] < min_value)
                    min_value = InputSignal.Samples[i];
            }
            if (InputMinRange == 0 && InputMaxRange == 1)
            {
                for(int i = 0; i < InputSignal.Samples.Count; i++)
                {
                    normalized_signals = (InputSignal.Samples[i] - min_value)/ (max_value - min_value);
                    output_signals.Add(normalized_signals);
                }
                OutputNormalizedSignal = new Signal(output_signals, true);
            }
            else if(InputMinRange == -1 && InputMaxRange == 1)
            {
                for (int i = 0; i < InputSignal.Samples.Count; i++)
                {
                    normalized_signals = (2*(InputSignal.Samples[i] - min_value)/ (max_value - min_value))-1;
                    output_signals.Add(normalized_signals);
                }
                OutputNormalizedSignal = new Signal(output_signals, true);
            }
        }
    }
}
