#pragma warning disable IDE0002

using UnityEngine;

namespace Chickenen.Pancreas
{
    public static partial class Gobject
    {
        public static GameObject Generate(this GameObject g)
        {
            return GameObject.Instantiate(g);
        }

        public static GameObject Generate(this GameObject g, Vector3 p)
        {
            return GameObject.Instantiate(g, p, Quaternion.identity);
        }

        public static GameObject Generate(this GameObject g, Vector3 p, Quaternion r)
        {
            return GameObject.Instantiate(g, p, r);
        }

        public static GameObject Generate(this GameObject[] gs)
        {
            return GameObject.Instantiate(gs.Choice());
        }

        public static GameObject Generate(this GameObject[] gs, Vector3 p)
        {
            return GameObject.Instantiate(gs.Choice(), p, Quaternion.identity);
        }

        public static GameObject Generate(this GameObject[] gs, Vector3 p, Quaternion r)
        {
            return GameObject.Instantiate(gs.Choice(), p, r);
        }

        public static GameObject TryGenerate(this GameObject g)
        {
            return g != null ? g.Generate() : null;
        }

        public static GameObject TryGenerate(this GameObject g, Vector3 p)
        {
            return g != null ? g.Generate(p, Quaternion.identity) : null;
        }

        public static GameObject TryGenerate(this GameObject g, Vector3 p, Quaternion r)
        {
            return g != null ? g.Generate(p, r) : null;
        }

        public static GameObject TryGenerate(this GameObject[] gs)
        {
            return gs.Length > 0 ? gs.Generate() : null;
        }

        public static GameObject TryGenerate(this GameObject[] gs, Vector3 p)
        {
            return gs.Length > 0 ? gs.Generate(p, Quaternion.identity) : null;
        }

        public static GameObject TryGenerate(this GameObject[] gs, Vector3 p, Quaternion r)
        {
            return gs.Length > 0 ? gs.Generate(p, r) : null;
        }
    }
}