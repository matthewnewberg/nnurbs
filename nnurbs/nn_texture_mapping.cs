using System;
using Rhino.Geometry;

namespace Rhino.Render
{
  /// <summary>
  /// Defines enumerated constants for mapping types such as planar, cylindrical or spherical.
  /// </summary>
  public enum TextureMappingType : int
  {
    /// <summary>No mapping is selected.</summary>
    None = UnsafeNativeMethods.TextureMappingType.NoMapping,

    /// <summary>(u, v) = linear transform of surface params, w = 0.</summary>
    SurfaceParameters = UnsafeNativeMethods.TextureMappingType.SrfpMapping,

    /// <summary>(u, v, w) = 3d coordinates wrt frame.</summary>
    PlaneMapping = UnsafeNativeMethods.TextureMappingType.PlaneMapping,

    /// <summary>(u, v, w) = longitude, height, radius.</summary>
    CylinderMapping = UnsafeNativeMethods.TextureMappingType.CylinderMapping,

    /// <summary>(u, v, w) = longitude,latitude,radius.</summary>
    SphereMapping = UnsafeNativeMethods.TextureMappingType.SphereMapping,

    /// <summary>Box mapping type.</summary>
    BoxMapping = UnsafeNativeMethods.TextureMappingType.BoxMapping,

    /// <summary>Mapping primitive is a mesh.</summary>
    MeshMappingPrimitive = UnsafeNativeMethods.TextureMappingType.MeshMappingPrimitive,

    /// <summary>Mapping primitive is a surface.</summary>
    SurfaceMappingPrimitive = UnsafeNativeMethods.TextureMappingType.SrfMappingPrimitive,

    /// <summary>Mapping primitive is a brep.</summary>
    BrepMappingPrimitive = UnsafeNativeMethods.TextureMappingType.BrepMappingPrimitive
  }

  /// <summary>
  /// Represents a texture mapping.
  /// </summary>
  public class TextureMapping : Rhino.Runtime.CommonObject
  {
    private TextureMapping()
    {
      IntPtr ptr = UnsafeNativeMethods.ON_TextureMapping_New();
      ConstructNonConstObject(ptr);
    }
    internal TextureMapping(IntPtr pTextureMapping)
    {
      ConstructNonConstObject(pTextureMapping);
    }

    /// <summary>
    /// Texture mapping type associated with this Mapping object.
    /// </summary>
    public TextureMappingType MappingType
    {
      get
      {
        var ptr = ConstPointer();
        var value = UnsafeNativeMethods.ON_TextureMapping_GetMappingType(ptr);
        switch (value)
        {
          case UnsafeNativeMethods.TextureMappingType.NoMapping:
            return TextureMappingType.None;
          case UnsafeNativeMethods.TextureMappingType.SrfpMapping:
            return TextureMappingType.SurfaceParameters;
          case UnsafeNativeMethods.TextureMappingType.PlaneMapping:
            return TextureMappingType.PlaneMapping;
          case UnsafeNativeMethods.TextureMappingType.CylinderMapping:
            return TextureMappingType.CylinderMapping;
          case UnsafeNativeMethods.TextureMappingType.SphereMapping:
            return TextureMappingType.SphereMapping;
          case UnsafeNativeMethods.TextureMappingType.BoxMapping:
            return TextureMappingType.BoxMapping;
          case UnsafeNativeMethods.TextureMappingType.MeshMappingPrimitive:
            return TextureMappingType.BrepMappingPrimitive;
          case UnsafeNativeMethods.TextureMappingType.SrfMappingPrimitive:
            return TextureMappingType.SurfaceMappingPrimitive;
          case UnsafeNativeMethods.TextureMappingType.BrepMappingPrimitive:
            return TextureMappingType.BrepMappingPrimitive;
        }
        throw new Exception("Unknown TextureMappingType");
      }
    }

    /// <summary>
    /// The unique Id for this texture mapping object.
    /// </summary>
    public Guid Id
    {
      get
      {
        return UnsafeNativeMethods.ON_TextureMapping_GetId(ConstPointer());
      }
    }

    /// <summary>
    /// Transform applied to mapping coordinate (u,v,w) to convert it into a
    /// texture coordinate.
    /// </summary>
    public Transform UvwTransform
    {
      get
      {
        var ptr = ConstPointer();
        var value = Transform.Identity;
        UnsafeNativeMethods.ON_TextureMapping_GetTransform(ptr, UnsafeNativeMethods.TextureMappingGetTransform.UVW, ref value);
        return value;
      }
      set
      {
        var ptr = NonConstPointer();
        UnsafeNativeMethods.ON_TextureMapping_SetTransform(ptr, UnsafeNativeMethods.TextureMappingGetTransform.UVW, ref value);
      }
    }

