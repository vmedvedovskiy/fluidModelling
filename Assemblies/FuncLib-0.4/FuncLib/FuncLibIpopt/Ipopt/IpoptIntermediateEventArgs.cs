// Copyright (c) 2011 Morten Bakkedal
// This code is published under the MIT License.

using System;
using System.ComponentModel;

namespace FuncLib.Optimization.Ipopt
{
	public class IpoptIntermediateEventArgs : CancelEventArgs
	{
		private IpoptAlgorithmMode algorithmMode;
		private int iteration;
		private double objectiveValue;

		public IpoptIntermediateEventArgs(IpoptAlgorithmMode algorithmMode, int iteration, double objectiveValue)
		{
			this.algorithmMode = algorithmMode;
			this.iteration = iteration;
			this.objectiveValue = objectiveValue;
		}

		/// <summary>
		/// Current Ipopt algorithm mode.
		/// </summary>
		public IpoptAlgorithmMode AlgorithmMode
		{
			get
			{
				return algorithmMode;
			}
		}

		/// <summary>
		/// Current iteration number.
		/// </summary>
		public int Iteration
		{
			get
			{
				return iteration;
			}
		}

		/// <summary>
		/// The unscaled objective value at the current point.
		/// </summary>
		public double ObjectiveValue
		{
			get
			{
				return objectiveValue;
			}
		}

		// PrimalInfeasibility
		// DualInfeasibility
		// ...
	}
}
