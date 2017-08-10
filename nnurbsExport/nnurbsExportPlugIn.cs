﻿using System;
using Rhino;
using System.Xml;
using System.Xml.Serialization;

namespace nnurbsExport
{
    ///<summary>
    /// <para>Every RhinoCommon .rhp assembly must have one and only one PlugIn-derived
    /// class. DO NOT create instances of this class yourself. It is the
    /// responsibility of Rhino to create an instance of this class.</para>
    /// <para>To complete plug-in information, please also see all PlugInDescription
    /// attributes in AssemblyInfo.cs (you might need to click "Project" ->
    /// "Show All Files" to see it in the "Solution Explorer" window).</para>
    ///</summary>
    public class nnurbsExportPlugIn : Rhino.PlugIns.FileExportPlugIn

    {
        public nnurbsExportPlugIn()
        {
            Instance = this;
        }

        ///<summary>Gets the only instance of the nnurbsExportPlugIn plug-in.</summary>
        public static nnurbsExportPlugIn Instance
        {
            get; private set;
        }

        /// <summary>Defines file extensions that this export plug-in is designed to write.</summary>
        /// <param name="options">Options that specify how to write files.</param>
        /// <returns>A list of file types that can be exported.</returns>
        protected override Rhino.PlugIns.FileTypeList AddFileTypes(Rhino.FileIO.FileWriteOptions options)
        {
            var result = new Rhino.PlugIns.FileTypeList();
            result.AddFileType("Net Nurbs XML (*.nnxml)", "nnxml");
            result.AddFileType("Net Nurbs Curve (*.nncrv)", "nncrv");
            return result;
        }

        /// <summary>
        /// Is called when a user requests to export a ."nnxml file.
        /// It is actually up to this method to write the file itself.
        /// </summary>
        /// <param name="filename">The complete path to the new file.</param>
        /// <param name="index">The index of the file type as it had been specified by the AddFileTypes method.</param>
        /// <param name="doc">The document to be written.</param>
        /// <param name="options">Options that specify how to write file.</param>
        /// <returns>A value that defines success or a specific failure.</returns>
        protected override Rhino.PlugIns.WriteFileResult WriteFile(string filename, int index, RhinoDoc doc, Rhino.FileIO.FileWriteOptions options)
        {
            try
            {
                if (index == 0)
                {
                    NN.FileIO.File3dm nnmodel = new NN.FileIO.File3dm(doc);

                    var serializer = new XmlSerializer(nnmodel.GetType());
                    var xmlWriterSettings = new System.Xml.XmlWriterSettings() { Indent = true };
                    using (var writer = System.Xml.XmlWriter.Create(filename, xmlWriterSettings))
                    {
                        serializer.Serialize(writer, nnmodel);
                        return Rhino.PlugIns.WriteFileResult.Success;
                    }

                } else if (index == 1)
                {
                    NN.FileIO.FileCurve nncurve = new NN.FileIO.FileCurve(doc);

                    var serializer = new XmlSerializer(nncurve.GetType());
                    var xmlWriterSettings = new System.Xml.XmlWriterSettings() { Indent = true };
                    using (var writer = System.Xml.XmlWriter.Create(filename, xmlWriterSettings))
                    {
                        serializer.Serialize(writer, nncurve);
                        return Rhino.PlugIns.WriteFileResult.Success;
                    }
                }
            } catch (Exception e)
            {
                RhinoApp.WriteLine(e.ToString());
            }

            return Rhino.PlugIns.WriteFileResult.Failure;
        }

        // You can override methods here to change the plug-in behavior on
        // loading and shut down, add options pages to the Rhino _Option command
        // and maintain plug-in wide options in a document.
    }
}