    /// <summary>
    /// For primitive based mappings, these transformations are used to map
    /// the world coordinate (x,y,z) point P and  surface normal N before it is
    /// projected to the normalized mapping primitive. The surface normal
    /// transformation, m_Nxyz, is always calculated from m_Pxyz.  It is a
    /// runtime setting that is not saved in 3dm files. If m_type is
    /// srfp_mapping, then m_Pxyz and m_Nxyz are ignored.
    /// </summary>
    public Transform PrimativeTransform
    {
      get
      {
        var ptr = ConstPointer();
        var value = Transform.Identity;
        UnsafeNativeMethods.ON_TextureMapping_GetTransform(ptr, UnsafeNativeMethods.TextureMappingGetTransform.Pxyz, ref value);
        return value;
      }
      set
      {
        var ptr = NonConstPointer();
        UnsafeNativeMethods.ON_TextureMapping_SetTransform(ptr, UnsafeNativeMethods.TextureMappingGetTransform.Pxyz, ref value);
      }
    }

    /// <summary>
    /// For primitive based mappings, these transformations are used to map
    /// the world coordinate (x,y,z) point P and  surface normal N before it is
    /// projected to the normalized mapping primitive. The surface normal
    /// transformation, m_Nxyz, is always calculated from m_Pxyz.  It is a
    /// runtime setting that is not saved in 3dm files. If m_type is
    /// srfp_mapping, then m_Pxyz and m_Nxyz are ignored.
    /// </summary>
    public Transform NormalTransform
    {
      get
      {
        var ptr = ConstPointer();
        var value = Transform.Identity;
        UnsafeNativeMethods.ON_TextureMapping_GetTransform(ptr, UnsafeNativeMethods.TextureMappingGetTransform.Nxyz, ref value);
        return value;
      }
      set
      {
        var ptr = NonConstPointer();
        UnsafeNativeMethods.ON_TextureMapping_SetTransform(ptr, UnsafeNativeMethods.TextureMappingGetTransform.Nxyz, ref value);
      }
    }

    /// <summary>
    /// 	Get a box projection from the texture mapping.
    /// </summary>
    /// <param name="plane">
    /// The center of the box is at plane.origin and the sides of the box are
    /// parallel to the plane's coordinate planes.
    /// </param>
    /// <param name="dx">
    /// The "front" and "back" sides of the box are in spanned by the vectors
    /// plane.yaxis and plane.zaxis.  The back plane contains the point
    /// plane.PointAt(dx[0],0,0) and the front plane contains the point
    /// plane.PointAt(dx[1],0,0).
    /// </param>
    /// <param name="dy">
    /// The "left" and "right" sides of the box are in spanned by the vectors
    /// plane.zaxis and plane.xaxis.  The left plane contains the point
    /// plane.PointAt(0,dx[0],0) and the back plane contains the point
    /// plane.PointAt(0,dy[1],0).
    /// </param>
    /// <param name="dz">
    /// The "top" and "bottom" sides of the box are in spanned by the vectors
    /// plane.xaxis and plane.yaxis.  The bottom plane contains the point
    /// plane.PointAt(0,0,dz[0]) and the top plane contains the point
    /// plane.PointAt(0,0,dz[1]).
    /// </param>
    /// <returns>
    /// Returns true if a valid box is returned.
    /// </returns>
    /// <remarks>
    /// Generally, GetMappingBox will not return the same parameters passed to
    /// SetBoxMapping.  However, the location of the box will be the same.
    /// </remarks>
    public bool TryGetMappingBox(out Plane plane, out Interval dx, out Interval dy, out Interval dz)
    {
      var ptr = ConstPointer();
      plane = Plane.Unset;
      dx = Interval.Unset;
      dy = Interval.Unset;
      dz = Interval.Unset;
      var success = UnsafeNativeMethods.ON_TextureMapping_GetMappingBox(ptr, ref plane, ref dx, ref dy, ref dz);
      return success;
    }

    /// <summary>
    /// Get a spherical projection parameters from this texture mapping.
    /// </summary>
    /// <param name="sphere">/// </param>
    /// <returns>
    /// Returns true if a valid sphere is returned.
    /// </returns>
    /// <remarks>
    /// Generally, GetMappingShere will not return the same parameters passed
    /// to SetSphereMapping.  However, the location of the sphere will be the
    /// same.  If this mapping is not cylindrical, the cylinder will
    /// approximate the actual mapping primitive.
    /// </remarks>
    public bool TryGetMappingSphere(out Sphere sphere)
    {
      var ptr = ConstPointer();
      sphere = Sphere.Unset;
      var success = UnsafeNativeMethods.ON_TextureMapping_GetMappingSphere(ptr, ref sphere);
      return success;
    }

