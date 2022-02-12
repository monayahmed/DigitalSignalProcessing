using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Folder : Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputFoldedSignal { get; set; }
        public List<float> SampleValues = new List<float>();
        public List<float> FoldedSampleValues = new List<float>(); 
        public List<int> SampleIndices = new List<int>();
        //public List<int> ShiftedSampleIndices = new List<int>();
        
        public override void Run()
        {
            SampleValues = InputSignal.Samples;
            SampleIndices = InputSignal.SamplesIndices;
            int count = (InputSignal.Samples.Count())-1;
            OutputFoldedSignal = new Signal(FoldedSampleValues, false);
            for (int i = 0; i <=count; i++) 
            { 
                OutputFoldedSignal.Samples.Add(SampleValues[count -i]);
            }
            for (int i = 0; i <=count; i ++)
            {
                OutputFoldedSignal.SamplesIndices.Add(SampleIndices[i]);
            }

            //OutputFoldedSignal = new Signal(ShiftedSampleValues, ShiftedSampleIndices, false);
        }
    }
}
