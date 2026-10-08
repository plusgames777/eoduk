namespace Eoduk.Gaze
{
    public interface IGazeTarget
    {
        void OnGaze(float deltaTime);
        void OnGazeLost(float deltaTime);
    }
}
