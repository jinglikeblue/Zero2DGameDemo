using System;
using Zero;

namespace Sokoban
{
    class ILRuntimeGenerics : BaseILRuntimeGenerics
    {
        public override void Register(ILRuntime.Runtime.Enviorment.AppDomain appdomain)
        {
            appdomain.DelegateManager.RegisterMethodDelegate<System.String>();            appdomain.DelegateManager.RegisterMethodDelegate<System.Int32, System.Object>();            appdomain.DelegateManager.RegisterMethodDelegate<UnityEngine.Vector2>();            appdomain.DelegateManager.RegisterDelegateConvertor<DG.Tweening.TweenCallback>((act) =>
            {
                return new DG.Tweening.TweenCallback(() =>
                {
                    ((Action)act)();
                });
            });

        }
    }
}