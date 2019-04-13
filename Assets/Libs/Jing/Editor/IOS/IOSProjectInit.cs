using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;

namespace Jing.Editor.iOS
{
    /// <summary>
    /// XCODE项目的初始化
    /// </summary>
    public class IOSProjectInit
    {
        /// <summary>
        /// XCODE项目发布后的处理
        /// </summary>
        /// <param name="target"></param>
        /// <param name="path"></param>
        [PostProcessBuild]
        public static void OnPostProcessBuild(BuildTarget target, string path)
        {
            if (BuildTarget.iOS != target)
            {
                return;
            }

            IOSProjectInitData data = IOSProjectInitData.Load();

            string projPath = PBXProject.GetPBXProjectPath(path);
            PBXProject pbx = new PBXProject();
            pbx.ReadFromString(File.ReadAllText(projPath));
            string guid = pbx.TargetGuidByName("Unity-iPhone");

            pbx.AddFrameworkToProject(guid, "CoreTelephony.framework", false);
            pbx.AddFrameworkToProject(guid, "StoreKit.framework", false);
            pbx.AddFrameworkToProject(guid, "AdSupport.framework", false);
            pbx.AddFrameworkToProject(guid, "WebKit.framework", false);
            pbx.AddFrameworkToProject(guid, "MessageUI.framework", false);
            pbx.AddFrameworkToProject(guid, "GLKit.framework", false);
            pbx.AddFrameworkToProject(guid, "MobileCoreService.framework", false);

            pbx.AddFileToBuild(guid, pbx.AddFile("usr/lib/libz.tbd", "Frameworks/libz.tbd", PBXSourceTree.Sdk));
            pbx.AddFileToBuild(guid, pbx.AddFile("usr/lib/libc++.tbd", "Frameworks/libc++.tbd", PBXSourceTree.Sdk));
            pbx.AddFileToBuild(guid, pbx.AddFile("usr/lib/libicucore.tbd", "Frameworks/libicucore.tbd", PBXSourceTree.Sdk));
            pbx.AddFileToBuild(guid, pbx.AddFile("usr/lib/libsqlite3.0.tbd", "Frameworks/libsqlite3.0.tbd", PBXSourceTree.Sdk));


            pbx.SetBuildProperty(guid, "ENABLE_BITCODE", "NO");
            pbx.AddBuildProperty(guid, "OTHER_LDFLAGS", "-force_load $(PROJECT_DIR)/Libraries/Plugins/iOS/WeChat/libWeChatSDK.a");
            pbx.AddBuildProperty(guid, "OTHER_LDFLAGS", "-ObjC");
            //$(PROJECT_DIR) / Libraries / Plugins / iOS / WeChat / libWeChatSDK.a

            //有米SDK需要内容


            File.WriteAllText(projPath, pbx.WriteToString());

            ////修改PList
            string plistPath = path + "/Info.plist";
            InfoPListEditor pListEditor = new InfoPListEditor(plistPath);
            foreach (string urlScheme in data.urlSchemes)
            {
                pListEditor.AddUrlScheme("yfy", urlScheme);
            }
            foreach (string whiteUrlScheme in data.whiteSchemeList)
            {
                pListEditor.AddLSApplicationQueriesScheme(whiteUrlScheme);
            }
            pListEditor.Save();
        }
    }
}
