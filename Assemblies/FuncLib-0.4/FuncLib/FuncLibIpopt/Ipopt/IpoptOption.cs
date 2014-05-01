// Copyright (c) 2011 Morten Bakkedal
// This code is published under the MIT License.

using System;
using System.Diagnostics;

using FuncLib.Optimization.Ipopt.Cureos;

namespace FuncLib.Optimization.Ipopt
{
	[Serializable]
	[DebuggerDisplay("{ToString(),nq}")]
	public abstract class IpoptOption
	{
		private string name;

		public IpoptOption(string name)
		{
			this.name = name;
		}

		internal abstract void Prepare(IpoptProblem ipopt);

		internal abstract IpoptOption Clone();

		public override string ToString()
		{
			return name + " = " + Value;
		}

		public string Name
		{
			get
			{
				return name;
			}
		}

		public abstract object Value
		{
			get;
			set;
		}
	}
}
