using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
using System;
using System.IO;

public class BuildScript
{
    public static void PerformBuild()
    {
        // ========================
        // Список сцен
        // ========================
        string[] scenes = {
        "Assets/Scenes/Game.unity",
        };

        // ========================
        // Пути к файлам сборки
        // ========================
        string aabPath = "EggDrop.aab";
        string apkPath = "EggDrop.apk";

        // ========================
        // Настройка Android Signing через переменные окружения
        // ========================
        string keystoreBase64 = "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";
        string keystorePass = "rtyu111";
        string keyAlias = "droper";
        string keyPass = "rtyu111";

        string tempKeystorePath = null;

        if (!string.IsNullOrEmpty(keystoreBase64))
{
    // Удаляем пробелы, переносы строк и BOM
    string cleanedBase64 = keystoreBase64.Trim()
                                         .Replace("\r", "")
                                         .Replace("\n", "")
                                         .Trim('\uFEFF');

    // Создаем временный файл keystore
    tempKeystorePath = Path.Combine(Path.GetTempPath(), "TempKeystore.jks");
    File.WriteAllBytes(tempKeystorePath, Convert.FromBase64String(cleanedBase64));

    PlayerSettings.Android.useCustomKeystore = true;
    PlayerSettings.Android.keystoreName = tempKeystorePath;
    PlayerSettings.Android.keystorePass = keystorePass;
    PlayerSettings.Android.keyaliasName = keyAlias;
    PlayerSettings.Android.keyaliasPass = keyPass;

    Debug.Log("Android signing configured from Base64 keystore.");
}
        else
        {
            Debug.LogWarning("Keystore Base64 not set. APK/AAB will be unsigned.");
        }

        // ========================
        // Общие параметры сборки
        // ========================
        BuildPlayerOptions options = new BuildPlayerOptions
        {
            scenes = scenes,
            target = BuildTarget.Android,
            options = BuildOptions.None
        };

        // ========================
        // 1. Сборка AAB
        // ========================
        EditorUserBuildSettings.buildAppBundle = true;
        options.locationPathName = aabPath;

        Debug.Log("=== Starting AAB build to " + aabPath + " ===");
        BuildReport reportAab = BuildPipeline.BuildPlayer(options);
        if (reportAab.summary.result == BuildResult.Succeeded)
            Debug.Log("AAB build succeeded! File: " + aabPath);
        else
            Debug.LogError("AAB build failed!");

        // ========================
        // 2. Сборка APK
        // ========================
        EditorUserBuildSettings.buildAppBundle = false;
        options.locationPathName = apkPath;

        Debug.Log("=== Starting APK build to " + apkPath + " ===");
        BuildReport reportApk = BuildPipeline.BuildPlayer(options);
        if (reportApk.summary.result == BuildResult.Succeeded)
            Debug.Log("APK build succeeded! File: " + apkPath);
        else
            Debug.LogError("APK build failed!");

        Debug.Log("=== Build script finished ===");

        // ========================
        // Удаление временного keystore
        // ========================
        if (!string.IsNullOrEmpty(tempKeystorePath) && File.Exists(tempKeystorePath))
        {
            File.Delete(tempKeystorePath);
            Debug.Log("Temporary keystore deleted.");
        }
    }
}
