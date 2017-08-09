using System;
using System.Runtime.Serialization;

namespace NN.Geometry
{
    /// <summary>
    /// Represents an extrusion, or objects such as beams or linearly extruded elements,
    /// that can be represented by profile curves and two miter planes at the extremes.
    /// </summary>
    [Serializable]
    public class Extrusion : Surface
    {
#if RHINO3DMIO
        public Extrusion(Rhino.Geometry.Extrusion f)
        {
            CopyFrom(f);
        }

        public bool CopyFrom(Rhino.Geometry.Extrusion from)
        {

            if (from == null)
                return false;

            this.PathStart = new NN.Geometry.Point3d(from.PathStart);
            this.PathEnd = new NN.Geometry.Point3d(from.PathEnd);
            this.PathTangent = new NN.Geometry.Vector3d(from.PathTangent);
            this.MiterPlaneNormalAtStart = new NN.Geometry.Vector3d(from.MiterPlaneNormalAtStart);
            this.MiterPlaneNormalAtEnd = new NN.Geometry.Vector3d(from.MiterPlaneNormalAtEnd);
            this.IsMiteredAtStart = from.IsMiteredAtStart;
            this.IsMiteredAtEnd = from.IsMiteredAtEnd;
            this.IsSolid = from.IsSolid;
            this.IsCappedAtBottom = from.IsCappedAtBottom;
            this.IsCappedAtTop = from.IsCappedAtTop;
            this.CapCount = from.CapCount;
            this.ProfileCount = from.ProfileCount;

            this.ObjectType = (NN.DocObjects.ObjectType)from.ObjectType;
            
            return true;
        }


        public bool CopyTo(Rhino.Geometry.Extrusion to)
        {
            to.MiterPlaneNormalAtStart = this.MiterPlaneNormalAtStart.RhinoObject();
            to.MiterPlaneNormalAtEnd = this.MiterPlaneNormalAtEnd.RhinoObject();
            
            return true;
        }

        public  Rhino.Geometry.Extrusion RhinoObject()
        {
            var rhinoObject = new Rhino.Geometry.Extrusion();

            CopyTo(rhinoObject);

            return rhinoObject;
        }
#endif

        public Extrusion() { }
        

        /// <summary>
        /// Gets the start point of the path.
        /// </summary>
        public Point3d PathStart { get; set; }
        

        /// <summary>
        /// Gets the end point of the path.
        /// </summary>
        public Point3d PathEnd { get; set; }
        

        /// <summary>
        /// Gets the up vector of the path.
        /// </summary>
        public Vector3d PathTangent { get; set; }
        
        /// <summary>
        /// Gets or sets the normal of the miter plane at the start in profile coordinates.
        /// In profile coordinates, 0,0,1 always maps to the extrusion axis
        /// </summary>
        public Vector3d MiterPlaneNormalAtStart { get; set; }
        

        /// <summary>
        /// Gets or sets the normal of the miter plane at the end in profile coordinates.
        /// In profile coordinates, 0,0,1 always maps to the extrusion axis
        /// </summary>
        public Vector3d MiterPlaneNormalAtEnd { get; set; }
        

        /// <summary>
        /// Returns a value indicating whether a miter plane at start is defined.
        /// </summary>
        public bool IsMiteredAtStart { get; set; }
        

        /// <summary>
        /// Gets a value indicating whether a miter plane at the end is defined.
        /// </summary>
        public bool IsMiteredAtEnd { get; set; }
        
        /// <summary>
        /// Gets a value indicating whether there is no gap among all surfaces constructing this object.
        /// </summary>
        public override bool IsSolid { get; set; }
        

        /// <summary>
        /// Gets a value indicating whether the surface that fills the bottom profile is existing.
        /// </summary>
        public bool IsCappedAtBottom { get; set; }
        

        /// <summary>
        /// Gets a value indicating whether the surface that fills the top profile is existing.
        /// </summary>
        public bool IsCappedAtTop { get; set; }
        
        /// <summary>
        /// Gets the amount of capping surfaces.
        /// </summary>
        public int CapCount { get; set; }
        

        /// <summary>
        /// Gets the amount of profile curves.
        /// </summary>
        public int ProfileCount { get; set; }


        public System.Collections.Generic.List<Curve> Profile3dTop = new System.Collections.Generic.List<Curve>();
        public System.Collections.Generic.List<Curve> Profile3dBottom = new System.Collections.Generic.List<Curve>();
        

        /// <summary>
        /// Gets the line-like curve that is the conceptual axis of the extrusion.
        /// </summary>
        /// <returns>The path as a line curve.</returns>
        public LineCurve PathLineCurve { get; set; }
    
        public Mesh RenderMesh { get; set; }
        public Mesh PreviewMesh { get; set; }
    }
}
