using Crockhead.Core;
using UnityEngine;
using Crockhead.Scripting;
using Crockhead.Unity.UI;


/// <summary>
/// 게임 진입점.
/// </summary>
public static class RuntimeInitializer
{
	/// <summary>
	/// 시작.
	/// </summary>
	[RuntimeInitializeOnLoadMethod]
	public static void Run()
	{
		Debug.Log("[RuntimeInitializer] StartAsync()");
		SharedInstances.Clear();

		//// 스크립트 테스트.
		//static Variable Print(Variable[] parameters)
		//{
		//	if (parameters.Length == 0)
		//		return Variable.Null();

		//	var message = parameters[0].ToString();
		//	Debug.Log($"[Crockhead.Scripting] {message}");
		//	return Variable.Null();
		//}

		//var textAsset = Resources.Load<TextAsset>("script");
		//var scriptEngine = new ScriptEngine();
		//scriptEngine.Load(textAsset.text);
		//scriptEngine.AddFunction("Print", Print);
		//scriptEngine.Execute("doSomething");

		// UI 생성.
		var application = UIHelper.CreateApplication();
		var scene = UIHelper.CreateScene();
		application.ConnectScene(scene);

		var window = UIHelper.CreateWindow(new Vector2Int(1920, 1080));
		scene.AddWindow(window);
		window.RootViewController = new MainViewController();
		window.MakeKeyAndVisible();
	}

	public class MainViewController : UIViewController
	{
		protected override void OnViewDidLoad()
		{
			base.OnViewDidLoad();
		}
	}
}