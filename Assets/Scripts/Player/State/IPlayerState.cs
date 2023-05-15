public interface IPlayerState
{
    public void Start() { }
    public void End() { }
    public void Update() { }
    public void OnAnimatorMove() { }
    public void LookAt(UnityEngine.Vector3 pos) { }
}
