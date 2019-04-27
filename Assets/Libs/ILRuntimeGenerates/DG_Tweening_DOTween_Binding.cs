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
    unsafe class DG_Tweening_DOTween_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            MethodBase method;
            FieldInfo field;
            Type[] args;
            Type type = typeof(DG.Tweening.DOTween);
            args = new Type[]{};
            method = type.GetMethod("Sequence", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Sequence_0);
            args = new Type[]{typeof(System.Nullable<System.Boolean>), typeof(System.Nullable<System.Boolean>), typeof(System.Nullable<DG.Tweening.LogBehaviour>)};
            method = type.GetMethod("Init", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Init_1);

            field = type.GetField("defaultEaseType", flag);
            app.RegisterCLRFieldGetter(field, get_defaultEaseType_0);
            app.RegisterCLRFieldSetter(field, set_defaultEaseType_0);


        }


        static StackObject* Sequence_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* __ret = ILIntepreter.Minus(__esp, 0);


            var result_of_this_method = DG.Tweening.DOTween.Sequence();

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* Init_1(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 3);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Nullable<DG.Tweening.LogBehaviour> @logBehaviour = (System.Nullable<DG.Tweening.LogBehaviour>)typeof(System.Nullable<DG.Tweening.LogBehaviour>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.Nullable<System.Boolean> @useSafeMode = (System.Nullable<System.Boolean>)typeof(System.Nullable<System.Boolean>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 3);
            System.Nullable<System.Boolean> @recycleAllByDefault = (System.Nullable<System.Boolean>)typeof(System.Nullable<System.Boolean>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);


            var result_of_this_method = DG.Tweening.DOTween.Init(@recycleAllByDefault, @useSafeMode, @logBehaviour);

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }


        static object get_defaultEaseType_0(ref object o)
        {
            return DG.Tweening.DOTween.defaultEaseType;
        }
        static void set_defaultEaseType_0(ref object o, object v)
        {
            DG.Tweening.DOTween.defaultEaseType = (DG.Tweening.Ease)v;
        }


    }
}
