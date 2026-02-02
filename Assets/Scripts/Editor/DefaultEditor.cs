using UnityEditor;
using UnityEditor.SceneManagement;


namespace MillenniumOfCultivation.Editor
{
	public static class DefaultEditor
	{
		[MenuItem("Project/Open MainScene")]
		public static void OpenMainScene()
		{
			var scenePath = $"Assets/Scenes/{RuntimeInitializer.MainRuntimeSceneName}.unity";
			EditorSceneManager.OpenScene(scenePath);
			//AssetDatabase.SaveAssetIfDirty()
			AssetDatabase.SaveAssets();
		}
	}
}