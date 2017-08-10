﻿using System;
using Rhino;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace nnurbsImport
{
    ///<summary>
    /// <para>Every RhinoCommon .rhp assembly must have one and only one PlugIn-derived
    /// class. DO NOT create instances of this class yourself. It is the
    /// responsibility of Rhino to create an instance of this class.</para>
    /// <para>To complete plug-in information, please also see all PlugInDescription
    /// attributes in AssemblyInfo.cs (you might need to click "Project" ->
    /// "Show All Files" to see it in the "Solution Explorer" window).</para>
    ///</summary>
    public class nnurbsImportPlugIn : Rhino.PlugIns.FileImportPlugIn

    {
        public nnurbsImportPlugIn()
        {
            Instance = this;
        }

        ///<summary>Gets the only instance of the nnurbsImportPlugIn plug-in.</summary>
        public static nnurbsImportPlugIn Instance
        {
            get; private set;
        }

        ///<summary>Defines file extensions that this import plug-in is designed to read.</summary>
        /// <param name="options">Options that specify how to read files.</param>
        /// <returns>A list of file types that can be imported.</returns>
        protected override Rhino.PlugIns.FileTypeList AddFileTypes(Rhino.FileIO.FileReadOptions options)
        {
            var result = new Rhino.PlugIns.FileTypeList();
            result.AddFileType("Net Nurbs XML (*.nnxml)", "nnxml");
            result.AddFileType("Net Nurbs Curve (*.nncrv)", "nncrv");
            return result;
        }

        /// <summary>
        /// Is called when a user requests to import a ."nnxml file.
        /// It is actually up to this method to read the file itself.
        /// </summary>
        /// <param name="filename">The complete path to the new file.</param>
        /// <param name="index">The index of the file type as it had been specified by the AddFileTypes method.</param>
        /// <param name="doc">The document to be written.</param>
        /// <param name="options">Options that specify how to write file.</param>
        /// <returns>A value that defines success or a specific failure.</returns>
        protected override bool ReadFile(string filename, int index, RhinoDoc doc, Rhino.FileIO.FileReadOptions options)
        {
            bool read_success = false;

            if (index == 0)
            {
                NN.FileIO.File3dm demodel;
                // Construct an instance of the XmlSerializer with the type  
                // of object that is being deserialized.  
                XmlSerializer xmlSerializer =
                new XmlSerializer(typeof(NN.FileIO.File3dm));
                // To read the file, create a FileStream.  
                using (FileStream fileStream = new FileStream(filename, FileMode.Open))
                {
                    // Call the Deserialize method and cast to the object type.  
                    demodel = (NN.FileIO.File3dm)xmlSerializer.Deserialize(fileStream);
                }

                Rhino.FileIO.File3dm fromXML = new Rhino.FileIO.File3dm();

                if (demodel != null)
                {
                    read_success = demodel.AddTo(doc);
                }
            } else if (index == 1)
            {
                NN.FileIO.FileCurve demodel;

                XmlSerializer xmlSerializer =
                new XmlSerializer(typeof(NN.FileIO.FileCurve));
                // To read the file, create a FileStream.  
                using (FileStream fileStream = new FileStream(filename, FileMode.Open))
                {
                    // Call the Deserialize method and cast to the object type.  
                    demodel = (NN.FileIO.FileCurve)xmlSerializer.Deserialize(fileStream);
                }

                Rhino.FileIO.File3dm fromXML = new Rhino.FileIO.File3dm();

                if (demodel != null)
                {
                    read_success = demodel.AddTo(doc);
                }
            }

            return read_success;
        }

        // You can override methods here to change the plug-in behavior on
        // loading and shut down, add options pages to the Rhino _Option command
        // and maintain plug-in wide options in a document.
    }
}