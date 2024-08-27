namespace Script
{
    public interface IState
    {
        void OnEnter(StateController controller);

        void UpdateState(StateController controller);

        void FixedUpdateState(StateController controller);
        void OnHurt(StateController controller);

        void OnExit(StateController controller);
    }
}