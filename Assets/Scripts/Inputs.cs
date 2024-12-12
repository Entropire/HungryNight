using UnityEngine;

namespace HungryNight.Player
{
    [CreateAssetMenu(fileName = "Inputs", menuName = "ScriptableObjects/Inputs")]
    public class Inputs : ScriptableObject
    {
        public KeyCode[] up;
        public KeyCode[] down;
        public KeyCode[] left;
        public KeyCode[] right;
        public KeyCode[] jump;
        public KeyCode[] attack;
    }
}