using Crockhead.Core;


/// <summary>
/// 식별 될 수 있는 객체.
/// </summary>
public class Identifiable : Disposable
{
	/// <summary>
	/// 인스턴스 고유 식별자.
	/// </summary>
	public int InstanceId { get; }

	/// <summary>
	/// 생성됨.
	/// </summary>
	public Identifiable(int instanceId) : base()
	{
		InstanceId = instanceId;
	}

	/// <summary>
	/// 해제됨.
	/// </summary>
	protected override void OnDispose(bool explicitDisposing)
	{
	}
}