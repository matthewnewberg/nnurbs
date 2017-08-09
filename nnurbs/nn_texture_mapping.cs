using System;
using NN.Geometry;

namespace NN.Render
{
    /// <summary>
    /// Defines enumerated constants for mapping types such as planar, cylindrical or spherical.
    /// </summary>
    public enum TextureMappingType : int
    {
        NoMapping = 0,
        SrfpMapping = 1, // u,v = linear transform of surface params,w = 0
        PlaneMapping = 2, // u,v,w = 3d coordinates wrt frame
        CylinderMapping = 3, // u,v,w = longitude, height, radius
        SphereMapping = 4, // (u,v,w) = longitude,latitude,radius
        BoxMapping = 5,
        MeshMappingPrimitive = 6, // m_mapping_primitive is an ON_Mesh
        SrfMappingPrimitive = 7, // m_mapping_primitive is an ON_Surface
        BrepMappingPrimitive = 8, // m_mapping_primitive is an ON_Brep
    }

    /// <summary>
    /// Represents a texture mapping.
    /// </summary>
    public class TextureMapping
    {

        /// <summary>
        /// Texture mapping type associated with this Mapping object.
        /// </summary>
        public TextureMappingType MappingType;


        /// <summary>
        /// The unique Id for this texture mapping object.
        /// </summary>
        public Guid Id;

        /// <summary>
        /// Transform applied to mapping coordinate (u,v,w) to convert it into a
        /// texture coordinate.
        /// </summary>
        public Transform UvwTransform;

        /// <summary>
        /// For primitive based mappings, these transformations are used to map
        /// the world coordinate (x,y,z) point P and  surface normal N before it is
        /// projected to the normalized mapping primitive. The surface normal
        /// transformation, m_Nxyz, is always calculated from m_Pxyz.  It is a
        /// runtime setting that is not saved in 3dm files. If m_type is
        /// srfp_mapping, then m_Pxyz and m_Nxyz are ignored.
        /// </summary>
        public Transform PrimativeTransform;

        /// <summary>
        /// For primitive based mappings, these transformations are used to map
        /// the world coordinate (x,y,z) point P and  surface normal N before it is
        /// projected to the normalized mapping primitive. The surface normal
        /// transformation, m_Nxyz, is always calculated from m_Pxyz.  It is a
        /// runtime setting that is not saved in 3dm files. If m_type is
        /// srfp_mapping, then m_Pxyz and m_Nxyz are ignored.
        /// </summary>
        public Transform NormalTransform;
    }
}
