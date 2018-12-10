using UnityEngine;

public class WindManager : MonoBehaviour
{
    public struct WindInfo
    {
        public Vector2 Direction;
        public float Strength;
    }

    public WindInfo Wind
    {
        get;
        private set;
    }

    private void Awake()
    {
        Wind = new WindInfo
        {
            Direction = Vector2.up,
            Strength = 1
        };
    }
}
