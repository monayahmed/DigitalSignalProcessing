using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
	public class QuantizationAndEncoding : Algorithm
	{
		// You will have only one of (InputLevel or InputNumBits), the other property will take a negative value
		// If InputNumBits is given, you need to calculate and set InputLevel value and vice versa
		public int InputLevel { get; set; } //number of levels.
		public int InputNumBits { get; set; } //number of bits --> Log number of levels.
		public Signal InputSignal { get; set; } //Samle signals.
		public Signal OutputQuantizedSignal { get; set; } //Final output.
		public List<int> OutputIntervalIndices { get; set; } //Interval level (el level eli hahot fih kol sample)
		public List<string> OutputEncodedSignal { get; set; } //el level bel binary ba3d el encoding.
		public List<float> OutputSamplesError { get; set; } //el error ely hasabto l kol sample.

		public List<float> StartInterval = new List<float>(); //carry every interval.
		public List<float> EndInterval = new List<float>(); //carry every interval.
		public List<float> Midpoints = new List<float>();
		public List<float> SignalResult = new List<float>();
		public List<float> Sample_Error = new List<float>();
		public List<int> IntervalIndex = new List<int>();
		public List<string> Encoded = new List<string>();
		float delta;
		float max_value, EndOfEachInterval;
		int j;
		public override void Run()
		{
			if (InputNumBits <= 0)
			{
				InputNumBits = (int) Math.Log(InputLevel, 2);
			}
			else if (InputLevel <= 0)
			{
				InputLevel = (int)Math.Pow(2, InputNumBits);
			}
			delta = (InputSignal.Samples.Max() - InputSignal.Samples.Min()) / InputLevel;
			max_value = InputSignal.Samples.Max();
			
			StartInterval.Add(InputSignal.Samples.Min());
			EndOfEachInterval = (InputSignal.Samples.Min() + delta);
			EndInterval.Add(EndOfEachInterval);

			for (int i = 0; i < InputLevel-1; i++)
			{
				StartInterval.Add(EndOfEachInterval);
				EndOfEachInterval = EndOfEachInterval + delta;
				EndInterval.Add(EndOfEachInterval);
			}
			for (int i = 0; i < InputLevel; i++)
			{
				//Midpoint = (StartInterval[i] + EndInterval[i]) / (float)2;
				Midpoints.Add((StartInterval[i] + EndInterval[i]) / 2);
			}
			for (int i = 0; i < InputSignal.Samples.Count; i++)
			{
				for (j = 0; j < InputLevel; j++)
				{
					if (InputSignal.Samples[i] >= StartInterval[j] && InputSignal.Samples[i] <= EndInterval[j] + 0.001)
					{

						SignalResult.Add(Midpoints[j]);
						IntervalIndex.Add(j + 1);
						Sample_Error.Add(Midpoints[j] - InputSignal.Samples[i]);
						Encoded.Add(Convert.ToString(j, 2).PadLeft(InputNumBits, '0'));
					}
				}
			}
			
			OutputIntervalIndices = IntervalIndex;
			OutputSamplesError = Sample_Error;
			OutputEncodedSignal = Encoded;
			OutputQuantizedSignal = new Signal(SignalResult, false);

		}
	}
}
