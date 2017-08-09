using System;
using System.Runtime.InteropServices;

namespace NN.Geometry
{
  /// <summary>
  /// Represents the center plane, radius and height values in a right circular cone.
  /// </summary>
  [StructLayout(LayoutKind.Sequential, Pack = 8, Size = 144)]
  [Serializable]
  public struct Cone : IEpsilonComparable<Cone>
  {
    internal Plane m_baseplane;
    internal double m_height;
    internal double m_radius;

    #region constants
    /// <summary>
    /// Gets an invalid Cone.
    /// </summary>
    public static Cone Unset
    {
      get
      {
        return new Cone(Plane.Unset, RhinoMath.UnsetValue, RhinoMath.UnsetValue);
      }
    }
    #endregion

    #region constructors
    /// <summary>
    /// Initializes a new cone with a specified base plane, height and radius.
    /// </summary>
    /// <param name="plane">Base plane of cone.</param>
    /// <param name="height">Height of cone.</param>
    /// <param name="radius">Radius of cone.</param>
    public Cone(Plane plane, double height, double radius)
    {
      m_baseplane = plane;
      m_height = height;
      m_radius = radius;
    }
    #endregion

    #region properties
    /// <summary>
    /// Gets or sets the base plane of the cone.
    /// </summary>
    public Plane Plane
    {
      get { return m_baseplane; }
      set { m_baseplane = value; }
    }

    /// <summary>
    /// Gets or sets the height of the circular right cone.
    /// </summary>
    public double Height
    {
      get { return m_height; }
      set { m_height = value; }
    }

    /// <summary>
    /// Gets or sets the radius of the cone.
    /// </summary>
    public double Radius
    {
      get { return m_radius; }
      set { m_radius = value; }
    }

    /// <summary>
    /// true if plane is valid, height is not zero and radius is not zero.
    /// </summary>
    public bool IsValid
    {
      get
      {
        if (!RhinoMath.IsValidDouble(m_height)) { return false; }
        if (!RhinoMath.IsValidDouble(m_radius)) { return false; }
        return m_baseplane.IsValid && m_height != 0 && m_radius != 0;
      }
    }

    /// <summary>Center of base circle.</summary>
    public Point3d BasePoint
    {
      get
      {
        return m_baseplane.Origin + m_height * m_baseplane.ZAxis;
      }
    }

    /// <summary>Point at tip of the cone.</summary>
    public Point3d ApexPoint
    {
      get
      {
        return m_baseplane.Origin;
      }
    }

    /// <summary>Unit vector axis of cone.</summary>
    public Vector3d Axis
    {
      get
      {
        return m_baseplane.ZAxis;
      }
    }

    #endregion

    #region methods
    /// <summary>
    /// Computes the angle (in radians) between the axis and the 
    /// side of the cone.
    /// The angle and the height have the same sign.
    /// </summary>
    /// <returns>Math.Atan(Radius / Height) if the height is not 0; 0 if the radius is 0; Math.PI otherwise.</returns>
    public double AngleInRadians()
    {
      return m_height == 0.0 ? (m_radius != 0.0 ? Math.PI : 0.0) : Math.Atan(m_radius / m_height);
    }
    
    /// <summary>
    /// Computes the angle (in degrees) between the axis and the 
    /// side of the cone.
    /// The angle and the height have the same sign.
    /// </summary>
    /// <returns>An angle in degrees.</returns>
    public double AngleInDegrees()
    {
      return 180.0 * AngleInRadians() / Math.PI;
    }

    
    #endregion

    /// <summary>
    /// Check that all values in other are within epsilon of the values in this
    /// </summary>
    /// <param name="other"></param>
    /// <param name="epsilon"></param>
    /// <returns></returns>
    public bool EpsilonEquals(Cone other, double epsilon)
    {
      return m_baseplane.EpsilonEquals(other.m_baseplane, epsilon) &&
             RhinoMath.EpsilonEquals(m_height, other.m_height, epsilon) &&
             RhinoMath.EpsilonEquals(m_radius, other.m_radius, epsilon);
    }
  }
}