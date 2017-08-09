using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using NN.Runtime.InteropWrappers;

namespace NN.Runtime
{
    /// <summary>
    /// Represents the error that happen when a class user attempts to execute a modifying operation
    /// on an object that has been added to a document.
    /// </summary>
    [Serializable]
    public class DocumentCollectedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the document controlled exception class.
        /// </summary>
        public DocumentCollectedException() :
          base("This object cannot be modified because it is controlled by a document.")
        { }
    }

    /// <summary>
    /// Base class for .NET classes that wrap C++ unmanaged Rhino classes.
    /// </summary>
    [Serializable]
    public abstract class CommonObject
    {
        /// <summary>
        /// Allows construction from inheriting classes.
        /// </summary>
        public CommonObject() { }

        /// <summary>
        /// Gets true if this class has any custom information attached to it through UserData.
        /// </summary>
        public bool HasUserData { get; set; }

        NN.DocObjects.Custom.UserDataList m_userdatalist;
        /// <summary>
        /// List of custom information that is attached to this class.
        /// </summary>
        public NN.DocObjects.Custom.UserDataList UserData
        {
            get
            {
                return m_userdatalist ?? (m_userdatalist = new DocObjects.Custom.UserDataList());
            }
            set { m_userdatalist = value; }
        }

        /// <summary>
        /// Dictionary of custom information attached to this class. The dictionary is actually user
        /// data provided as an easy to use sharable set of information.
        /// </summary>
        public NN.Collections.ArchivableDictionary UserDictionary
        {
            get
            {
                NN.DocObjects.Custom.UserDictionary ud = UserData.Find(typeof(NN.DocObjects.Custom.SharedUserDictionary)) as NN.DocObjects.Custom.SharedUserDictionary;
                if (ud == null)
                {
                    ud = new DocObjects.Custom.SharedUserDictionary();
                    UserData.Add(ud);
                }
                return ud.Dictionary;
            }
            set
            {


            }
        }
    }

    class ConstCastHolder
    {
        public object m_oldparent;
        public ConstCastHolder(CommonObject obj, object old_parent)
        {
            m_oldparent = old_parent;
        }
    }

}


namespace NN.DocObjects
{
    /// <summary>
    /// Attributes (color, material, layer,...) associated with a rhino object
    /// </summary>
    [Serializable]
    public class ObjectAttributes
    {
        public ObjectAttributes() { }

#if RHINO3DMIO || RHINOCOMMON
        public ObjectAttributes(Rhino.DocObjects.ObjectAttributes f)
        {
            CopyFrom(f);
        }

        public bool CopyFrom(Rhino.DocObjects.ObjectAttributes from)
        {

            if (from == null)
                return false;

            this.Mode = (NN.DocObjects.ObjectMode)from.Mode;
            this.IsInstanceDefinitionObject = from.IsInstanceDefinitionObject;
            this.Visible = from.Visible;
            this.LinetypeSource = (NN.DocObjects.ObjectLinetypeSource)from.LinetypeSource;
            this.ColorSource = (NN.DocObjects.ObjectColorSource)from.ColorSource;
            this.PlotColorSource = (NN.DocObjects.ObjectPlotColorSource)from.PlotColorSource;
            this.PlotWeightSource = (NN.DocObjects.ObjectPlotWeightSource)from.PlotWeightSource;
            this.ObjectId = from.ObjectId;
            this.Name = from.Name;
            this.LayerIndex = from.LayerIndex;
            this.LinetypeIndex = from.LinetypeIndex;
            this.MaterialIndex = from.MaterialIndex;
            this.MaterialSource = (NN.DocObjects.ObjectMaterialSource)from.MaterialSource;
            // TODO ??
            //		this.MaterialRefs =  new NN.DocObjects.MaterialRefs(from.MaterialRefs);
            this.ObjectColor = from.ObjectColor;
            this.PlotColor = from.PlotColor;
            this.HasMapping = from.HasMapping;
            this.PlotWeight = from.PlotWeight;
            this.ObjectDecoration = (NN.DocObjects.ObjectDecoration)from.ObjectDecoration;
            this.WireDensity = from.WireDensity;
            this.ViewportId = from.ViewportId;
            this.Space = (NN.DocObjects.ActiveSpace)from.Space;
            this.GroupCount = from.GroupCount;
            // TODO convert to GUID
            if (from.GetGroupList() != null)
                this.GroupList = new System.Collections.Generic.List<int>(from.GetGroupList());

            this.UserStringCount = from.UserStringCount;
            return true;
        }


