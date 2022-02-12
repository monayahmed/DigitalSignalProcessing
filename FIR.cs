using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;
using DSPAlgorithms;
using System.IO;
namespace DSPAlgorithms.Algorithms
{
    public class FIR : Algorithm
    {
        public Signal InputTimeDomainSignal { get; set; } //input signal.
        public FILTER_TYPES InputFilterType { get; set; } //filter type.
        public float InputFS { get; set; } //sampling frequency.
        public float? InputCutOffFrequency { get; set; } //fc
        public float? InputF1 { get; set; } //fc1
        public float? InputF2 { get; set; } //fc2
        public float InputStopBandAttenuation { get; set; }
        public float InputTransitionBand { get; set; }
        public Signal OutputHn { get; set; }
        public Signal OutputYn { get; set; }
        public List<float> list = new List<float>();
        public List<float> temp = new List<float>();
        public float equation;
        public float equation2;
        public float NormalizedTransitionBand; //fc1'
        public float NormalizedTransitionBand2; //fc2'
        public float TransitionWidth;
        public float Result;
        public float Width;
        public float WindowFunction;
        public float impulseResponse;
        public int index;
        public float iterator;
        public float N;
        public List<float> Convolution(Signal InputTimeDomainSignal, Signal OutputHn)
        {
            DirectConvolution ConvolvedSignal = new DirectConvolution();
            ConvolvedSignal.InputSignal1 = new Signal(InputTimeDomainSignal.Samples, false);
            ConvolvedSignal.InputSignal2 = new Signal(OutputHn.Samples, false);
            ConvolvedSignal.Run();
            return ConvolvedSignal.OutputConvolvedSignal.Samples;
        }
        public float CheckFilterType(FILTER_TYPES type, int index, float InputCutOffFrequency, float InputTransitionBand, float InputF1, float InputF2)
        {
            if (type == FILTER_TYPES.LOW)
            {
                NormalizedTransitionBand = (float)(InputCutOffFrequency + (InputTransitionBand / 2));
                NormalizedTransitionBand /= InputFS;
                if (index == 0)
                    equation = 2 * NormalizedTransitionBand;
                else
                    equation = (float)((2 * NormalizedTransitionBand) * ((Math.Sin(index * 2 * (Math.PI) * NormalizedTransitionBand))
                        / (index * 2 * Math.PI * NormalizedTransitionBand)));
                return equation;
            }
            else if (type == FILTER_TYPES.HIGH)
            {
                NormalizedTransitionBand = (float)(InputCutOffFrequency + (InputTransitionBand / 2)); //fc'
                NormalizedTransitionBand /= InputFS;
                if (index == 0)
                    equation = 1 - (2 * NormalizedTransitionBand);
                else
                    equation = (float)((-2 * NormalizedTransitionBand) * (Math.Sin(index * 2 * Math.PI * NormalizedTransitionBand)
                       / (index * 2 * Math.PI * NormalizedTransitionBand)));
                return equation;
            }
            else if (type == FILTER_TYPES.BAND_PASS)
            {
                NormalizedTransitionBand = (float)(InputF1 - (InputTransitionBand / 2));
                NormalizedTransitionBand /= InputFS;
                NormalizedTransitionBand2 = (float)(InputF2 + (InputTransitionBand / 2));
                NormalizedTransitionBand2 /= InputFS;
                if (index == 0)
                {
                    equation = 2 * (NormalizedTransitionBand2 - NormalizedTransitionBand);
                    return equation;
                }
                else
                {
                    equation = (float)((2 * NormalizedTransitionBand) * (Math.Sin(index * 2 * Math.PI * NormalizedTransitionBand)
                         / (index * 2 * Math.PI * NormalizedTransitionBand)));
                    equation2 = (float)((2 * NormalizedTransitionBand2) * (Math.Sin(index * 2 * Math.PI * NormalizedTransitionBand2)
                         / (index * 2 * Math.PI * NormalizedTransitionBand2)));
                    return (equation2 - equation);
                }

            }
            else
            {
                NormalizedTransitionBand = (float)(InputF1 - (InputTransitionBand / 2));
                NormalizedTransitionBand = NormalizedTransitionBand / InputFS;
                NormalizedTransitionBand2 = (float)(InputF2 + (InputTransitionBand / 2));
                NormalizedTransitionBand2 = NormalizedTransitionBand2 / InputFS;
                if (index == 0)
                {
                    equation = 1 - (2 * (NormalizedTransitionBand2 - NormalizedTransitionBand));
                    return equation;
                }
                else
                {
                    equation = (float)((2 * NormalizedTransitionBand) * (Math.Sin(index * 2 * Math.PI * NormalizedTransitionBand)
                         / (index * 2 * Math.PI * NormalizedTransitionBand)));
                    equation2 = (float)((2 * NormalizedTransitionBand2) * (Math.Sin(index * 2 * Math.PI * NormalizedTransitionBand2)
                         / (index * 2 * Math.PI * NormalizedTransitionBand2)));
                    return (equation - equation2);
                }

            }
        }
        public override void Run()
        {
            List<float> output = new List<float>();
            Signal new_signal = new Signal(output, false);
            OutputHn = new_signal;
            DirectConvolution ConvolvedSignal = new DirectConvolution();
            ConvolvedSignal.InputSignal1 = new Signal(InputTimeDomainSignal.Samples, false);
            Width = InputTransitionBand / InputFS; //delta f.
            if (InputStopBandAttenuation <= 44)
            {
                N = (3.1f / Width);
                if (N % 2 == 0) //If N --> even.
                    N++;
                iterator = (int)(N - 1) / 2;
                if (InputFilterType == FILTER_TYPES.LOW || InputFilterType == FILTER_TYPES.HIGH)
                {
                    InputF1 = 0; InputF2 = 0;
                }
                else
                    InputCutOffFrequency = 0;
                for (index = (int)(iterator * -1); index <= (int)(iterator); index++)
                {
                    impulseResponse = CheckFilterType(InputFilterType, index, (float)InputCutOffFrequency, InputTransitionBand, (float)InputF1, (float)InputF2);
                    WindowFunction = (float)(0.5f + (0.5f * (Math.Cos(Math.PI * 2 * index) / N)));
                    Result = WindowFunction * impulseResponse;
                    Console.WriteLine(Result);
                    OutputHn.Samples.Add((float)Result);
                    OutputHn.SamplesIndices.Add(index);
                }
                list = Convolution(InputTimeDomainSignal, OutputHn);
                OutputYn = new Signal(list, false);
                int End = list.Count() - ((int)(iterator));
                for (int i = (int)(iterator * -1); i < End; i++)
                {
                    //Console.WriteLine(i);
                    OutputYn.SamplesIndices.Add(i);
                }
            }
            else if (InputStopBandAttenuation <= 53)
            {
                N = (3.3f / Width);
                N = (float)(Math.Round(N));
                if (N % 2 == 0)
                    N++;
                iterator = (int)(N - 1) / 2;
                if (InputFilterType == FILTER_TYPES.LOW || InputFilterType == FILTER_TYPES.HIGH)
                {
                    InputF1 = 0; InputF2 = 0;
                }
                else
                    InputCutOffFrequency = 0;
                for (index = (int)(iterator * -1); index <= (int)iterator; index++)
                {
                    impulseResponse = CheckFilterType(InputFilterType, index, (float)InputCutOffFrequency, InputTransitionBand, (float)InputF1, (float)InputF2);
                    WindowFunction = (float)(0.54f + 0.46f * (Math.Cos(Math.PI * 2 * index / N)));
                    Result = WindowFunction * impulseResponse;
                    //Console.WriteLine(Result);
                    OutputHn.Samples.Add(Result);
                    OutputHn.SamplesIndices.Add(index);
                }
                list = Convolution(InputTimeDomainSignal, OutputHn);
                List<float> temp = new List<float>();
                Signal tempsignal = new Signal(temp, false);
                OutputYn = tempsignal;
                OutputYn.Samples = list;
                int End = list.Count() - ((int)(iterator));
                for (int i = (int)(iterator * -1); i < End; i++)
                {
                    OutputYn.SamplesIndices.Add(i);
                }
            }
            else if (InputStopBandAttenuation <= 74)  //InputStopBandAttenuation >= 54 &&
            {
                N = (5.5f / Width);
                N = (float)(Math.Round(N));
                if (N % 2 == 0)
                    N++;
                iterator = (int)(N - 1) / 2;
                if (InputFilterType == FILTER_TYPES.LOW || InputFilterType == FILTER_TYPES.HIGH)
                {
                    InputF1 = 0; InputF2 = 0;
                }
                else
                    InputCutOffFrequency = 0;
                for (index = (int)(iterator * -1); index <= (int)iterator; index++)
                {
                    impulseResponse = CheckFilterType(InputFilterType, index, (float)InputCutOffFrequency, InputTransitionBand, (float)InputF1, (float)InputF2);
                    WindowFunction = (float)(0.42f + (0.5f * (Math.Cos((2 * Math.PI * index) / (N - 1))))
                             + (0.08f * (Math.Cos((4 * Math.PI * index) / (N - 1)))));
                    Result = WindowFunction * impulseResponse;
                    Console.WriteLine(Result);
                    OutputHn.Samples.Add((float)Result);
                    OutputHn.SamplesIndices.Add(index);
                }
                list = Convolution(InputTimeDomainSignal, OutputHn);
                List<float> temp = new List<float>();
                Signal tempsignal = new Signal(temp, false);
                OutputYn = tempsignal;
                OutputYn.Samples = list;
                int End = list.Count() - ((int)(iterator));
                for (int i = (int)(iterator * -1); i < End; i++)
                {
                    OutputYn.SamplesIndices.Add(i);
                }
            }
            StreamWriter WriteInFile = new StreamWriter("FIRCoefficients.txt");
            for (int i = 0; i < OutputHn.Samples.Count(); i++)
            {
                WriteInFile.WriteLine("Coefficient #" + i + ":"+ OutputHn.Samples[i]);
            }
            WriteInFile.WriteLine("----------------------------------------------------------------");
            WriteInFile.Close();

        }
    }
}
