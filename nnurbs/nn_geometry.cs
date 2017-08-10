using System;
using System.Runtime.Serialization;
using NN.DocObjects;

namespace NN.Geometry
{
    /// <summary>
    /// Provides a common base for most geometric classes. This class is abstract.
    /// </summary>
    [Serializable]
    public class GeometryBase 
    {
        public GeometryBase() { }

#if RHINO3DMIO || RHINOCOMMON

        public static GeometryBase CreateGeometery(Rhino.Geometry.GeometryBase f) { 

            var brep = f as Rhino.Geometry.Brep;

            if (brep != null)
                return new NN.Geometry.Brep(brep);

            var surface = f as Rhino.Geometry.NurbsSurface;

            if (surface != null)
                return new NN.Geometry.NurbsSurface(surface);

            var sumSurface = f as Rhino.Geometry.SumSurface;

            if (sumSurface != null)
                return new NN.Geometry.SumSurface(sumSurface);
            
            var curve = f as Rhino.Geometry.Curve;

            if (curve != null)
                return Curve.CreateCurve(curve);

            var extrusion = f as Rhino.Geometry.Extrusion;

            if (extrusion != null)
                return new NN.Geometry.Extrusion(extrusion);

            var mesh = f as Rhino.Geometry.Mesh;

            if (mesh != null)
                return new NN.Geometry.Mesh(mesh);

            var point = f as Rhino.Geometry.Point;

            if (point != null)
                return new NN.Geometry.Point(point);

            Console.WriteLine("Missing!!!!");
            Console.WriteLine(f.GetType().ToString());
            Console.WriteLine(f.ToString());

            return null;
        }

        public bool CopyFrom(Rhino.Geometry.GeometryBase from)
        {
          
            return true;
        }


        public bool CopyTo(Rhino.Geometry.GeometryBase to)
        {
            

            return true;
        }
#endif

        


        /// <summary>
        /// Useful for switch statements that need to differentiate between
        /// basic object types like points, curves, surfaces, and so on.
        /// </summary>

        public ObjectType ObjectType { get; set; }


        /// <summary>
        /// Transforms the geometry. If the input Transform has a SimilarityType of
        /// OrientationReversing, you may want to consider flipping the transformed
        /// geometry after calling this function when it makes sense. For example,
        /// you may want to call Flip() on a Brep after transforming it.
        /// </summary>
        /// <param name="xform">
        /// Transformation to apply to geometry.
        /// </param>
        /// <returns>true if geometry successfully transformed.</returns>



        /// <summary>
        /// Boundingbox solver. Gets the world axis aligned boundingbox for the geometry.
        /// </summary>
        /// <param name="accurate">If true, a physically accurate boundingbox will be computed. 
        /// If not, a boundingbox estimate will be computed. For some geometry types there is no 
        /// difference between the estimate and the accurate boundingbox. Estimated boundingboxes 
        /// can be computed much (much) faster than accurate (or "tight") bounding boxes. 
        /// Estimated bounding boxes are always similar to or larger than accurate bounding boxes.</param>
        /// <returns>
        /// The boundingbox of the geometry in world coordinates or BoundingBox.Empty 
        /// if not bounding box could be found.
        /// </returns>
        /// <example>
        /// <code source='examples\vbnet\ex_curveboundingbox.vb' lang='vbnet'/>
        /// <code source='examples\cs\ex_curveboundingbox.cs' lang='cs'/>
        /// <code source='examples\py\ex_curveboundingbox.py' lang='py'/>
        /// </example>
        [System.Xml.Serialization.XmlIgnore]
        public BoundingBox BoundingBox { get; private set; }
        public BoundingBox BoundingBoxAccurate { get; }

        public ComponentIndex ComponentIndex { get; set; }


        /*
        [System.Xml.Serialization.XmlElement("UserString")]
        public System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, string>> XMLUserStringProxy
        {
            get
            {
                return new System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, string>>(this.UserString);
            }
            set
            {
                this.UserString = new System.Collections.Generic.Dictionary<string, string>();
                foreach (var pair in value)
                    this.UserString[pair.Key] = pair.Value;
            }
        }

        [System.Xml.Serialization.XmlIgnore]
        System.Collections.Generic.Dictionary<string, string> UserString = new System.Collections.Generic.Dictionary<string, string>();
        */
    }

    // DO NOT make public
    class UnknownGeometry : GeometryBase
    {
        public UnknownGeometry()
        {

        }
    }
}
