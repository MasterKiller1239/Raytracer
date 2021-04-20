using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace Raytracer
{
    class ObjReader
    {
        /// <summary>
		/// Proccess face
		/// </summary>
		/// <param name=" f">string of faces</param>
        /// <param name=" currentMesh">currentMesh</param>
		public void ProcessFace(string[] f,ref Mesh currentMesh)
        {
            int[,] faceData = new int[3,3];
			for (int i = 0; i < 3; i++)
				for (int y = 0;y < 3; y++)
                    faceData[i,y] = -1;

            for (int i = 1; i < 4; i++)
            {
                var splitted = f[i].Split(new[]  { '/' }, StringSplitOptions.RemoveEmptyEntries);
                int iterator = 0;
                foreach ( string  s in splitted)
                {
                 
                    if (s == "") faceData[i - 1,iterator] = -1;
                    else faceData[i - 1,iterator] = Int32.Parse(s);
                    ++iterator;
                }
            }

            currentMesh.AddFace(faceData);
        }
        /// <summary>
        /// Parse Strings to Vector3
        /// </summary>
        /// <param name=" x,y,z">3 vector args</param>
        public Vector3 parseFromStringsToVector3(string x, string y,string z)
        {
            string fixx = x.Replace(".", ",");
            string fixy = y.Replace(".", ",");
            string fixz = z.Replace(".", ",");
            //Console.WriteLine(fixx + " " + fixy + " " + fixz);
            //Console.WriteLine(Convert.ToDouble(x, CultureInfo.InvariantCulture) + " " + Convert.ToDouble(y, CultureInfo.InvariantCulture) + " " + Convert.ToDouble(z, CultureInfo.InvariantCulture));
            return new Vector3((float)Convert.ToDouble(x, CultureInfo.InvariantCulture), (float)Convert.ToDouble(y, CultureInfo.InvariantCulture), (float)Convert.ToDouble(z, CultureInfo.InvariantCulture));
            //  return new Vector3((float)Double.Parse(fixx, NumberStyles.AllowLeadingSign), (float)Double.Parse(fixy, NumberStyles.AllowLeadingSign), (float)Double.Parse(fixz, NumberStyles.AllowLeadingSign));

        }
        /// <summary>
		/// Parse obj file
		/// </summary>
		/// <param name=" filename">name of the obj file</param>
        public ObjFile ReadFromFile(string filename)
        {

            ObjFile result = new ObjFile();
            string text = System.IO.File.ReadAllText("CubeTex2.obj");
           
            string[] lines = System.IO.File.ReadAllLines(filename);
           
            Mesh currentMesh = new Mesh();
            foreach(string line in lines)
            {
             
                //Console.WriteLine(line);
                var splitted = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (splitted.Length == 0)
                    continue;
            
                if (splitted[0] == "g")
                {
                    currentMesh = result.CreateAndAddMesh();
                    if (splitted.Length > 1)
                        currentMesh.name = splitted[1];
                  //  Console.WriteLine("g");
                }
                else if (splitted[0] == "v")
                {
                  //  Console.WriteLine("spit1:"+splitted[2]+ ' '+ "spit2:" + splitted[4] + ' ' + "spit3:" + splitted[6]);
                    result.vertices.Add(parseFromStringsToVector3(splitted[1], splitted[2], splitted[3]));
                   
                }
                else if (splitted[0] == "vn")
                {
                    //int I = 0;
                    //foreach (string lie in splitted)
                    //{

                    //    Console.WriteLine(I + ":" + lie);
                    //    I++;
                    //}
                    result.normals.Add(parseFromStringsToVector3(splitted[1], splitted[2], splitted[3]));
                 //   Console.WriteLine("vn");
                }
                else if (splitted[0] == "vt")
                {
                    splitted[1] = splitted[1].Replace(".", ",");
                    splitted[2] = splitted[2].Replace(".", ",");
                    result.uvs.Add(new Vector3((float)Convert.ToDouble(splitted[1]), (float)Convert.ToDouble(splitted[2]), 0));
                 //  Console.WriteLine(num[0]+"," +num[1]);
                }
                else if (splitted[0] == "f")
                {
                   ProcessFace(splitted,ref currentMesh);
                   // Console.WriteLine("f");
                }


            }
            return result;
        }
    }
}
