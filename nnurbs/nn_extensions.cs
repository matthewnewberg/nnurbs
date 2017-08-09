using System;
using System.IO;
using System.Collections.Generic;
using NN.Geometry;
using NN.Runtime.InteropWrappers;
using NN.DocObjects;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

namespace NN.FileIO
{
    /// <summary>
    /// Represents a 3dm file, which is stored using the OpenNURBS file standard.
    /// <para>The 3dm format is the main Rhinoceros storage format.</para>
    /// <para>Visit http://www.opennurbs.com/ for more details.</para>
    /// </summary>
    public class File3dm
    {
        // Helper Function
        public static bool GeneratorCopyCode(object thisObject, object o, string objectNameTo, string objectNameFrom)
        {
            if (thisObject == null || o == null)
                return false;


            Uri exeUri = new Uri(thisObject.GetType().Assembly.CodeBase);

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(exeUri.LocalPath + "_" + thisObject.GetType().Name + "_" + o.GetType().Name + ".cs", false))
            {
                string className = o.GetType().FullName;

                file.WriteLine("\tpublic bool CopyFrom(" + className + " " + objectNameFrom + ") {");
                file.WriteLine("");
                file.WriteLine("\t\tif (" + objectNameFrom + " == null)");
                file.WriteLine("\t\t\treturn false;");
                file.WriteLine("");

                bool exception = false;

                // Fields
                foreach (var fieldInfo in o.GetType().GetFields())
                {
                    exception = false;

                    string currentLine = "";
                    object value = null;

                    try
                    {
                        value = fieldInfo.GetValue(o);

                        currentLine = "\t\t" + "this." + fieldInfo.Name + " = " + objectNameFrom + "." + fieldInfo.Name + ";";

                        if (null != value)
                        {
                            FieldInfo thisField = thisObject.GetType().GetField(fieldInfo.Name);

                            if (value.GetType().IsEnum)
                            {
                                currentLine = "\t\t" + "this." + fieldInfo.Name + " = " + "(" + thisField.FieldType.FullName + ") " + objectNameFrom + "." + fieldInfo.Name + ";";
                            }

                            if (thisField != null)
                            {
                                thisField.SetValue(thisObject, value);
                            }
                            else
                            {
                                currentLine += "// Missing This Prop";
                                exception = true; // No prop to assign this value
                            }
                        }
                    }
                    catch (Exception)
                    {
                        // Round two 

                        // Run  Copy Constructor

                        try
                        {
                            FieldInfo thisField = thisObject.GetType().GetField(fieldInfo.Name);

                            if (thisField != null)
                            {
                                currentLine = "\t\t" + "this." + fieldInfo.Name + " = " + " new " + thisField.FieldType + "(" + objectNameFrom + "." + fieldInfo.Name + ")" + ";";

                                object newValue = Activator.CreateInstance(thisField.FieldType, value);

                                thisField.SetValue(thisObject, newValue);
                            }
                        }
                        catch (Exception)
                        {
                            exception = true;
                        }
                    }

                    if (!exception)
                    {
                        file.WriteLine(currentLine);
                        //Console.WriteLine(currentLine);
                    }
                    else
                    {
                        file.WriteLine("//" + currentLine);
                        //Console.WriteLine("//" + currentLine);
                    }
                }


                // Properties
                foreach (PropertyInfo propertyInfo in o.GetType().GetProperties())
                {
                    exception = false;

                    string currentLine = "";
                    object value = null;

                    try
                    {
                        value = propertyInfo.GetValue(o, null);

                        currentLine = "\t\t" + "this." + propertyInfo.Name + " = " + objectNameFrom + "." + propertyInfo.Name + ";";

                        if (null != value)
                        {
                            PropertyInfo thisProp = thisObject.GetType().GetProperty(propertyInfo.Name);

                            if (thisProp.GetAccessors(true)[0].IsStatic)
                                continue;

                            if (value.GetType().IsEnum)
                            {
                                currentLine = "\t\t" + "this." + propertyInfo.Name + " = " + "(" + thisProp.PropertyType.FullName + ") " + objectNameFrom + "." + propertyInfo.Name + ";";
                            }

                            if (thisProp != null)
                            {
                                thisProp.SetValue(thisObject, value, null);
                            }
                            else
                            {
                                currentLine += "// Missing This Prop";
                                exception = true; // No prop to assign this value
                            }
                        }
                    }
                    catch (Exception)
                    {
                        // Round two 

                        // Run  Copy Constructor

                        try
                        {
                            PropertyInfo thisProp = thisObject.GetType().GetProperty(propertyInfo.Name);

                            if (thisProp.GetAccessors(true)[0].IsStatic)
                                continue;

                            if (thisProp != null)
                            {
                                currentLine = "\t\t" + "this." + propertyInfo.Name + " = " + " new " + thisProp.PropertyType + "(" + objectNameFrom + "." + propertyInfo.Name + ")" + ";";

                                object newValue = Activator.CreateInstance(thisProp.PropertyType, value);

                                thisProp.SetValue(thisObject, newValue, null);
                            }
                        }
                        catch (Exception)
                        {
                            exception = true;
                        }
                    }

                    if (!exception)
                    {
                        file.WriteLine(currentLine);
                        //Console.WriteLine(currentLine);
                    }
                    else
                    {
                        file.WriteLine("//" + currentLine);
                        //Console.WriteLine("//" + currentLine);
                    }
                }

                file.WriteLine("\t\t return true;");

                file.WriteLine("\t}");

                file.WriteLine("");
                file.WriteLine("");


                file.WriteLine("\tpublic bool CopyTo(" + className + " " + objectNameTo + ") {");

                foreach (PropertyInfo propertyInfo in o.GetType().GetProperties())
                {
                    string currentLine = "";
                    object value = null;
                    exception = false;

                    try
                    {
                        value = propertyInfo.GetValue(o, null);

                        currentLine = "\t\t" + objectNameTo + "." + propertyInfo.Name + " = " + "this." + propertyInfo.Name + ";";


                        if (null != value)
                        {
                            PropertyInfo thisProp = thisObject.GetType().GetProperty(propertyInfo.Name);

                            if (value.GetType().IsEnum)
                            {
                                currentLine = "\t\t" + objectNameTo + "." + propertyInfo.Name + " = " + "(" + propertyInfo.PropertyType.FullName + ") " + "this." + propertyInfo.Name + ";";
                            }

                            //if (thisProp != null)
                            //    thisProp.SetValue(thisObject, value, null);
                        }
                    }
                    catch (Exception)
                    {
                        // Console.WriteLine(e.ToString());
                        exception = true;
                    }

                    if (!exception)
                    {
                        file.WriteLine(currentLine);
                        //Console.WriteLine(currentLine);
                    }
                    else
                    {
                        file.WriteLine("//" + currentLine);
                        //Console.WriteLine("//" + currentLine);
                    }
                }

                file.WriteLine("\t return true;");

                file.WriteLine("\t}");
            }

