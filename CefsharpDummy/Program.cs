// Copyright © 2010-2015 The CefSharp Authors. All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found in the LICENSE file.

using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Windows.Forms;

namespace CefsharpDummy
{
    public static class Program
    {
        [STAThread]
        public static int Main(string[] args)
        {


            string path = Assembly.GetExecutingAssembly().Location;
            string strDllPath = Path.Combine(new FileInfo(path).Directory.FullName, "CefsharpRunner.dll");

            if (File.Exists(strDllPath))
            {
                // Execute the method from the requested .dll using reflection (System.Reflection).
                Assembly DLL = Assembly.LoadFrom(strDllPath);
                Type classType = DLL.GetType(String.Format("{0}.{1}", "CefsharpRunner", "Program"));
                if (classType != null)
                {
                    // Create class instance.
                    object classInst = Activator.CreateInstance(classType);

                    // Invoke required method.
                    MethodInfo methodInfo = classType.GetMethod("Launch");
                    if (methodInfo != null)
                    {
                        object result = null;
                        result = methodInfo.Invoke(classInst, new object[] { args });
                    }
                }
            }

            return 0;
        }
    }
}
