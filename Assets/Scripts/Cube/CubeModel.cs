using UnityEngine;

namespace RaccoonsGames.Cube
{
    public class CubeModel
    {
        public int Value { get; private set; }

        public void SetValue(int newValue)
        {
            Value = newValue;
        }

        public Color GetColorForValue()
        {
            switch (Value)
            {
                case 2:
                    return Color.red;
                case 4:
                    return new Color(1f, 0.647f, 0f);
                case 8:
                    return new Color(1f, 0.647f, 0f);
                case 16:
                    return Color.yellow;
                case 32:
                    return new Color(0.0f, 1.0f, 0.0f);
                case 64:
                    return new Color(0.0f, 0.0f, 1.0f);
                case 128:
                    return new Color(0.5f, 0.0f, 0.5f);
                case 256:
                    return new Color(0.75f, 0.75f, 0.75f);
                case 512:
                    return Color.cyan;
                case 1024:
                    return new Color(0.0f, 0.5f, 0.5f);
                case 2048:
                    return new Color(1f, 0.5f, 0.0f);
                default:
                    return Color.gray;
            }
        }
    }
}