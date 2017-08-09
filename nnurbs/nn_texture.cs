using System;
using System.Runtime.Serialization;
using NN.Geometry;

namespace NN.DocObjects
{
    /// <summary>
    /// The TextureType controls how the pixels in the bitmap
    /// are interpreted.
    /// </summary>
    public enum TextureType : int
    {
        /// <summary> 
        /// </summary>
        None = 0,
        /// <summary>
        /// Standard image texture
        /// </summary>
        Bitmap = 1,
        /// <summary>
        /// bump map
        /// </summary>
        Bump = 2,
        /// <summary>
        /// value = alpha
        /// </summary>
        Transparency = 3
    }

    /// <summary>
    /// Determines how this texture is combined with others in a material's
    /// texture list.
    /// </summary>
    public enum TextureCombineMode : int
    {
        /// <summary>
        /// </summary>
        None = 0,
        /// <summary>
        /// Modulate with material diffuse color
        /// </summary>
        Modulate = 1,
        /// <summary>
        /// Decal
        /// </summary>
        Decal = 2,
        /// <summary>
        /// Blend texture with others in the material
        ///   To "add" a texture, set BlendAmount = +1
        ///   To "subtract" a texture, set BlendAmount = -1
        /// </summary>
        Blend = 3,
    }

    /// <summary>
    /// Defines Texture UVW wrapping modes
    /// </summary>
    public enum TextureUvwWrapping : int
    {
        /// <summary>
        /// Repeat the texture
        /// </summary>
        Repeat = 0,
        /// <summary>
        /// Clamp the texture
        /// </summary>
        Clamp = 1
    }

    /// <summary>
    /// Represents a texture that is mapped on objects.
    /// </summary>
    [Serializable]
    public class Texture 
    {
        /// <summary>
        /// Gets or sets a file name that is used by this texture.
        /// NOTE: this filename may well not be a path that makes sense
        /// on a user's computer because it was a path initially set on
        /// a different user's computer. If you want to get a workable path
        /// for this user, use the BitmapTable.Find function using this
        /// property.
        /// </summary>
        public string FileName;

        /// <summary>
        /// Gets the globally unique identifier of this texture.
        /// </summary>
        public Guid Id;
        

        /// <summary>
        /// If the texture is enabled then it will be visible in the rendered
        /// display otherwise it will not.
        /// </summary>
        public bool Enabled;
        

        /// <summary>
        /// Controls how the pixels in the bitmap are interpreted
        /// </summary>
        public TextureType TextureType;

        /// <summary>
        /// 
        /// </summary>
        public int MappingChannelId;

        /// <summary>
        /// Determines how this texture is combined with others in a material's
        /// texture list.
        /// </summary>
        public TextureCombineMode TextureCombineMode;

        //skipping for now
        //  FILTER m_minfilter;
        //  FILTER m_magfilter;

        const int IDX_WRAPMODE_U = 0;
        const int IDX_WRAPMODE_V = 1;
        const int IDX_WRAPMODE_W = 2;


        /// <summary>
        /// Texture wrapping mode in the U direction
        /// </summary>
        public TextureUvwWrapping WrapU;


        /// <summary>
        /// Texture wrapping mode in the V direction
        /// </summary>
        public TextureUvwWrapping WrapV;

        /// <summary>
        /// Texture wrapping mode in the W direction
        /// </summary>
        public TextureUvwWrapping WrapW;

        /// <summary>
        /// If true then the UVW transform is applied to the texture
        /// otherwise the UVW transform is ignored.
        /// </summary>
        public bool ApplyUvwTransform;
        
        /// <summary>
        /// Transform to be applied to each instance of this texture
        /// if ApplyUvw is true
        /// </summary>
        public Transform UvwTransform;


        // skipping for now
        //  ON_Color m_border_color;
        //  ON_Color m_transparent_color;
        //  ON_UUID m_transparency_texture_id;
        //  ON_Interval m_bump_scale;

        /// <summary>
        /// If the TextureCombineMode is Blend, then the blending function
        /// for alpha is determined by
        /// <para>
        /// new alpha = constant
        ///             + a0*(current alpha)
        ///             + a1*(texture alpha)
        ///             + a2*min(current alpha,texture alpha)
        ///             + a3*max(current alpha,texture alpha)
        /// </para>
        /// </summary>
        /// <param name="constant"></param>
        /// <param name="a0"></param>
        /// <param name="a1"></param>
        /// <param name="a2"></param>
        /// <param name="a3"></param>
        public void GetAlphaBlendValues(out double constant, out double a0, out double a1, out double a2, out double a3)
        {
            constant = 0;
            a0 = 0;
            a1 = 0;
            a2 = 0;
            a3 = 0;
        }

        /// <summary>
        /// If the TextureCombineMode is Blend, then the blending function
        /// for alpha is determined by
        /// <para>
        /// new alpha = constant
        ///             + a0*(current alpha)
        ///             + a1*(texture alpha)
        ///             + a2*min(current alpha,texture alpha)
        ///             + a3*max(current alpha,texture alpha)
        /// </para>
        /// </summary>
        /// <param name="constant"></param>
        /// <param name="a0"></param>
        /// <param name="a1"></param>
        /// <param name="a2"></param>
        /// <param name="a3"></param>
        public void SetAlphaBlendValues(double constant, double a0, double a1, double a2, double a3)
        {
            //IntPtr ptr_this = NonConstPointer();
        }

        // skipping for now
        //  ON_Color m_blend_constant_RGB;
        //  double m_blend_RGB[4];
        //  int m_blend_order;
    }
}