    /// <summary>
    /// Get a cylindrical projection parameters from this texture mapping.
    /// </summary>
    /// <param name="cylinder"></param>
    /// <returns>
    /// Returns true if a valid cylinder is returned.
    /// </returns>
    /// <remarks>
    /// Generally, GetMappingCylinder will not return the same parameters passed
    /// to SetCylinderMapping.  However, the location of the cylinder will be
    /// the same.  If this mapping is not cylindrical, the cylinder will
    /// approximate the actual mapping primitive.
    /// </remarks>
    public bool TryGetMappingCylinder(out Cylinder cylinder)
    {
      var ptr = ConstPointer();
      cylinder = Cylinder.Unset;
      var success = UnsafeNativeMethods.ON_TextureMapping_GetMappingCylinder(ptr, ref cylinder);
      return success;
    }

    /// <summary>
    /// Get plane mapping parameters from this texture mapping.
    /// </summary>
    /// <param name="plane"></param>
    /// <param name="dx">
    /// Portion of the plane's x axis that is mapped to [0,1]
    /// </param>
    /// <param name="dy">
    /// Portion of the plane's y axis that is mapped to [0,1]
    /// </param>
    /// <param name="dz">
    /// Portion of the plane's z axis that is mapped to [0,1]
    /// </param>
    /// <returns>
    /// Return true if valid plane mapping parameters were returned.
    /// </returns>
    /// <remarks>
    /// NOTE WELL:
    ///  Generally, GetMappingPlane will not return the same parameters passed
    ///  to SetPlaneMapping.  However, the location of the plane will be the
    ///  same.
    /// </remarks>
    public bool TryGetMappingPlane(out Plane plane, out Interval dx, out Interval dy, out Interval dz)
    {
      var ptr = ConstPointer();
      plane = Plane.Unset;
      dx = Interval.Unset;
      dy = Interval.Unset;
      dz = Interval.Unset;
      var success = UnsafeNativeMethods.ON_TextureMapping_GetMappingPlane(ptr, ref plane, ref dx, ref dy, ref dz);
      return success;
    }

    /// <summary>Create a planar projection texture mapping</summary>
    /// <param name="plane">A plane to use for mapping.</param>
    /// <param name="dx">portion of the plane's x axis that is mapped to [0,1] (can be a decreasing interval)</param>
    /// <param name="dy">portion of the plane's y axis that is mapped to [0,1] (can be a decreasing interval)</param>
    /// <param name="dz">portion of the plane's z axis that is mapped to [0,1] (can be a decreasing interval)</param>
    /// <returns>TextureMapping instance if input is valid</returns>
    public static TextureMapping CreatePlaneMapping(Plane plane, Interval dx, Interval dy, Interval dz)
    {
      TextureMapping rc = new TextureMapping();
      IntPtr pMapping = rc.NonConstPointer();
      if (!UnsafeNativeMethods.ON_TextureMapping_SetPlaneMapping(pMapping, ref plane, dx, dy, dz))
      {
        rc.Dispose();
        rc = null;
      }
      return rc;
    }

    /// <summary>Create a cylindrical projection texture mapping.</summary>
    /// <param name="cylinder">
    /// cylinder in world space used to define a cylindrical coordinate system.
    /// The angular parameter maps (0,2pi) to texture "u" (0,1), The height
    /// parameter maps (height[0],height[1]) to texture "v" (0,1), and the
    /// radial parameter maps (0,r) to texture "w" (0,1).
    /// </param>
    /// <param name="capped">
    /// If true, the cylinder is treated as a finite capped cylinder
    /// </param>
    /// <remarks>
    /// When the cylinder is capped and m_texture_space = divided, the
    /// cylinder is mapped to texture space as follows:
    /// The side is mapped to 0 &lt;= "u" &lt;= 2/3.
    /// The bottom is mapped to 2/3 &lt;= "u" &lt;= 5/6.
    /// The top is mapped to 5/6 &lt;= "u" &lt;= 5/6.
    /// This is the same convention box mapping uses.
    /// </remarks>
    /// <returns>TextureMapping instance if input is valid</returns>
    public static TextureMapping CreateCylinderMapping(Cylinder cylinder, bool capped)
    {
      TextureMapping rc = new TextureMapping();
      IntPtr pMapping = rc.NonConstPointer();
      if (!UnsafeNativeMethods.ON_TextureMapping_SetCylinderMapping(pMapping, ref cylinder, capped))
      {
        rc.Dispose();
        rc = null;
      }
      return rc;
    }

