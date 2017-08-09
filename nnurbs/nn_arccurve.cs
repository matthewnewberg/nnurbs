using System;
using NN.Display;
using System.Runtime.Serialization;

namespace NN.Geometry
{
  /// <summary>
  /// Represent arcs and circles.
  /// <para>ArcCurve.IsCircle returns true if the curve is a complete circle.</para>
  /// </summary>
  /// <remarks>
  /// <para>Details:</para>
  /// <para>an ArcCurve is a subcurve of a circle, with a constant speed
  /// parameterization. The parameterization is	an affine linear
  /// reparameterzation of the underlying arc	m_arc onto the domain m_t.</para>
  /// <para>A valid ArcCurve has Radius()>0 and  0&lt;AngleRadians()&lt;=2*PI
  /// and a strictly increasing Domain.</para>
  /// </remarks>
  [Serializable]
  public class ArcCurve : Curve
  {
    /// <summary>
    /// Initializes a new <see cref="ArcCurve"/> instance.
    /// <para>Radius is set to 1, position to Origin and Domain to full span (circle).</para>
    /// </summary>
    public ArcCurve()
    {
      
    }


#if RHINO3DMIO || RHINOCOMMON
        public ArcCurve(Rhino.Geometry.ArcCurve f)
        {
            CopyFrom(f);
        }

        public bool CopyFrom(Rhino.Geometry.ArcCurve from)
        {
            

            if (from == null)
                return false;

            //		this.Arc =  new NN.Geometry.Arc(from.Arc);
            //		this.IsCompleteCircle = from.IsCompleteCircle;
            this.Radius = from.Radius;
            this.AngleRadians = from.AngleRadians;
            this.AngleDegrees = from.AngleDegrees;
            this.Domain = new NN.Geometry.Interval(from.Domain);
            this.Dimension = from.Dimension;
            this.SpanCount = from.SpanCount;
            this.Degree = from.Degree;
            this.IsClosed = from.IsClosed;
            this.IsPeriodic = from.IsPeriodic;

            this.ObjectType = (NN.DocObjects.ObjectType)from.ObjectType;

            this.Arc = new Arc(from.Arc);

            ComponentIndex = new ComponentIndex(from.ComponentIndex());

            return true;
        }


        public bool CopyTo(Rhino.Geometry.ArcCurve to)
        {
            to.Domain = this.Domain.RhinoObject();

            return true;
        }

        public Rhino.Geometry.ArcCurve RhinoObject()
        {
            return new Rhino.Geometry.ArcCurve(Arc.RhinoObject());
        }

        public override Rhino.Geometry.Curve RhinoCurveObject()
        {
            return RhinoObject();
        }
#endif

        /// <summary>
        /// Gets the arc that is contained within this ArcCurve.
        /// </summary>
        public Arc Arc { get; set; }
    
    /// <summary>
    /// Gets the radius of this ArcCurve.
    /// </summary>
    public double Radius { get; set; }
    

    /// <summary>
    /// Gets the angles of this arc in radians.
    /// </summary>
    public double AngleRadians { get; set; }
    
    /// <summary>
    /// Gets the angles of this arc in degrees.
    /// </summary>
    public double AngleDegrees { get; set; }
  }
}