using System;
using System.Runtime.Serialization;

namespace NN.Geometry
{
    /// <summary>
    /// Represents a plane surface, with plane and two intervals.
    /// </summary>
    [Serializable]
    public class PlaneSurface : Surface
    {
        public PlaneSurface()
        { }

#if RHINO3DMIO
        public PlaneSurface(Rhino.Geometry.PlaneSurface f)
        {
            CopyFrom(f);
        }

        public bool CopyFrom(Rhino.Geometry.PlaneSurface from)
        {

            return true;
        }


        public bool CopyTo(Rhino.Geometry.PlaneSurface to)
        {
            return true;
        }
#endif

    }

    /// <summary>
    /// Represents a planar surface that is used as clipping plane in viewports.
    /// </summary>
    [Serializable]
    public class ClippingPlaneSurface : PlaneSurface
    {
        public ClippingPlaneSurface() { }

        /// <summary>
        /// Gets or sets the clipping plane.
        /// </summary>
        public Plane Plane { get; set; }


        /// <summary>
        /// Returns Ids of viewports that this clipping plane is supposed to clip.
        /// </summary>
        /// <returns>An array of globally unique ideantifiers (Guids) to the viewports.</returns>
        /// 
        System.Collections.Generic.List<Guid> ViewportIds = new System.Collections.Generic.List<Guid>();

    }
}
