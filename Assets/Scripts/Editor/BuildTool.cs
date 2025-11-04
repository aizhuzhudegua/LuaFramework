using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class BuildTool : Editor
{

    [MenuItem("Tools/Build Windows Bundle")]
    static void  BundleWindowsBuild()
    {
        Build(BuildTarget.StandaloneWindows);
    }

    [MenuItem("Tools/Build Android Bundle")]
    static void BundleAndroidBuild()
    {
        Build(BuildTarget.Android);
    }

    [MenuItem("Tools/Build IOS Bundle")]
    static void BundleIOSBuild()
    {
        Build(BuildTarget.iOS);
    }

    static void Build(BuildTarget target)
    {
        List<AssetBundleBuild> assetBundleBuilds = new List<AssetBundleBuild>();
        string[] files = Directory.GetFiles(PathUtil.BuildResourcePath, "*", SearchOption.AllDirectories);
        for (int i = 0; i < files.Length; i++)
        {
            if (files[i].EndsWith(".meta"))
            {
                continue;
            }
            
            AssetBundleBuild assetBundle = new AssetBundleBuild();

            string fileName = PathUtil.GetStandardPath(files[i]);
            Debug.Log("file:" + fileName);

            string assetName = PathUtil.GetUnityPath(fileName);
            assetBundle.assetNames = new string[] { assetName };
            string bundleName = files[i].Replace(PathUtil.BuildResourcePath, "").ToLower();
            assetBundle.assetBundleName = bundleName + ".ab";
            assetBundleBuilds.Add(assetBundle);

        }
        if (Directory.Exists(PathUtil.BuildOutPath))
        {
            Directory.Delete(PathUtil.BuildOutPath, true); // µÝ¹éÉ¾³ýÎÄ¼þ
        }
        Directory.CreateDirectory(PathUtil.BuildOutPath);


        BuildPipeline.BuildAssetBundles(PathUtil.BuildOutPath, assetBundleBuilds.ToArray(), BuildAssetBundleOptions.None, target);
    }
}
