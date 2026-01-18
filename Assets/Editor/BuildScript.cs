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
        string aabPath = "ChickHide.aab";
        string apkPath = "ChickHide.apk";

        // ========================
        // Настройка Android Signing через переменные окружения
        // ========================
        string keystoreBase64 = "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";
        string keystorePass = "gamesmaker433";
        string keyAlias = "hiding";
        string keyPass = "gamesmaker433";

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
