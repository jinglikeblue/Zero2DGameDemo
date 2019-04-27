using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

using ILRuntime.CLR.TypeSystem;
using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using ILRuntime.Runtime.Stack;
using ILRuntime.Reflection;
using ILRuntime.CLR.Utils;

namespace ILRuntime.Runtime.Generated
{
    unsafe class GameKit_Joystick_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            FieldInfo field;
            Type[] args;
            Type type = typeof(GameKit.Joystick);

            field = type.GetField("camera", flag);
            app.RegisterCLRFieldGetter(field, get_camera_0);
            app.RegisterCLRFieldSetter(field, set_camera_0);


        }



        static object get_camera_0(ref object o)
        {
            return ((GameKit.Joystick)o).camera;
        }
        static void set_camera_0(ref object o, object v)
        {
            ((GameKit.Joystick)o).camera = (UnityEngine.Camera)v;
        }


    }
}
