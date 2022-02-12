using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Shifter : Algorithm
    {
        public Signal InputSignal { get; set; }
        public int ShiftingValue { get; set; }
        public Signal OutputShiftedSignal { get; set; }
        public List<int> ShiftedIndicies = new List<int>(); 
        public List<float> Samples = new List<float>();
        public override void Run()
        {
           int count = InputSignal.Samples.Count();
           OutputShiftedSignal = new Signal(Samples, true);
           for (int i = 0; i < count; i++)
           {
               OutputShiftedSignal.Samples.Add(InputSignal.Samples[i]);
               OutputShiftedSignal.SamplesIndices.Add(InputSignal.SamplesIndices[i] - ShiftingValue);
           }  
           //OutputShiftedSignal = new Signal(Samples, ShiftedIndicies, false);
        }
    }
}
