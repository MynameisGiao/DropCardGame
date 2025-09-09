using UnityEditor;

public class BuildScript
{
    public static void BuildAndroid()
    {
        string pathToDeploy = "Build/Android/CitadelDefense.apk"; // Đường dẫn lưu APK
        BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, pathToDeploy, BuildTarget.Android, BuildOptions.None);
    }
}
