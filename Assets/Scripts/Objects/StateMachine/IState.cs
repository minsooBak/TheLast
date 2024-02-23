
public interface IState
{
    public void Enter();
    public void Exit();
    public void HandleInput(); //State중 입력처리
    public void Update();
    public void PhysicsUpdate();

}