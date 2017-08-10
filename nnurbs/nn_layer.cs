#pragma warning disable 1591
using System;
using System.Runtime.Serialization;
using System.Collections.Generic;


namespace NN.DocObjects
{
    [Serializable]
    public class Layer 
    {
        // Represents both a CRhinoLayer and an ON_Layer. When m_ptr is
        // null, the object uses m_doc and m_id to look up the const
        // CRhinoLayer in the layer table.

        readonly Guid m_id = Guid.Empty;
        readonly NN.FileIO.File3dm m_onx_model;

        public Layer()
        {

        }


        internal Layer(Guid id, NN.FileIO.File3dm model)
        {
            m_id = id;
            m_onx_model = model;
        }
        
        /// <summary>Gets or sets the name of this layer.</summary>
        /// <example>
        /// <code source='examples\vbnet\ex_sellayer.vb' lang='vbnet'/>
        /// <code source='examples\cs\ex_sellayer.cs' lang='cs'/>
        /// <code source='examples\py\ex_sellayer.py' lang='py'/>
        /// </example>
        /// <remarks>If you are modifying a layer inside a Rhino document, 
        /// you must call CommitChanges for the modifications to take effect.</remarks>
        /// <example>
        /// <code source='examples\vbnet\ex_renamelayer.vb' lang='vbnet'/>
        /// <code source='examples\cs\ex_renamelayer.cs' lang='cs'/>
        /// <code source='examples\py\ex_renamelayer.py' lang='py'/>
        /// </example>
        public string Name;

        /// <summary>
        /// Gets the full path to this layer. The full path includes nesting information.
        /// </summary>
        /// <example>
        /// <code source='examples\vbnet\ex_locklayer.vb' lang='vbnet'/>
        /// <code source='examples\cs\ex_locklayer.cs' lang='cs'/>
        /// <code source='examples\py\ex_locklayer.py' lang='py'/>
        /// </example>
        public string FullPath;
        

        public override string ToString()
        {
            return FullPath;
        }

        /// <summary>
        /// Gets or sets the index of this layer.
        /// </summary>
        public int LayerIndex;

        /// <summary>
        /// Gets or sets the ID of this layer object. 
        /// You typically do not need to assign a custom ID.
        /// </summary>
        public Guid Id;

        /// <summary>
        /// Gets the ID of the parent layer. Layers can be origanized in a hierarchical structure, 
        /// in which case this returns the parent layer ID. If the layer has no parent, 
        /// Guid.Empty will be returned.
        /// </summary>
        /// <example>
        /// <code source='examples\vbnet\ex_addchildlayer.vb' lang='vbnet'/>
        /// <code source='examples\cs\ex_addchildlayer.cs' lang='cs'/>
        /// <code source='examples\py\ex_addchildlayer.py' lang='py'/>
        /// </example>
        public Guid ParentLayerId;

        /// <summary>
        /// Gets or sets the IGES level for this layer.
        /// </summary>
        public int IgesLevel;

        /// <summary>
        /// Gets or sets the display color for this layer.
        /// </summary>
        /// <remarks>If you are modifying a layer inside a Rhino document, 
        /// you must call CommitChanges for the modifications to take effect.</remarks>
        public ColorEx Color;

        /// <summary>
        /// Gets or sets the plot color for this layer.
        /// </summary>
        /// <remarks>If you are modifying a layer inside a Rhino document, 
        /// you must call CommitChanges for the modifications to take effect.</remarks>
        public ColorEx PlotColor;

        /// <summary>
        /// Gets or sets the thickness of the plotting pen in millimeters. 
        /// A thickness of 0.0 indicates the "default" pen weight should be used.
        /// </summary>
        /// <remarks>If you are modifying a layer inside a Rhino document, 
        /// you must call CommitChanges for the modifications to take effect.</remarks>
        public double PlotWeight;

        /// <summary>
        /// Gets or sets the line-type index for this layer.
        /// </summary>
        /// <remarks>If you are modifying a layer inside a Rhino document, 
        /// you must call CommitChanges for the modifications to take effect.</remarks>
        public int LinetypeIndex;

        /// <summary>
        /// Gets or sets the index of render material for objects on this layer that have
        /// MaterialSource() == MaterialFromLayer. 
        /// A material index of -1 indicates no material has been assigned 
        /// and the material created by the default Material constructor 
        /// should be used.
        /// </summary>
        /// <remarks>If you are modifying a layer inside a Rhino document, 
        /// you must call CommitChanges for the modifications to take effect.</remarks>
        public int RenderMaterialIndex;

        /// <summary>
        /// Gets or sets the visibility of this layer.
        /// </summary>
        /// <remarks>If you are modifying a layer inside a Rhino document, 
        /// you must call CommitChanges for the modifications to take effect.</remarks>
        public bool IsVisible;


        /// <summary>
        /// Gets or sets a value indicating the locked state of this layer.
        /// </summary>
        /// <remarks>If you are modifying a layer inside a Rhino document, 
        /// you must call CommitChanges for the modifications to take effect.</remarks>
        /// <example>
        /// <code source='examples\vbnet\ex_locklayer.vb' lang='vbnet'/>
        /// <code source='examples\cs\ex_locklayer.cs' lang='cs'/>
        /// <code source='examples\py\ex_locklayer.py' lang='py'/>
        /// </example>
        public bool IsLocked;

        public bool PersistentVisibility;

        public bool UnsetPersistentVisibility;

        /// <summary>
        /// The persistent locking setting is used for layers that can be locked by
        /// a "parent" object. A common case is when a layer is a child layer
        /// (Layer.ParentI is not nil). In this case, when a parent layer is locked,
        /// then child layers are also locked. The persistent locking setting
        /// determines what happens when the parent is unlocked again.
        /// </summary>
        /// <returns></returns>
        public bool PersistentLocking;


        public bool UnsetPersistentLocking;

        /// <summary>
        /// Gets or sets a value indicating whether this layer is expanded in the Rhino Layer dialog.
        /// </summary>
        /// <remarks>If you are modifying a layer inside a Rhino document, 
        /// you must call CommitChanges for the modifications to take effect.</remarks>
        public bool IsExpanded;

        /// <summary>
        /// Gets a value indicating whether this layer has been deleted and is 
        /// currently in the Undo buffer.
        /// </summary>
        public bool IsDeleted;

        /// <summary>
        /// Gets a value indicting whether this layer is a referenced layer. 
        /// Referenced layers are part of referenced documents.
        /// </summary>
        public bool IsReference;

        /// <summary>
        /// Runtime index used to sort layers in layer dialog.
        /// </summary>
        public int SortIndex;

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