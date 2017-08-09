using System;
using System.Runtime.Serialization;

namespace NN.Geometry
{
    /// <summary>
    /// Represents a sum surface, or an extrusion of a curve along a curved path.
    /// </summary>
    [Serializable]
    public class SumSurface : Surface
    {
        public SumSurface()
          : base()
        { }

#if RHINO3DMIO || RHINOCOMMON
        public SumSurface(Rhino.Geometry.SumSurface f)
        {
            CopyFrom(f);
        }

        public bool CopyFrom(Rhino.Geometry.SumSurface from)
        {
            // TODO ??? Unable to find test file
            return true;
        }


        public bool CopyTo(Rhino.Geometry.SumSurface to)
        {
            return true;
        }
#endif

    }
}