            return true;
        }

        /// <summary></summary>

        [Flags]
        public enum TableTypeFilter : uint
        {
            None = 0,
            PropertiesTable = 0x000001,
            SettingsTable = 0x000002,
            BitmapTable = 0x000004,
            TextureMappingTable = 0x000008,
            MaterialTable = 0x000010,
            LinetypeTable = 0x000020,
            LayerTable = 0x000040,
            GroupTable = 0x000080,
            FontTable = 0x000100,
            FutureFontTable = 0x000200,
            DimstyleTable = 0x000400,
            LightTable = 0x000800,
            HatchpatternTable = 0x001000,
            InstanceDefinitionTable = 0x002000,
            ObjectTable = 0x004000,
            HistoryrecordTable = 0x008000,
            UserTable = 0x010000
        }

        /// <summary></summary>

        [Flags]
        public enum ObjectTypeFilter : uint
        {
            None = 0,
            Point = 1, // some type of ON_Point
            Pointset = 2, // some type of ON_PointCloud, ON_PointGrid, ...
            Curve = 4, // some type of ON_Curve like ON_LineCurve, ON_NurbsCurve, etc.
            Surface = 8, // some type of ON_Surface like ON_PlaneSurface, ON_NurbsSurface, etc.
            Brep = 0x10, // some type of ON_Brep
            Mesh = 0x20, // some type of ON_Mesh
            Annotation = 0x200, // some type of ON_Annotation
            InstanceDefinition = 0x800, // some type of ON_InstanceDefinition
            InstanceReference = 0x1000, // some type of ON_InstanceRef
            TextDot = 0x2000, // some type of ON_TextDot
            Detail = 0x8000, // some type of ON_DetailView
            Hatch = 0x10000, // some type of ON_Hatch
            Extrusion = 0x40000000, // some type of ON_Extrusion
            Any = 0xFFFFFFFF
        }


