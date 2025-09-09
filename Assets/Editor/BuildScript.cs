using UnityEditor;

public class BuildScript
{
    [MenuItem("Build/Build Android APK")]
    static void BuildGame()
    {
        string[] scenes = {
            "Assets/Scenes/Boot.unity",
            "Assets/Scenes/Buffer.unity",
            "Assets/Scenes/Ingame_01.unity",
            "Assets/Scenes/Ingame_02.unity",
            "Assets/Scenes/Ingame_03.unity",
            "Assets/Scenes/Ingame_04.unity",
        };  // Đường dẫn tới các scene cần build
        string path = "Builds/Android/CitadelDefense.apk";  // Đường dẫn lưu file APK sau khi build
        BuildPipeline.BuildPlayer(scenes, path, BuildTarget.Android, BuildOptions.None);
    }
}
