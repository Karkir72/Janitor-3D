using UnityEngine;

public class Player : MonoBehaviour { }

public static class AnimatorPlayer
{
    public static class States
    {
        public const string Idle = nameof(Idle);
        public const string RunForward = nameof(RunForward);
        public const string RunBack = nameof(RunBack);
        public const string Jump = nameof(Jump);
    }
}
public static class Preferences
{
    public const string Volume = nameof(Volume);
    public const string LastLevel = nameof(LastLevel);
}