        public bool CopyTo(Rhino.DocObjects.ObjectAttributes to)
        {
            to.Mode = (Rhino.DocObjects.ObjectMode)this.Mode;
            //to.IsInstanceDefinitionObject = this.IsInstanceDefinitionObject;
            to.Visible = this.Visible;
            to.LinetypeSource = (Rhino.DocObjects.ObjectLinetypeSource)this.LinetypeSource;
            to.ColorSource = (Rhino.DocObjects.ObjectColorSource)this.ColorSource;
            to.PlotColorSource = (Rhino.DocObjects.ObjectPlotColorSource)this.PlotColorSource;
            to.PlotWeightSource = (Rhino.DocObjects.ObjectPlotWeightSource)this.PlotWeightSource;
            to.ObjectId = this.ObjectId;
            to.Name = this.Name;
            to.LayerIndex = this.LayerIndex;
            to.LinetypeIndex = this.LinetypeIndex;
            to.MaterialIndex = this.MaterialIndex;
            to.MaterialSource = (Rhino.DocObjects.ObjectMaterialSource)this.MaterialSource;
            /// TODO ??? 
            //to.MaterialRefs = this.MaterialRefs;
            to.ObjectColor = this.ObjectColor;
            to.PlotColor = this.PlotColor;
            to.PlotWeight = this.PlotWeight;
            to.ObjectDecoration = (Rhino.DocObjects.ObjectDecoration)this.ObjectDecoration;
            to.WireDensity = this.WireDensity;
            to.ViewportId = this.ViewportId;
            to.Space = (Rhino.DocObjects.ActiveSpace)this.Space;

            if (this.GroupList != null)
                for (int i = 0; i < this.GroupList.Count; ++i)
                    to.AddToGroup(i);


            return true;
        }

        public Rhino.DocObjects.ObjectAttributes RhinoObject()
        {
            var rhinoObject = new Rhino.DocObjects.ObjectAttributes();
            CopyTo(rhinoObject);
            return rhinoObject;
        }
#endif



        /// <summary>
        /// An object must be in one of three modes: normal, locked or hidden.
        /// If an object is in normal mode, then the object's layer controls visibility
        /// and selectability. If an object is locked, then the object's layer controls
        /// visibility by the object cannot be selected. If the object is hidden, it is
        /// not visible and it cannot be selected.
        /// </summary>
        public ObjectMode Mode;

        /// <summary>
        /// Use this query to determine if an object is part of an instance definition.
        /// </summary>
        public bool IsInstanceDefinitionObject;

        /// <summary>object visibility.</summary>
        public bool Visible;


        /// <summary>
        /// The Linetype used to display an object is specified in one of two ways.
        /// If LinetypeSource is ON::linetype_from_layer, then the object's layer ON_Layer::Linetype() is used.
        /// If LinetypeSource is ON::linetype_from_object, then value of m_linetype is used.
        /// </summary>
        public ObjectLinetypeSource LinetypeSource;


        public ObjectColorSource ColorSource { get; set; }


        /// <summary>
        /// The color used to plot an object on paper is specified in one of three ways.
        /// If PlotColorSource is ON::plot_color_from_layer, then the object's layer ON_Layer::PlotColor() is used.
        /// If PlotColorSource is ON::plot_color_from_object, then value of PlotColor() is used.
        /// </summary>
        public ObjectPlotColorSource PlotColorSource { get; set; }

        public ObjectPlotWeightSource PlotWeightSource { get; set; }

        public Guid ObjectId { get; set; }

        /// <summary>
        /// Gets or sets an object optional text name.
        /// <para>More than one object in a model can have the same name and
        /// some objects may have no name.</para>
        /// </summary>
        public string Name { get; set; }

        public int LayerIndex { get; set; }


