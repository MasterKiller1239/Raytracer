using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracer.Photon
{
	class Photon
	{
		public float distanceSquared;
		public short dimension;
		public float[] position = new float[3];
		public float[] direction = new float[3];//kierunek nadej?cia
		public float[] energy = new float[3];
		public double distance;//odleglosc od szukanego punktu
		public Photon()
        {

        }
		public Photon(Photon p)
        {
			for (int i = 0; i < 3; i++)
			{
				energy[i] = p.energy[i];
				position[i] = p.position[i];
				direction[i] = p.direction[i];
			}
		}
	}

}
