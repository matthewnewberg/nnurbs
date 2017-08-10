using System;
using NN.Display;
using System.Runtime.Serialization;

namespace NN.Geometry
{
  /// <summary>
  /// Represents a linear curve.
  /// </summary>
  [Serializable]
  public class LineCurve : Curve
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="LineCurve"/> class.
    /// </summary>
    public LineCurve()
    {
    }

#if RHINO3DMIO || RHINOCOMMON
        public LineCurve(Rhino.Geometry.LineCurve f)
        {
            CopyFrom(f);
        }
    
        public bool CopyFrom(Rhino.Geometry.LineCurve from)
        {
            if (from == null)
                return false;

            ComponentIndex = new ComponentIndex(from.ComponentIndex());

            this.Line =  new NN.Geometry.Line(from.Line);
            this.Domain = new NN.Geometry.Interval(from.Domain);
            this.Dimension = from.Dimension;
            this.SpanCount = from.SpanCount;
            this.Degree = from.Degree;
            this.IsClosed = from.IsClosed;
            this.IsPeriodic = from.IsPeriodic;
            this.ObjectType = (NN.DocObjects.ObjectType)from.ObjectType;

            return true;
        }
        
        public bool CopyTo(Rhino.Geometry.LineCurve to)
        {
            to.Line = this.Line.RhinoObject();
            to.Domain = this.Domain.RhinoObject();
           
            return true;
        }

        public Rhino.Geometry.LineCurve RhinoObject()
        {
            return new Rhino.Geometry.LineCurve(Line.RhinoObject());
        }

        public override Rhino.Geometry.Curve RhinoCurveObject()
        {
            return RhinoObject();
        }
#endif

        /// <summary>
        /// Gets or sets the Line value inside this curve.
        /// </summary>
        public Line Line { get; set; }
    
  }
}
