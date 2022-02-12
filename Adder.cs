using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Adder : Algorithm
    {
        public List<Signal> InputSignals { get; set; }  //List of class signals.
        public Signal OutputSignal { get; set; }
        //number of input signals --> equal.
        public override void Run()
        {
            float added_signals;
            List<float> output_signals = new List<float>();
           for(int i=0; i< InputSignals[0].Samples.Count; i++)  
            {
                added_signals = InputSignals[0].Samples[i] + InputSignals[1].Samples[i];
                output_signals.Add(added_signals);
            }
            OutputSignal = new Signal(output_signals, true);
        }
    }
}