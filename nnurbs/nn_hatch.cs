using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace NN.Geometry
{
  /// <summary>
  /// Represents a hatch in planar boundary loop or loops.
  /// This is a 2d entity with a plane defining a local coordinate system.
  /// The loops, patterns, angles, etc are all in this local coordinate system.
  /// The Hatch object manages the plane and loop array
  /// Fill definitions are in the HatchPattern or class derived from HatchPattern
  /// Hatch has an index to get the pattern definition from the pattern table.
  /// </summary>
  [Serializable]
  public class Hatch : GeometryBase
  {
    public Hatch()
    {
    }
        
    public System.Collections.Generic.List<Curve> Curves3dInner = new System.Collections.Generic.List<Curve>();
    public System.Collections.Generic.List<Curve> Curves3dOutput = new System.Collections.Generic.List<Curve>();
            
    public int PatternIndex { get; set; }
    
    /// <summary>
    /// Gets or sets the relative rotation of the pattern.
    /// </summary>
    public double PatternRotation { get; set; }
    
    /// <summary>
    /// Gets or sets the scaling factor of the pattern.
    /// </summary>
    public double PatternScale { get; set; }
    
  }
}

namespace NN.DocObjects
{
    public enum HatchPatternFillType
    {
        Solid = 0,
        Lines = 1,
        Gradient = 2
    }

    [Serializable]
    public class HatchPattern 
    {
        // Represents both a CRhinoHatchPattern and an ON_HatchPattern. When m_ptr is
        // null, the object uses m_doc and m_id to look up the const
        // CRhinoHatchPattern in the hatch pattern table.
        readonly Guid m_id = Guid.Empty;

        public HatchPattern()
        {

        }

        internal HatchPattern(Guid id)
        {
            m_id = id;
        }


        /// <summary>
        /// Index in the hatch pattern table for this pattern. -1 if not in the table.
        /// </summary>
        public int Index;

        /// <example>
        /// <code source='examples\vbnet\ex_hatchcurve.vb' lang='vbnet'/>
        /// <code source='examples\cs\ex_hatchcurve.cs' lang='cs'/>
        /// <code source='examples\py\ex_hatchcurve.py' lang='py'/>
        /// </example>
        public string Name;
        
        public string Description;
        public HatchPatternFillType FillType;
    }
}