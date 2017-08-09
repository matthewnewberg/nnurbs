using System;
using System.Runtime.InteropServices;

namespace NN.Geometry
{
  /// <summary>
  /// Represents the values of a plane, a radius and two heights -on top and beneath-
  /// that define a right circular cylinder.
  /// </summary>
  [StructLayout(LayoutKind.Sequential, Pack = 8, Size = 152)]
  public struct Cylinder : IEpsilonComparable<Cylinder>
  {
    #region members
    Circle m_basecircle;
    double m_height1;
    double m_height2;
    #endregion

    #region constants
    /// <summary>
    /// Gets an invalid Cylinder.
    /// </summary>
    public static Cylinder Unset
    {
      get
      {
        return new Cylinder(Circle.Unset, RhinoMath.UnsetValue);
      }
    }
    #endregion

    #region constructors
    // If m_height1 == m_height2, the cylinder is infinite,
    // Otherwise, m_height1 < m_height2 and the center of
    // the "bottom" cap is 
    //   m_basecircle.plane.origin + m_height1*m_basecircle.plane.zaxis,
    // and the center of the top cap is 
    //   m_basecircle.plane.origin + m_height2*m_basecircle.plane.zaxis.

    /// <summary>
    /// Constructs a new cylinder with infinite height.
    /// </summary>
    /// <param name="baseCircle">Base circle for infinite cylinder.</param>
    public Cylinder(Circle baseCircle)
    {
      m_basecircle = baseCircle;
      m_height1 = 0.0;
      m_height2 = 0.0;
    }

    /// <summary>
    /// Constructs a new cylinder with a finite height.
    /// </summary>
    /// <param name="baseCircle">Base circle for cylinder.</param>
    /// <param name="height">Height of cylinder (zero for infinite cylinder).</param>
    /// <example>
    /// <code source='examples\vbnet\ex_addcylinder.vb' lang='vbnet'/>
    /// <code source='examples\cs\ex_addcylinder.cs' lang='cs'/>
    /// <code source='examples\py\ex_addcylinder.py' lang='py'/>
    /// </example>
    public Cylinder(Circle baseCircle, double height)
    {
      m_basecircle = baseCircle;
      if (height > 0.0)
      {
        m_height1 = 0.0;
        m_height2 = height;
      }
      else
      {
        m_height1 = height;
        m_height2 = 0.0;
      }
    }
    #endregion

    #region properties

    /// <summary>
    /// Gets a boolean value indicating whether this cylinder is valid.
    /// <para>A valid cylinder is represented by a valid circle and two valid heights.</para>
    /// </summary>
    public bool IsValid
    {
      get
      {
        if (!m_basecircle.IsValid) { return false; }
        if (!RhinoMath.IsValidDouble(m_height1)) { return false; }
        if (!RhinoMath.IsValidDouble(m_height2)) { return false; }

        return true;
      }
    }

    /// <summary>
    /// true if the cylinder is finite (Height0 != Height1)
    /// false if the cylinder is infinite.
    /// </summary>
    public bool IsFinite
    {
      get
      {
        return m_height1!=m_height2;
      }
    }

    /// <summary>
    /// Gets the center point of the defining circle.
    /// </summary>
    public Point3d Center
    {
      get
      {
        return m_basecircle.Plane.Origin;
      }
    }

    /// <summary>
    /// Gets the axis direction of the cylinder.
    /// </summary>
    public Vector3d Axis
    {
      get
      {
        return m_basecircle.Plane.ZAxis;
      }
    }

    /// <summary>
    /// Gets the height of the cylinder. 
    /// Infinite cylinders have a height of zero, not Double.PositiveInfinity.
    /// </summary>
    public double TotalHeight
    {
      get
      {
        return m_height2 - m_height1;
      }
    }

    /// <summary>
    /// Gets or sets the start height of the cylinder. 
    /// </summary>
    public double Height1
    {
      get { return m_height1; }
      set { m_height1 = value; }
    }

    /// <summary>
    /// Gets or sets the end height of the cylinder. 
    /// If the end height equals the start height, the cylinder is 
    /// presumed to be infinite.
    /// </summary>
    public double Height2
    {
      get { return m_height2; }
      set { m_height2 = value; }
    }
    #endregion

    /// <summary>
    /// Compute the circle at the given elevation parameter.
    /// </summary>
    /// <param name="linearParameter">Height parameter for circle section.</param>
    public Circle CircleAt(double linearParameter)
    {
      Circle c = m_basecircle;
      if (linearParameter != 0)
        c.Translate(linearParameter * c.Plane.ZAxis);
      return c;
    }

    /// <summary>
    /// Compute the line at the given angle parameter. This line will be degenerate if the cylinder is infite.
    /// </summary>
    /// <param name="angularParameter">Angle parameter for line section.</param>
    public Line LineAt(double angularParameter)
    {
      Point3d p = m_basecircle.PointAt(angularParameter);
      Vector3d z = m_basecircle.Plane.ZAxis;
      Point3d from = p + m_height1 * z;
      Point3d to = p + m_height2 * z;
      Line line = new Line(from, to);
      return line;
    }

     /// <summary>
    /// Check that all values in other are within epsilon of the values in this
    /// </summary>
    /// <param name="other"></param>
    /// <param name="epsilon"></param>
    /// <returns></returns>
    public bool EpsilonEquals(Cylinder other, double epsilon)
    {
      return m_basecircle.EpsilonEquals(other.m_basecircle, epsilon) &&
             RhinoMath.EpsilonEquals(m_height1, other.m_height1, epsilon) &&
             RhinoMath.EpsilonEquals(m_height2, other.m_height2, epsilon);
    }
  }
}
