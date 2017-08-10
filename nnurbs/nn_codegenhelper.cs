using System;
using System.Reflection;

namespace nnurbs
{
    class nn_codegenhelper
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
    }
}
