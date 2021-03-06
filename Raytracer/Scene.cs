using Raytracer.Lights;
using System.Collections.Generic;

namespace Raytracer
{
    public class Scene
    {
        public List<Primitive> primitives = new List<Primitive>();

        public List<Light> lights = new List<Light>();

        public List<Mesh> meshes = new List<Mesh>();

        public Color ambientColor = new Color(0.1, 0.1, 0.1);

        public Scene(List<Primitive> primitives, List<Light> lights, List<Mesh> meshes, Color ambientColor)
        {
            this.primitives = primitives;
            this.lights = lights;
            this.meshes = meshes;
            this.ambientColor = ambientColor;
        }

        public Scene()
        {
            ambientColor = new Color(0.1, 0.1, 0.1);
        }

        public void SetObjectsForRendering(ref List<Primitive> objects)
        {
            this.primitives = objects;
        }

        public bool AddMeshForRendering(ref Mesh m)
        {
        
            meshes.Add(m);
            foreach (Face f in m.faces)
            {
                primitives.Add(f.GetTriangle());

            }
            return true;
        }
        public void AddPrimitiveForRendering(ref Primitive p)

        {
            primitives.Add(p);
        }

    }
}