        private File3dmViewTable m_view_table;
        private File3dmViewTable m_named_view_table;
        private File3dmHistoryRecordTable m_history_record_table;


        //int m_3dm_file_version;
        //int m_3dm_opennurbs_version;

        /// <summary>
        /// Gets or sets the start section comments, which are the comments with which the 3dm file begins.
        /// </summary>
        public string StartSectionComments { get; set; }

        /// <summary>
        /// Gets or sets the model notes.
        /// </summary>
        /// 
        /*
        public File3dmNotes Notes { get; set; }
        */

        // TODO change to array
        ///string GetString(int which)
        //void SetString(int which, string val)

        /// <summary>
        /// Gets or sets the name of the application that wrote this file.
        /// </summary>
        public string ApplicationName { get; set; }

        /// <summary>
        /// Gets or sets a URL for the application that wrote this file.
        /// </summary>
        public string ApplicationUrl { get; set; }

        /// <summary>
        /// Gets or sets details for the application that wrote this file.
        /// </summary>
        public string ApplicationDetails { get; set; }

        /// <summary>
        /// Gets a string that names the user who created the file.
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets a string that names the user who last edited the file.
        /// </summary>
        public string LastEditedBy { get; set; }


        /// <summary>
        /// Get the DateTime that this file was originally created. If the
        /// value is not set in the 3dm file, then DateTime.MinValue is returned
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Get the DateTime that this file was last edited. If the
        /// value is not set in the 3dm file, then DateTime.MinValue is returned
        /// </summary>
        public DateTime LastEdited { get; set; }

        /// <summary>
        /// Gets or sets the revision number.
        /// </summary>
        public int Revision { get; set; }


        public System.Drawing.Bitmap PreviewImage { get; set; }

        /// <summary>
        /// Settings include tolerance, and unit system, and defaults used
        /// for creating views and objects.
        /// </summary>
        /// 
        public File3dmSettings Settings { get; set; }

        /// <summary>
        /// Gets access to the <see cref="File3dmObjectTable"/> class associated with this file,
        /// which contains all objects.
        /// </summary>
        /// 
        public File3dmObjectTable Objects { get; set; }

        /// <summary>
        /// Materials in this file.
        /// </summary>

        public File3dmMaterialTable Materials { get; set; }


        /// <summary>
        /// Linetypes in this file.
        /// </summary>
        public File3dmLinetypeTable Linetypes { get; set; }

        /// <summary>
        /// Layers in this file.
        /// </summary>
        public File3dmLayerTable Layers { get; set; }

        /// <summary>
        /// Dimension Styles in this file
        /// </summary>
        public File3dmDimStyleTable DimStyles { get; set; }


        /// <summary>
        /// Hatch patterns in this file
        /// </summary>
        public File3dmHatchPatternTable HatchPatterns { get; set; }


        /// <summary>
        /// Instance definitions in this file
        /// </summary>
        public File3dmInstanceDefinitionTable InstanceDefinitions { get; set; }