    /// <summary>
    /// Create a spherical projection texture mapping.
    /// </summary>
    /// <param name="sphere">
    /// sphere in world space used to define a spherical coordinate system.
    /// The longitude parameter maps (0,2pi) to texture "u" (0,1).
    /// The latitude paramter maps (-pi/2,+pi/2) to texture "v" (0,1).
    /// The radial parameter maps (0,r) to texture "w" (0,1).
    /// </param>
    /// <returns>TextureMapping instance if input is valid</returns>
    public static TextureMapping CreateSphereMapping(Sphere sphere)
    {
      TextureMapping rc = new TextureMapping();
      IntPtr pMapping = rc.NonConstPointer();
      if( !UnsafeNativeMethods.ON_TextureMapping_SetSphereMapping(pMapping, ref sphere) )
      {
        rc.Dispose();
        rc = null;
      }
      return rc;
    }

    /// <summary>Create a box projection texture mapping.</summary>
    /// <param name="plane">
    /// The sides of the box the box are parallel to the plane's coordinate
    /// planes.  The dx, dy, dz intervals determine the location of the sides.
    /// </param>
    /// <param name="dx">
    /// Determines the location of the front and back planes. The vector
    /// plane.xaxis is perpendicular to these planes and they pass through
    /// plane.PointAt(dx[0],0,0) and plane.PointAt(dx[1],0,0), respectivly.
    /// </param>
    /// <param name="dy">
    /// Determines the location of the left and right planes. The vector
    /// plane.yaxis is perpendicular to these planes and they pass through
    /// plane.PointAt(0,dy[0],0) and plane.PointAt(0,dy[1],0), respectivly.
    /// </param>
    /// <param name="dz">
    /// Determines the location of the top and bottom planes. The vector
    /// plane.zaxis is perpendicular to these planes and they pass through
    /// plane.PointAt(0,0,dz[0]) and plane.PointAt(0,0,dz[1]), respectivly.
    /// </param>
    /// <param name="capped">
    /// If true, the box is treated as a finite capped box.
    /// </param>
    /// <remarks>
    /// When m_texture_space = divided, the box is mapped to texture space as follows:
    /// If the box is not capped, then each side maps to 1/4 of the texture map.
    /// v=1+---------+---------+---------+---------+
    ///   | x=dx[1] | y=dy[1] | x=dx[0] | y=dy[0] |
    ///   | Front   | Right   | Back    | Left    |
    ///   | --y-&gt;   | &lt;-x--   | &lt;-y--   | --x-&gt;   |
    /// v=0+---------+---------+---------+---------+
    /// 0/4 &lt;=u&lt;= 1/4 &lt;=u&lt;= 2/4 &lt;=u&lt;= 3/4 &lt;=u&lt;= 4/4
    /// If the box is capped, then each side and cap gets 1/6 of the texture map.
    /// v=1+---------+---------+---------+---------+---------+---------+
    ///   | x=dx[1] | y=dy[1] | x=dx[0] | y=dy[0] | z=dx[1] | z=dz[0] |
    ///   | Front   | Right   | Back    | Left    | Top     |  Bottom |
    ///   | --y-&gt;   | &lt;x--   | &lt;-y--   | --x-&gt;   | --x-&gt;   | --x-&gt;   |
    /// v=0+---------+---------+---------+---------+---------+---------+
    /// 0/6 &lt;=u&lt;= 1/6 &lt;=u&lt;= 2/6 &lt;=u&lt;= 3/6 &lt;=u&lt;= 4/6 &lt;=u&lt;= 5/6 &lt;=u&lt;= 6/6 
    /// </remarks>
    /// <returns>TextureMapping instance if input is valid</returns>
    public static TextureMapping CreateBoxMapping(Plane plane, Interval dx, Interval dy, Interval dz, bool capped)
    {
      TextureMapping rc = new TextureMapping();
      IntPtr pMapping = rc.NonConstPointer();
      if (!UnsafeNativeMethods.ON_TextureMapping_SetBoxMapping(pMapping, ref plane, dx, dy, dz, capped))
      {
        rc.Dispose();
        rc = null;
      }
      return rc;
    }

    internal override IntPtr _InternalGetConstPointer()
    {
      return IntPtr.Zero;
    }

    internal override IntPtr _InternalDuplicate(out bool applymempressure)
    {
      applymempressure = false;
      IntPtr pConstPointer = ConstPointer();
      return UnsafeNativeMethods.ON_Object_Duplicate(pConstPointer);
    }
  }
}
