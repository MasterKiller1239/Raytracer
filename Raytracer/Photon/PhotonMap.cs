using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracer.Photon
{
    class PhotonMap
    {
		public List<Photon> photons;
		public List<Photon> data;
		public List<Photon> knn;
		public int maxPhotons;
		public int currentKNNSize = 1;
		public int size() { return photons.Count(); }
		public int median(ref List<Photon> photons, int w, int dimension, int a, int b)
		{
			int i = a;
			int n = b;
			int j = n - 1;
			while (i != j)
			{
				int k = partition(ref photons, dimension, i, j);
				//std::cout << "i " << i << ", j " << j << ", k " << k <<" w "<<w<< std::endl;		
				k = k - i + 1;
				if (k >= w) j = i + k - 1;
				else if (k < w)
				{
					w -= k;
					i += k;
				}
			}
			return i;
		}
		public int partition(ref List<Photon> photons, int dimension, int a, int b)
		{
			float e, tmp;
			e = photons[a].position[dimension];
			while (a < b)
			{
				while ((a < b) && (photons[b].position[dimension] >= e)) b--;
				while ((a < b) && (photons[a].position[dimension] < e)) a++;
				if (a < b)
				{
					Photon temp = photons[a];
					photons[a] = photons[b];
					photons[b] = temp;
				}
			}
			return a;
		}

		public void AddPhoton(ref Photon p) { photons.Add(p); }
		public void crateDataVector(int size)
        {
			int temp = 0;
			while (size > 0)
			{
				size /= 2;
				temp++;
			}
			int dataSize = 1 << temp;
			data = new List<Photon>(dataSize + 1);
			data[0] = null;
		}
		public void createKdTree(List<Photon> photons, int depth, int a, int b, int positionInArray = 1)
        {
			if (photons.Count() == 0) return;
			int size = b - a;
			int dimension = depth % 3;
			int pos = size / 2;
			if (size % 2 == 1) pos++;

			int medianPosition = median(ref photons, pos, dimension, a, b);
			data[positionInArray] = photons[medianPosition];
			data[positionInArray].dimension = (short)dimension;
			List<Photon> left = new List<Photon>();
			for (int i = 0; i < medianPosition; i++)
			{
				left.Add(photons[i]);
			}
			List<Photon> right = new List<Photon>();
			for (int i = medianPosition + 1; i < photons.Count(); i++)
			{
				right.Add(photons[i]);
			}
			createKdTree(left, depth + 1, 0, left.Count(), 2 * positionInArray);
			createKdTree(right, depth + 1, 0, right.Count(), 2 * positionInArray + 1);

		}
		public void findPhotons(ref Vector3 point, int photonsNumer,ref double searchRadius, int positionInArray,ref List<Photon> knnPhotons)
        {
			if (2 * positionInArray + 1 < knnPhotons.Count())
			{
				short dimension = data[positionInArray].dimension;
				double distance;
				if (dimension==0)
                {
					distance = point.x - data[positionInArray].position[dimension];

				}
				else if (dimension == 1)
				{
					distance = point.y - data[positionInArray].position[dimension];

				}
				else 
				{
					distance = point.z - data[positionInArray].position[dimension];

				}
				//cout << point << endl << *data[positionInArray] << endl;
				//cout << "odleglosc " << dimension << " = " << distance;
				//system("pause");
				double distanceSquared = distance * distance;
				if (distance < 0)
				{
					findPhotons(ref point, photonsNumer,ref searchRadius, 2 * positionInArray,ref knnPhotons);
					if (distanceSquared < searchRadius)
					{
						findPhotons(ref point, photonsNumer,ref searchRadius, 2 * positionInArray + 1,ref knnPhotons);
					}
				}
				else
				{
					findPhotons(ref point, photonsNumer, ref searchRadius, 2 * positionInArray + 1,ref knnPhotons);
					if (distanceSquared <  searchRadius)
					{
						findPhotons(ref point, photonsNumer, ref searchRadius, 2 * positionInArray,ref knnPhotons);
					}
				}
				double SquaredDistance2 =
					(point.x - data[positionInArray].position[0]) * (point.x - data[positionInArray].position[0]) +
					(point.y - data[positionInArray].position[1]) * (point.y - data[positionInArray].position[1]) +
					(point.z - data[positionInArray].position[2]) * (point.z - data[positionInArray].position[2]);
				data[positionInArray].distance = SquaredDistance2;
				if (SquaredDistance2 < searchRadius)
				{
					Photon tem = data[positionInArray];
					insertPhotonToKNN(ref tem,ref knnPhotons);
					if (currentKNNSize == knn.Count())
					{
						removePhotonFromKNN(ref knn);
					}

					//searchRadiusSquared = SquaredDistance2;
				}
			}
		}
		public void insertPhotonToKNN(ref Photon p, ref List<Photon> knnPhotons)
        {
			int position = currentKNNSize;
			int parentPosition = currentKNNSize;
			Photon temp;
			if (knn[1] == null)
			{//pusty kopiec
				knn[1] = p;
				currentKNNSize = currentKNNSize + 1;
				return;
			}
			else
			{
				knn[position] = p;
				currentKNNSize = currentKNNSize + 1;
				while (position > 1)
				{
					parentPosition = parentPosition / 2;
					if (knn[parentPosition] == null)
					{
						temp = knn[parentPosition];
						knn[parentPosition] = knn[position];
						knn[position] = temp;
						position = parentPosition;
					}
					else if (knn[parentPosition].distance < knn[position].distance)
					{
						temp = knn[parentPosition];
						knn[parentPosition] = knn[position];
						knn[position] = temp;
						position = parentPosition;
					}
					else
					{
						break;
					}
				}
			}
		}
		public void removePhotonFromKNN(ref List<Photon> knnPhotons)
        {
			knn[1] = null;
			currentKNNSize = currentKNNSize - 1;
			knn[1] = knn[currentKNNSize];
			int position = 1;
			Photon temp;
			while ((2 * position + 1) < knn.Count())
			{
				int childLeft = 2 * position;
				int childRight = 2 * position + 1;
				if (knn[childLeft].distance > knn[childRight].distance)
				{
					if (knn[position].distance < knn[childLeft].distance)
					{
						temp = knn[position];
						knn[position] = knn[childLeft];
						knn[childLeft] = temp;
					}
					position = childLeft;
				}
				else
				{
					if (knn[position].distance < knn[childRight].distance)
					{
						temp = knn[position];
						knn[position] = knn[childRight];
						knn[childRight] = temp;
					}
					position = childRight;
				}
			}
		}

        PhotonMap()
        {

        }
        PhotonMap(int size)
        {
			maxPhotons = size + 1;
			int temp = 0;
			while (size > 0)
			{
				size /= 2;
				temp++;
			}
			int dataSize = 1 << temp;
            data =  new List<Photon>(dataSize + 1);
			data[0] = null;
		}
		public Vector3 GetColor(double searchSquareRarius, ref Vector3 inter, int knnSize, ref Vector3 normal)
        {
			Vector3 colorTemp= new Vector3();
			currentKNNSize = 1;
			knn = new List<Photon>(knnSize + 1);
			for (int i = 0; i <= knnSize; i++)
			{
				knn[i] = null;
			}
			if (data.Count() == 0)
			{
				
				crateDataVector(photons.Count());
				createKdTree(photons, 1, 0, photons.Count(), 1);
			}
			double search = searchSquareRarius;
			findPhotons(ref inter, knnSize,ref search, 1,ref knn);
			if (knn[1] == null)
			{
				return colorTemp;
			}
			double r = knn[1].distance;
			for (int i = 1; i < knn.Count(); i++)
			{
				Photon photon = knn[i];
				if (photon == null) continue;
				double cosinus = normal.x * photon.direction[0] + normal.y * photon.direction[1] + normal.z * photon.direction[2];
				if (cosinus < 0) continue;
				colorTemp.x += photon.energy[0];
				colorTemp.y += photon.energy[1];
				colorTemp.z += photon.energy[2];

			}
			colorTemp *= (1 / (Math.PI * r));
			return colorTemp;
		}
       public void ScaleEnergy()
        {
			float counter = 1 / 8;
			foreach (Photon x in data)
			{
				x.energy[0] *= counter;
				x.energy[1] *= counter;
				x.energy[2] *= counter;
			}
		}
    }
}