        /// Gets or sets the linetype index.
        /// <para>Linetype definitions in an OpenNURBS model are stored in a linetype table.
        /// The linetype table is conceptually an array of ON_Linetype classes. Every
        /// OpenNURBS object in a model references some linetype.  The object's linetype
        /// is specified by zero based indicies into the ON_Linetype array.</para>
        /// <para>Index 0 is reserved for continuous linetype (no pattern).</para>
        /// </summary>
        public int LinetypeIndex { get; set; }

        public Guid LineTypeGuid { get; set; }

        /// <summary>
        /// Gets or sets the material index.
        /// <para>If you want something simple and fast, set the index of
        /// the rendering material.</para>
        /// </summary>
        /*
         * ...and ignore m_rendering_attributes. If you are developing
         * a high quality plug-in renderer, and a user is assigning one of your fabulous
         * rendering materials to this object, then add rendering material information to
         * the m_rendering_attributes.m_materials[] array.
        */
        public int MaterialIndex { get; set; }

        public Guid MaterialGuid { get; set; }


        // [skipping]
        // ON_ObjectRenderingAttributes m_rendering_attributes;

        /// <summary>
        /// Determines if the simple material should come from the object or from it's layer.
        /// High quality rendering plug-ins should use m_rendering_attributes.
        /// </summary>
        public ObjectMaterialSource MaterialSource { get; set; }


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
        public MaterialRefs MaterialRefs { get; set; }

        /// <summary>
        /// If ON::color_from_object == ColorSource, then color is the object's display color.
        /// </summary>
        /// <example>
        /// <code source='examples\vbnet\ex_modifyobjectcolor.vb' lang='vbnet'/>
        /// <code source='examples\cs\ex_modifyobjectcolor.cs' lang='cs'/>
        /// <code source='examples\py\ex_modifyobjectcolor.py' lang='py'/>
        /// </example>
        public System.Drawing.Color ObjectColor { get; set; }

        public System.Drawing.Color PlotColor { get; set; }

        /// <summary>
        /// A mapping from any plugin source is associated with these attributes
        /// Need to do this here to respond correctly to ModifyObjectAttributes event
        /// </summary>
        public bool HasMapping { get; set; }


        /// <summary>
        /// Plot weight in millimeters.
        /// =0.0 means use the default width
        /// &lt;0.0 means don't plot (visible for screen display, but does not show on plot)
        /// </summary>
        public double PlotWeight { get; set; }


        /// <summary>
        /// Used to indicate an object has a decoration (like an arrowhead on a curve)
        /// </summary>
        public ObjectDecoration ObjectDecoration { get; set; }


        /// <summary>
        /// When a surface object is displayed in wireframe, this controls
        /// how many isoparametric wires are used.
        /// value    number of isoparametric wires
        /// -1       boundary wires (off)
        /// 0        boundary and knot wires 
        /// 1        boundary and knot wires and, if there are no interior knots, a single interior wire.
        /// N>=2     boundary and knot wires and (N+1) interior wires.
        /// </summary>
        /// <example>
        /// <code source='examples\vbnet\ex_isocurvedensity.vb' lang='vbnet'/>
        /// <code source='examples\cs\ex_isocurvedensity.cs' lang='cs'/>
        /// <code source='examples\py\ex_isocurvedensity.py' lang='py'/>
        /// </example>
        public int WireDensity { get; set; }


        /// <summary>
        /// If ViewportId is nil, the object is active in all viewports. If ViewportId is not nil, then 
        /// this object is only active in a specific view. This field is primarily used to assign page
        /// space objects to a specific page, but it can also be used to restrict model space to a
        /// specific view.
        /// </summary>
        public Guid ViewportId { get; set; }


        /// <summary>
        /// Starting with V4, objects can be in either model space or page space.
        /// If an object is in page space, then ViewportId is not nil and
        /// identifies the page it is on.
        /// </summary>
        public ActiveSpace Space { get; set; }


        /// <summary>number of groups object belongs to.</summary>
        public int GroupCount { get; set; }


        public System.Collections.Generic.List<int> GroupList = new System.Collections.Generic.List<int>();

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

        public int UserStringCount { get; set; }

    }
}