        /// <summary>
        /// Views that represent the RhinoViews which are displayed when Rhino loads this file
        /// </summary>
        /// 
        public File3dmViewTable Views
        {
            get { return m_view_table ?? (m_view_table = new File3dmViewTable(false)); }
            set
            {
                m_view_table = value;
            }
        }

        /// <summary>
        /// Named view list
        /// </summary>
        public File3dmViewTable NamedViews
        {
            get { return m_named_view_table ?? (m_named_view_table = new File3dmViewTable(true)); }
            set
            {
                m_named_view_table = value;
            }
        }

        /// <summary>
        /// Custom plug-in data in this file.  This data is not attached to any geometry or attributes
        /// </summary>
        public File3dmPlugInDataTable PlugInData { get; set; }

        /// <summary>
        /// History records stored in this file
        /// </summary>
        public File3dmHistoryRecordTable HistoryRecords
        {
            get { return m_history_record_table ?? (m_history_record_table = new File3dmHistoryRecordTable()); }
        }

        /// <summary>
        /// Initializes a new instance of a 3dm file.
        /// </summary>
        public File3dm()
        {
        }

#if RHINO3DMIO || RHINOCOMMON
        public File3dm(Rhino.FileIO.File3dm f)
        {
            CopyFrom(f);
        }

#if RHINOCOMMON
        public File3dm(Rhino.RhinoDoc doc)
        {
            this.Objects = new File3dmObjectTable(doc);

            // TODO Other settings
        }
#endif

        public bool CopyTo(Rhino.FileIO.File3dm to)
        {
            to.StartSectionComments = this.StartSectionComments;
            //to.Notes = this.Notes;
            to.ApplicationName = this.ApplicationName;
            to.ApplicationUrl = this.ApplicationUrl;
            to.ApplicationDetails = this.ApplicationDetails;
            to.Revision = this.Revision;

            this.Settings.CopyTo(to.Settings);
            this.Objects.CopyTo(to.Objects);
            this.Materials.CopyTo(to.Materials);
            
            // TODO
            //to.Linetypes = this.Linetypes;
            //to.Layers = this.Layers;
            //to.DimStyles = this.DimStyles;
            //to.HatchPatterns = this.HatchPatterns;
            //to.InstanceDefinitions = this.InstanceDefinitions;
            //to.Views = this.Views;
            //to.NamedViews = this.NamedViews;
            //to.PlugInData = this.PlugInData;
            //to.HistoryRecords = this.HistoryRecords;
            return true;
        }

#if RHINOCOMMON
        public bool AddTo(Rhino.RhinoDoc doc)
        {
            return this.Objects.AddTo(doc);
        }
#endif


