using System;
using System.Runtime.Serialization;

namespace NN.Geometry
{
    /// <summary>
    /// Defines enumerated values for isoparametric curve direction on a surface, such as X or Y,
    /// and curve sides, such as North or West boundary.
    /// <para>Note: odd values are all x-constant; even values > 0 are all y-constant.</para>
    /// </summary>
    public enum IsoStatus : int
    {
        /// <summary>
        /// curve is not an isoparameteric curve.
        /// </summary>
        None = 0,
        /// <summary>
        /// curve is a "x" = constant (vertical) isoparametric curve in the interior of the surface's domain.
        /// </summary>
        X = 1,
        /// <summary>
        /// curve is a "y" = constant (horizontal) isoparametric curve in the interior of the surface's domain.
        /// </summary>
        Y = 2,
        /// <summary>
        /// curve is a "x" = constant isoparametric curve along the west side of the surface's domain.
        /// </summary>
        West = 3,
        /// <summary>
        /// curve is a "y" = constant isoparametric curve along the south side of the surface's domain.
        /// </summary>
        South = 4,
        /// <summary>
        /// curve is a "x" = constant isoparametric curve along the east side of the surface's domain.
        /// </summary>
        East = 5,
        /// <summary>
        /// curve is a "y" = constant isoparametric curve along the north side of the surface's domain.
        /// </summary>
        North = 6
    }

   

    /// <summary>
    /// Represents a base class that is common to most RhinoCommon surface types.
    /// <para>A surface represents an entity that can be all visited by providing
    /// two independent parameters, usually called (u, v), or sometimes (s, t).</para>
    /// </summary>
    [Serializable]
    public class Surface : GeometryBase
    {

        /// <summary>
        /// Protected constructor for internal use.
        /// </summary>
        public  Surface()
        {
            // the base class always handles set up of pointers
        }



#if RHINO3DMIO || RHINOCOMMON

        public static Surface CreateSurface(Rhino.Geometry.Surface f)
        {

            var nurbs = f as Rhino.Geometry.NurbsSurface;

            if (nurbs != null)
                return new NN.Geometry.NurbsSurface(nurbs);


            var planeSurface = f as Rhino.Geometry.PlaneSurface;

            if (planeSurface != null)
                return new NN.Geometry.PlaneSurface(planeSurface);

            var revsurface = f as Rhino.Geometry.RevSurface;

            if (revsurface != null)
                return new NN.Geometry.RevSurface(revsurface);

            var sumsurface = f as Rhino.Geometry.SumSurface;

            if (sumsurface != null)
                return new NN.Geometry.SumSurface(sumsurface);

            var extrusion = f as Rhino.Geometry.Extrusion;

            if (extrusion != null)
                return new NN.Geometry.Extrusion(extrusion);

            return null;
        }

        public virtual Rhino.Geometry.Surface RhinoSurfaceObject()
        {
            return null;
        }

#endif


        /// <summary>Gets the domain in a direction.</summary>
        /// <param name="direction">0 gets first parameter, 1 gets second parameter.</param>
        /// <returns>An interval value.</returns>
        public Interval DomainU { get; set; }
        public Interval DomainV { get; set; }

        public int DegreeU { get; set; }
        public int DegreeV { get; set; }

        public int SpanUCount { get; set; }
        public int SpanVCount { get; set; }

        public System.Collections.Generic.List<double> SpanU = new System.Collections.Generic.List<double>();
        public System.Collections.Generic.List<double> SpanV = new System.Collections.Generic.List<double>();

        
        public bool ClosedU { get; set; }
        public bool ClosedV { get; set; }
        
        public bool IsPeriodicU { get; set; }
        public bool IsPeriodicV { get; set; }

        public bool IsSingularSouth { get; set; }
        public bool IsSingularNorth { get; set; }
        public bool IsSingularEast { get; set; }
        public bool IsSingularWest { get; set; }


        public bool IsPlanar { get; set; }
        public bool IsSphere { get; set; }
        public bool IsCylinder { get; set; }    
        public bool IsCone { get; set; }
        public bool IsTorus { get; set; }
        
        public virtual bool IsSolid { get; set; }
    }
}