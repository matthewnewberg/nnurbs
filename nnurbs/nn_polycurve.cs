using System;

namespace NN.Geometry
{
    /// <summary>
    /// Represents a curve that is the result of joining several (possibly different)
    /// types of curves.
    /// </summary>
    [Serializable]
    public class PolyCurve : Curve
    {
        public PolyCurve()
        {
            SegmentCurves = new System.Collections.Generic.List<Curve>();
            SegmentDomain = new System.Collections.Generic.List<Interval>();
        }

#if RHINO3DMIO
        public PolyCurve(Rhino.Geometry.PolyCurve f)
        {
            CopyFrom(f);
        }

        public bool CopyFrom(Rhino.Geometry.PolyCurve from)
        {

            if (from == null)
                return false;

            this.SegmentCount = from.SegmentCount;
            this.IsNested = from.IsNested;
            this.HasGap = from.HasGap;
            this.Domain = new NN.Geometry.Interval(from.Domain);
            this.Dimension = from.Dimension;
            this.SpanCount = from.SpanCount;
            this.Degree = from.Degree;
            this.IsClosed = from.IsClosed;
            this.IsPeriodic = from.IsPeriodic;

            this.ObjectType = (NN.DocObjects.ObjectType)from.ObjectType;

            SegmentCurves = new System.Collections.Generic.List<Curve>();
            SegmentDomain = new System.Collections.Generic.List<Interval>();

            for (int i = 0; i < from.SegmentCount; ++i)
            {
                SegmentCurves.Add(Curve.CreateCurve(from.SegmentCurve(i)));
                SegmentDomain.Add(new Interval(from.SegmentDomain(i)));
            }

            ComponentIndex = new ComponentIndex(from.ComponentIndex());

            return true;
        }

        public bool CopyTo(Rhino.Geometry.PolyCurve to)
        {

            for (int i=0; i < SegmentCurves.Count; ++i)
            {
                var c = SegmentCurves[i];
                to.Append(c.RhinoCurveObject());
            }

            // ?? Is this needed
            // to.Domain = this.Domain.RhinoObject();

            return true;
        }

        public Rhino.Geometry.PolyCurve RhinoObject()
        {
            Rhino.Geometry.PolyCurve rhinoCurve = new Rhino.Geometry.PolyCurve();

            this.CopyTo(rhinoCurve);

            return rhinoCurve;
        }

        public override Rhino.Geometry.Curve RhinoCurveObject()
        {
            return RhinoObject();
        }
#endif

        public int SegmentCount { get; set; }


        public System.Collections.Generic.List<Curve> SegmentCurves { get; set; }

        public System.Collections.Generic.List<Interval> SegmentDomain { get; set; }

        /// <seealso cref="RemoveNesting"/>
        public bool IsNested { get; set; }

        /// <summary>
        /// This is a quick way to see if the curve has gaps between the sub curve segments. 
        /// </summary>
        public bool HasGap { get; set; }

    }
}
