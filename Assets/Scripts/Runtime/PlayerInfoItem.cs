using UnityEngine;
using UnityEngine.UI;

namespace ShrimpFPS.Runtime
{
    public class PlayerInfoItem : MonoBehaviour
    {
        [SerializeField] private Text playerName;
        public void SetUp(string name)
        {
            playerName.text = name;
        }
    }
}