        public bool CopyFrom(Rhino.FileIO.File3dm from)
        {


            this.StartSectionComments = from.StartSectionComments;
            		//this.Notes = from.Notes;
            this.ApplicationName = from.ApplicationName;
            this.ApplicationUrl = from.ApplicationUrl;
            this.ApplicationDetails = from.ApplicationDetails;
            this.CreatedBy = from.CreatedBy;
            this.LastEditedBy = from.LastEditedBy;
            
            // TODO figure out why this is broken
            //this.Created = from.Created;

            this.LastEdited = from.LastEdited;
            this.Revision = from.Revision;
            this.Settings = new NN.FileIO.File3dmSettings(from.Settings);
            this.Objects = new NN.FileIO.File3dmObjectTable(from.Objects);
            this.Materials = new NN.FileIO.File3dmMaterialTable(from.Materials);
            //		this.Linetypes =  new NN.FileIO.File3dmLinetypeTable(from.Linetypes);
            //		this.Layers =  new NN.FileIO.File3dmLayerTable(from.Layers);
            //		this.DimStyles =  new NN.FileIO.File3dmDimStyleTable(from.DimStyles);
            //		this.HatchPatterns =  new NN.FileIO.File3dmHatchPatternTable(from.HatchPatterns);
            //		this.InstanceDefinitions =  new NN.FileIO.File3dmInstanceDefinitionTable(from.InstanceDefinitions);
            //		this.Views =  new NN.FileIO.File3dmViewTable(from.Views);
            //		this.NamedViews =  new NN.FileIO.File3dmViewTable(from.NamedViews);
            //		this.PlugInData =  new NN.FileIO.File3dmPlugInDataTable(from.PlugInData);
            //		this.HistoryRecords =  new NN.FileIO.File3dmHistoryRecordTable(from.HistoryRecords);

            return true;
        }
#endif
    }

    /// <summary>Options used by File3dm.Write</summary>
    public class File3dmWriteOptions
    {
        /// <summary>
        /// Initializes properties to defaults
        /// </summary>
        public File3dmWriteOptions()
        {
            Version = 5;
            SaveRenderMeshes = true;
            SaveAnalysisMeshes = true;
            SaveUserData = true;
        }


#if RHINO3DMIO || RHINOCOMMON|| RHINOCOMMON
        public File3dmWriteOptions(Rhino.FileIO.File3dmWriteOptions f)
        {
            CopyFrom(f);
        }

        public bool CopyTo(Rhino.FileIO.File3dmWriteOptions to)
        {

            return true;
        }

        public bool CopyFrom(Rhino.FileIO.File3dmWriteOptions f)
        {

            foreach (PropertyInfo propertyInfo in f.GetType().GetProperties())
            {
                object value = null;

                try
                {
                    value = propertyInfo.GetValue(f, null);

                    if (null != value)
                    {
                        PropertyInfo thisProp = this.GetType().GetProperty(propertyInfo.Name);

                        if (thisProp != null)
                            thisProp.SetValue(this, value, null);
                    }

                    //if (null != value) propertyInfo.SetValue(this, value, null);
                }
                catch (Exception)
                {
                    // Console.WriteLine(e.ToString());
                    Console.Write("Exception");
                }

                if (null != value)
                    Console.WriteLine(propertyInfo.Name + ":" + value.ToString());
                else
                    Console.WriteLine(propertyInfo.Name + ":");
            }

            return true;
        }
#endif

        /// <summary>
        /// File version. Default is 5
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// Include Render meshes in the file. Default is true
        /// </summary>
        public bool SaveRenderMeshes { get; set; }

        /// <summary>
        /// Include analysis meshes in the file. Default is true
        /// </summary>
        public bool SaveAnalysisMeshes { get; set; }

        /// <summary>
        /// Include custom user data in the file. Default is true
        /// </summary>
        public bool SaveUserData { get; set; }
    }

    /// <summary>
    /// Used to store geometry table object definition and attributes in a File3dm.
    /// </summary>
    public class File3dmObject
    {
        int m_index;
        readonly File3dm m_parent;

        public File3dmObject()
        {

        }

#if RHINO3DMIO || RHINOCOMMON
        public File3dmObject(Rhino.FileIO.File3dmObject f)
        {
            CopyFrom(f);
        }

#if RHINOCOMMON
        public File3dmObject(Rhino.DocObjects.RhinoObject obj)
        {
            this.Geometry = GeometryBase.CreateGeometery(obj.Geometry);
            this.Attributes = new NN.DocObjects.ObjectAttributes(obj.Attributes);
            this.Name = obj.Name;
        }
#endif

        public bool CopyFrom(Rhino.FileIO.File3dmObject from)
        {
            this.Geometry = GeometryBase.CreateGeometery(from.Geometry);
            this.Attributes = new NN.DocObjects.ObjectAttributes(from.Attributes);
            this.Name = from.Name;
            return true;
        }


        public bool CopyTo(Rhino.FileIO.File3dmObject to)
        {
            //		to.Geometry = this.Geometry;
            //		to.Attributes = this.Attributes;
            to.Name = this.Name;
            return true;
        }
#endif

        internal File3dmObject(int index, File3dm parent)
        {
            m_index = index;
            m_parent = parent;
        }

        /// <summary>
        /// Gets the geometry that is linked with this document object.
        /// </summary>

        [XmlElement(typeof(NN.Geometry.Brep))]
        [XmlElement(typeof(NN.Geometry.NurbsSurface))]
        [XmlElement(typeof(NN.Geometry.NurbsCurve))]
        [XmlElement(typeof(NN.Geometry.PolyCurve))]
        [XmlElement(typeof(NN.Geometry.PolylineCurve))]
        [XmlElement(typeof(NN.Geometry.LineCurve))]
        [XmlElement(typeof(NN.Geometry.ArcCurve))]
        [XmlElement(typeof(NN.Geometry.Mesh))]
        [XmlElement(typeof(NN.Geometry.Extrusion))]
        public GeometryBase Geometry { get; set; }

        /// <summary>
        /// Gets the attributes that are linked with this document object.
        /// </summary>
        public DocObjects.ObjectAttributes Attributes { get; set; }

        /// <summary>
        /// Gets or sets the Name of the object. Equivalent to this.Attributes.Name.
        /// </summary>
        public string Name
        {
            get { return Attributes.Name; }
            set
            {
                if (Attributes == null) Attributes = new DocObjects.ObjectAttributes();
                Attributes.Name = value;
            }
        }

        public int Index { get { return m_index; } set { m_index = value; } }
    }

    // Can't add a cref to an XML comment here since the ObjectTable is not included in the
    // OpenNURBS flavor build of RhinoCommon

    /// <summary>
    /// Represents a simple object table for a file that is open externally.
    /// <para>This class mimics NN.DocObjects.Tables.ObjectTable while providing external eccess to the file.</para>
    /// </summary>
    public class File3dmObjectTable : List<File3dmObject>, Collections.IRhinoTable<File3dmObject>
    {
        public File3dmObjectTable() { }

#if RHINO3DMIO || RHINOCOMMON
        public File3dmObjectTable(Rhino.FileIO.File3dmObjectTable f)
        {
            CopyFrom(f);
        }
#if RHINOCOMMON
        public File3dmObjectTable(Rhino.RhinoDoc doc)
        {
            this.Clear();

            foreach (var o in doc.Objects)
            {
                this.Add(new File3dmObject(o));
            }
        }
#endif
        public bool CopyFrom(Rhino.FileIO.File3dmObjectTable from)
        {
            this.Clear();

            foreach (var o in from)
            {
                this.Add(new File3dmObject(o));
            }

            return true;
        }

#if RHINOCOMMON
        public bool AddTo(Rhino.RhinoDoc to)
        {

            foreach (var o in this)
            {
                Rhino.DocObjects.ObjectAttributes rhinoAttrib = o.Attributes.RhinoObject();

                o.Attributes.CopyTo(rhinoAttrib);

                var brep = o.Geometry as Brep;

                if (brep != null)
                {
                    Rhino.Geometry.Brep rhinoBrep = new Rhino.Geometry.Brep();
                    brep.CopyTo(rhinoBrep);
                    to.Objects.AddBrep(rhinoBrep, rhinoAttrib);

                    continue;
                }

                var nurbsSurface = o.Geometry as NurbsSurface;
                if (nurbsSurface != null)
                    to.Objects.AddSurface(nurbsSurface.RhinoObject(), rhinoAttrib);

                var ext = o.Geometry as Extrusion;

                if (ext != null)
                    to.Objects.AddSurface(ext.RhinoObject(), rhinoAttrib);

                var arcCurve = o.Geometry as ArcCurve;

                if (arcCurve != null)
                {
                    to.Objects.AddArc(arcCurve.Arc.RhinoObject());
                }

                var nurbsCurve = o.Geometry as NurbsCurve;

                if (nurbsCurve != null)
                {
                    to.Objects.AddCurve(nurbsCurve.RhinoObject());
                }

                var polycurve = o.Geometry as PolyCurve;

                if (polycurve != null)
                {
                    to.Objects.AddCurve(polycurve.RhinoObject());
                }

                var polylinecurve = o.Geometry as PolylineCurve;

                if (polylinecurve != null)
                {
                    to.Objects.AddCurve(polylinecurve.RhinoObject(), rhinoAttrib);
                }

                var mesh = o.Geometry as Mesh;

                if (mesh != null)
                    to.Objects.AddMesh(mesh.RhinoObject(), rhinoAttrib);
            }

            return true;

        }
#endif

            public bool CopyTo(Rhino.FileIO.File3dmObjectTable to)
        {

#if RHINO3DMIO
            for (int i= to.Count-1; i>= 0; --i)
                to.Delete(to[i]);
#endif

            foreach (var o in this)
            {
                Rhino.DocObjects.ObjectAttributes rhinoAttrib = o.Attributes.RhinoObject(); 

                // TODO
                o.Attributes.CopyTo(rhinoAttrib);

                var brep = o.Geometry as Brep;

                if (brep != null)
                {
                    Rhino.Geometry.Brep rhinoBrep = new Rhino.Geometry.Brep();
                    brep.CopyTo(rhinoBrep);
                    to.AddBrep(rhinoBrep, rhinoAttrib);
                    continue;
                }

                var nurbsSurface = o.Geometry as NurbsSurface;
                if (nurbsSurface != null)
                    to.AddSurface(nurbsSurface.RhinoObject(), rhinoAttrib);
                
                var ext = o.Geometry as Extrusion;

                if (ext != null)
                    to.AddSurface(ext.RhinoObject(), rhinoAttrib);

                var arcCurve = o.Geometry as ArcCurve;

                if (arcCurve != null)
                {
                    to.AddArc(arcCurve.Arc.RhinoObject(), rhinoAttrib);
                }

                var nurbsCurve = o.Geometry as NurbsCurve;

                if (nurbsCurve != null)
                {
                    to.AddCurve(nurbsCurve.RhinoObject(), rhinoAttrib);
                }

                var polycurve = o.Geometry as PolyCurve;

                if (polycurve != null)
                {
                    to.AddCurve(polycurve.RhinoObject(), rhinoAttrib);
                }

                var polylinecurve = o.Geometry as PolylineCurve;

                if (polylinecurve != null)
                {
                    to.AddCurve(polylinecurve.RhinoObject(), rhinoAttrib);
                }

                var mesh = o.Geometry as Mesh;

                if (mesh != null)
                    to.AddMesh(mesh.RhinoObject(), rhinoAttrib);
            }

            return true;
        }
#endif

            readonly File3dm m_parent;
        internal File3dmObjectTable(File3dm parent)
        {
            m_parent = parent;
        }

        /// <summary>Gets the bounding box containing every object in this table.</summary>
        /// <returns>The computed bounding box.</returns>
        public NN.Geometry.BoundingBox BoundingBox { get; set; }
    }

    /// <summary>
    /// Custom data in the file supplied by a plug-in
    /// </summary>
    public class File3dmPlugInData
    {
        public File3dmPlugInData() { }

        internal File3dmPlugInData(Guid id)
        {
            PlugInId = id;
        }

        /// <summary>
        /// Plug-in this data is associated with
        /// </summary>
        public Guid PlugInId { get; set; }

    }

    /// <summary>
    /// Table of custom data provided by plug-ins
    /// </summary>
    public class File3dmPlugInDataTable : System.Collections.Generic.List<File3dmPlugInData>, Collections.IRhinoTable<File3dmPlugInData>
    {
        public File3dmPlugInDataTable()
        {
        }
    }

    /// <summary>
    /// Table of custom data provided by plug-ins
    /// </summary>
    public class File3dmHistoryRecordTable
    {
        public File3dmHistoryRecordTable() { }

        /// <summary>
        /// Gets the number of history records in this table.
        /// </summary>
        public int Count { get; set; }
    }

    public class File3dmMaterialTable : System.Collections.Generic.List<NN.DocObjects.Material>, Collections.IRhinoTable<NN.DocObjects.Material>
    {
        readonly File3dm m_parent;
        internal File3dmMaterialTable(File3dm parent)
        {
            m_parent = parent;
        }

        public File3dmMaterialTable() { }
#if RHINO3DMIO || RHINOCOMMON|| RHINOCOMMON
        public File3dmMaterialTable(IList<Rhino.DocObjects.Material> f)
        {
            CopyFrom(f);
        }

        public bool CopyFrom(IList<Rhino.DocObjects.Material> from)
        {
            this.Clear();

            foreach (var o in from)
            {
                this.Add(new Material(o));
            }

            return true;
        }


        public bool CopyTo(IList<Rhino.DocObjects.Material> to)
        {

            to.Clear();

            foreach (var m in this)
            {
                Rhino.DocObjects.Material newMaterial = new Rhino.DocObjects.Material();
                //m.CopyTo(material);
                to.Add(newMaterial);
            }

            return true;
        }
#endif

        public int Find(string name)
        {
            int cnt = Count;
            for (int i = 0; i < cnt; i++)
            {
                if (this[i].Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                    return i;
            }
            return -1;
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

    }

    public class File3dmLinetypeTable : System.Collections.Generic.List<DocObjects.Linetype>, Collections.IRhinoTable<DocObjects.Linetype>
    {
        public File3dmLinetypeTable()
        {
        }

        public int Find(string name)
        {
            int cnt = Count;
            for (int i = 0; i < cnt; i++)
            {
                if (this[i].Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                    return i;
            }
            return -1;
        }

        public bool IsReadOnly
        {
            get { return false; }
        }
    }

    public class File3dmLayerTable : System.Collections.Generic.List<DocObjects.Layer>, Collections.IRhinoTable<DocObjects.Layer>
    {
        public File3dmLayerTable()
        {
        }

        public int Find(string name)
        {
            int cnt = Count;
            for (int i = 0; i < cnt; i++)
            {
                if (this[i].Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                    return i;
            }
            return -1;
        }

        public bool IsReadOnly
        {
            get { return false; }
        }
    }

    public class File3dmDimStyleTable : System.Collections.Generic.List<DocObjects.DimensionStyle>, Collections.IRhinoTable<DocObjects.DimensionStyle>
    {
        public File3dmDimStyleTable()
        {
        }

        public int Find(string name)
        {
            int cnt = Count;
            for (int i = 0; i < cnt; i++)
            {
                if (this[i].Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                    return i;
            }
            return -1;
        }

        public bool IsReadOnly
        {
            get { return false; }
        }
    }

    public class File3dmHatchPatternTable : System.Collections.Generic.List<DocObjects.HatchPattern>, Collections.IRhinoTable<DocObjects.HatchPattern>
    {
        public File3dmHatchPatternTable()
        {
        }

        public int Find(string name)
        {
            int cnt = Count;
            for (int i = 0; i < cnt; i++)
            {
                if (this[i].Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                    return i;
            }
            return -1;
        }

    }

    public class File3dmInstanceDefinitionTable : System.Collections.Generic.List<InstanceDefinitionGeometry>, Collections.IRhinoTable<InstanceDefinitionGeometry>
    {

        public File3dmInstanceDefinitionTable() { }

        public int Find(string name)
        {
            int cnt = Count;
            for (int i = 0; i < cnt; i++)
            {
                if (this[i].Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                    return i;
            }
            return -1;
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

    }

    public class File3dmViewTable : System.Collections.Generic.List<DocObjects.ViewInfo>, Collections.IRhinoTable<DocObjects.ViewInfo>
    {

        File3dmViewTable() { }
        internal File3dmViewTable(bool namedViews)
        {
            NamedViews = namedViews;
        }

        public int Find(string name)
        {
            int cnt = Count;
            for (int i = 0; i < cnt; i++)
            {
                if (this[i].Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                    return i;
            }
            return -1;
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool NamedViews { get; set; }

    }
}
