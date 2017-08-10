
using System.Collections;
#pragma warning disable 1591
using System;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace NN.DocObjects
{
    public class MaterialRef : IDisposable
    {
        internal MaterialRef(IntPtr pointer, Guid plugInId)
        {
            // TODO?
           // m_temp_pointer = pointer;
          //  PlugInId = plugInId;
        }

        internal MaterialRef(MaterialRefs parent, Guid plugInId)
        {
            // TODO ??
            //m_parent = parent;
            //PlugInId = plugInId;
        }


        #region Public properties
        /// <summary>
        /// Determines if the simple material should come from the object or from
        /// it's layer.
        /// </summary>
        public ObjectMaterialSource MaterialSource { get; set; }

        /// <summary>
        /// Identifies a rendering plug-in
        /// </summary>
        public Guid PlugInId { get; private set; }
        /// <summary>
        /// The Id of the Material used to render the front of an object.
        /// </summary>
        public Guid FrontFaceMaterialId { get; set;}
        /// <summary>
        /// The Id of the Material used to render the back of an object.
        /// </summary>
        public Guid BackFaceMaterialId { get; set; }
        /// <summary>
        /// The index of the material used to render the front of an object
        /// </summary>
        public int FrontFaceMaterialIndex { get; set; }
        /// <summary>
        /// The index of the material used to render the back of an object
        /// </summary>
        public int BackFaceMaterialIndex { get; set; }
        #endregion Public properties

        #region IDisposable implementation
        public void Dispose() { Dispose(true); }
        ~MaterialRef() { Dispose(false); }
        void Dispose(bool disposing)
        {
        }
        #endregion IDisposable implementation
    }

    /// <summary>
    /// Options passed to MaterialRefs.Create
    /// </summary>
    public class MaterialRefCreateParams
    {
        /// <summary>
        /// Identifies a rendering plug-in
        /// </summary>
        public Guid PlugInId { get; set; }
        /// <summary>
        /// Determines if the simple material should come from the object or from
        /// it's layer.
        /// </summary>
        public ObjectMaterialSource MaterialSource { get; set; }
        /// <summary>
        /// The Id of the Material used to render the front of an object.
        /// </summary>
        public Guid FrontFaceMaterialId { get; set; }
        /// <summary>
        /// The index of the material used to render the front of an object
        /// </summary>
        public int FrontFaceMaterialIndex { get; set; }
        /// <summary>
        /// The Id of the Material used to render the back of an object.
        /// </summary>
        public Guid BackFaceMaterialId { get; set; }
        /// <summary>
        /// The index of the material used to render the back of an object
        /// </summary>
        public int BackFaceMaterialIndex { get; set; }
    }

    /// <summary>
    /// If you are developing a high quality plug-in renderer, and a user is
    /// assigning a custom render material to this object, then add rendering
    /// material information to the MaterialRefs dictionary.
    /// 
    /// Note to developers:
    ///  As soon as the MaterialRefs dictionary contains items rendering
    ///  material queries slow down.  Do not populate the MaterialRefs
    /// dictionary when setting the MaterialIndex will take care of your needs.
    /// </summary>
    public class MaterialRefs
    {
        [System.Xml.Serialization.XmlElement("Materials")]
        public System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<Guid, MaterialRef>> XmlMaterialsProxy
        {
            get
            {
                return new System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<Guid, MaterialRef>>(this.Materials);
            }
            set
            {
                this.Materials = new System.Collections.Generic.Dictionary<Guid, MaterialRef>();
                foreach (var pair in value)
                    this.Materials[pair.Key] = pair.Value;
            }
        }

        [System.Xml.Serialization.XmlIgnore]
        System.Collections.Generic.Dictionary<Guid, MaterialRef> Materials = new System.Collections.Generic.Dictionary<Guid, MaterialRef>();
    }
    
    class MaterialHolder
    {
        Material m_material;
        readonly bool m_is_opennurbs_material;

        public MaterialHolder(Material material, bool isOpenNurbsMaterial)
        {
            m_material = material;
            m_is_opennurbs_material = isOpenNurbsMaterial;
        }
        public void Done()
        {
            m_material = null;
        }

        Material Material { get { return m_material; } }
        
        public bool IsOpenNurbsMaterial
        {
            get { return m_is_opennurbs_material; }
        }
        
     }

    [Serializable]
    public class Material 
    {
                             
        public Material ()
        {
            Id = Guid.Empty;
        }

#if RHINO3DMIO || RHINOCOMMON
        public Material(Rhino.DocObjects.Material m)
        {
            CopyFrom(m);
        }
        public bool CopyFrom(Rhino.DocObjects.Material from)
        {
            if (from == null)
                return false;

            this.Id = from.Id;
            this.RenderPlugInId = from.RenderPlugInId;
            this.IsDefaultMaterial = from.IsDefaultMaterial;
            this.MaterialIndex = from.MaterialIndex;
            this.Name = from.Name;
            //		this.MaxShine =  new System.Double(from.MaxShine);
            this.Shine = from.Shine;
            this.Transparency = from.Transparency;
            this.IndexOfRefraction = from.IndexOfRefraction;
            this.Reflectivity = from.Reflectivity;
            this.DiffuseColor = from.DiffuseColor;
            this.AmbientColor = from.AmbientColor;
            this.EmissionColor = from.EmissionColor;
            this.SpecularColor = from.SpecularColor;
            this.ReflectionColor = from.ReflectionColor;
            this.TransparentColor = from.TransparentColor;
            return true;
        }


        public bool CopyTo(Rhino.DocObjects.Material to)
        {
            // 
            //to.Id = this.Id;
            to.RenderPlugInId = this.RenderPlugInId;

            //to.IsDefaultMaterial = this.IsDefaultMaterial;
            //to.MaterialIndex = this.MaterialIndex;

            to.Name = this.Name;
            //		to.MaxShine = this.MaxShine;
            to.Shine = this.Shine;
            to.Transparency = this.Transparency;
            to.IndexOfRefraction = this.IndexOfRefraction;
            to.Reflectivity = this.Reflectivity;
            to.DiffuseColor = this.DiffuseColor;
            to.AmbientColor = this.AmbientColor;
            to.EmissionColor = this.EmissionColor;
            to.SpecularColor = this.SpecularColor;
            to.ReflectionColor = this.ReflectionColor;
            to.TransparentColor = this.TransparentColor;

            return true;
        }
#endif


        internal Material(Guid id)
        {
            Id = id;
        }
        
      
        /// <summary>Gets or sets the ID of this material.</summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// The Id of the RenderPlugIn that is associated with this material.
        /// </summary>
        public Guid RenderPlugInId { get; set; }
        

        /// <summary>
        /// By default Rhino layers and objects are assigned the default rendering material.
        /// </summary>
        public bool IsDefaultMaterial { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public int MaterialIndex { get; set; }
        

        public string Name { get; set; }

        public static double MaxShine
        {
            get { return 255.0; }
        }

        /// <summary>
        /// Gets or sets the shine factor of the material.
        /// </summary>
        public double Shine { get; set; }
        

        /// <summary>
        /// Gets or sets the transparency of the material (0.0 = opaque to 1.0 = transparent)
        /// </summary>
        public double Transparency { get; set; }
        
        /// <summary>
        /// Gets or sets the index of refraction of the material, generally
        /// >= 1.0 (speed of light in vacuum)/(speed of light in material)
        /// </summary>
        public double IndexOfRefraction { get; set; }
        

        /// <summary>
        /// Gets or sets how reflective a material is, 0f is no reflection
        /// 1f is 100% reflective.
        /// </summary>
        public double Reflectivity { get; set; }


        public ColorEx DiffuseColor { get; set; }

        public ColorEx AmbientColor { get; set; }

        public ColorEx EmissionColor { get; set; }

        public ColorEx SpecularColor { get; set; }

        public ColorEx ReflectionColor { get; set; }

        public ColorEx TransparentColor { get; set; }


        [System.Xml.Serialization.XmlElement("Textures")]
        public List<KeyValuePair<int, Texture>> XMLDictionaryProxy
        {
            get
            {
                if (this.Textures == null)
                    return new List<KeyValuePair<int, Texture>>();

                return new List<KeyValuePair<int, Texture>>(this.Textures);
            }
            set
            {
                this.Textures = new Dictionary<int, Texture>();
                foreach (var pair in value)
                    this.Textures[pair.Key] = pair.Value;
            }
        }

        [System.Xml.Serialization.XmlIgnore]
        System.Collections.Generic.Dictionary<int, Texture> Textures { get; set; }

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
    }
}