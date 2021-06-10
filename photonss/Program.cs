using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RaytracerPhotonmapping
{
    static class Program
    {
		/// <summary>
		/// Główny punkt wejścia dla aplikacji.
		/// </summary>
		[STAThread]
		public static void Main(System.String[] args)
		{
			TextWriterTraceListener tr1 = new TextWriterTraceListener(System.IO.File.CreateText("Output.txt"));
			Trace.Listeners.Add(tr1);
			new RaytracerPhoton();
		
		}
	}
}
