using System;
using System.Runtime.Serialization;

namespace NN.Geometry
{
    /// <summary>
    /// Represents a surface of revolution.
    /// <para>Revolutions can be incomplete (they can form arcs).</para>
    /// </summary>
    [Serializable]
    public class RevSurface : Surface
    {
        public RevSurface()
        { }

#if RHINO3DMIO
        public RevSurface(Rhino.Geometry.RevSurface f)
        {
            CopyFrom(f);
        }

        public bool CopyFrom(Rhino.Geometry.RevSurface from)
        {

            return true;
        }


        public bool CopyTo(Rhino.Geometry.RevSurface to)
        {
            return true;
        }
#endif

    }
}
