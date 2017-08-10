using System;
using System.Collections.Generic;
using Rhino;
using Rhino.Commands;
using Rhino.Input;
using Rhino.Input.Custom;
using System.Xml.Serialization;
using System.IO;

namespace nnurbsExport
{
    public class nnurbsExportCommand : Command
    {
        public nnurbsExportCommand()
        {
            // Rhino only creates one instance of each command class defined in a
            // plug-in, so it is safe to store a refence in a static property.
            Instance = this;
        }

        ///<summary>The only instance of this command.</summary>
        public static nnurbsExportCommand Instance
        {
            get; private set;
        }

        ///<returns>The command name as it appears on the Rhino command line.</returns>
        public override string EnglishName
        {
            get { return "nnurbsExportCommand"; }
        }

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            const Rhino.DocObjects.ObjectType geometryFilter = Rhino.DocObjects.ObjectType.AnyObject;
         
            bool json = false;

            OptionToggle optionToggle = new OptionToggle(json, new Rhino.UI.LocalizeStringPair("XML", "XML"), new Rhino.UI.LocalizeStringPair("JSON", "JSON"));
   

            GetObject go = new GetObject();
            go.SetCommandPrompt("Select object for information");
            go.GeometryFilter = geometryFilter;
            int output_idx = go.AddOptionToggle("Output", ref optionToggle);
            go.GroupSelect = true;
            go.SubObjectSelect = false;
            go.EnableClearObjectsOnEntry(false);
            go.EnableUnselectObjectsOnExit(false);
            go.DeselectAllBeforePostSelect = false;

            for (;;)
            {
                GetResult res = go.GetMultiple(1, 0);

                if (res == GetResult.Option)
                {
                    go.EnablePreSelect(false, true);
                    continue;
                }

                else if (res != GetResult.Object)
                    return go.CommandResult();

                break;
            }

            

            NN.FileIO.File3dm nnmodel = new NN.FileIO.File3dm(doc, true);

            string message = "";

            if (optionToggle.CurrentValue)
            {
                using (StringWriter textWriter = new StringWriter())
                {
                    Newtonsoft.Json.JsonSerializer serializerJSON = new Newtonsoft.Json.JsonSerializer();
                    serializerJSON.Serialize(textWriter, nnmodel);
                    message = textWriter.ToString();
                    dynamic parsedJson = Newtonsoft.Json.JsonConvert.DeserializeObject(message);
                    message = Newtonsoft.Json.JsonConvert.SerializeObject(parsedJson, Newtonsoft.Json.Formatting.Indented);
                }

                Rhino.UI.Dialogs.ShowTextDialog(message, "JSON Information");
            }
            else
            {
                // XML
                XmlSerializer xmlSerializer = new XmlSerializer(nnmodel.GetType());

                using (StringWriter textWriter = new StringWriter())
                {
                    xmlSerializer.Serialize(textWriter, nnmodel);
                    message = textWriter.ToString();
                }

                Rhino.UI.Dialogs.ShowTextDialog(message, "XML Information");
            }
            
            return Result.Success;
        }
    }
}
