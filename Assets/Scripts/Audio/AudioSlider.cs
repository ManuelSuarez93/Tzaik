using UnityEngine;
using UnityEngine.UI;

namespace Tzaik.Audio
{
    [RequireComponent(typeof(Slider))]
    public class AudioSlider : MonoBehaviour
    {
        #region Fields
        Slider slider;
        [SerializeField] AudioType type;
        #endregion

        #region Properties
        public Slider Slider { get => slider;  set => slider = value; }
        public AudioType Type { get => type; }
        #endregion

        #region Methods
        public void Initialize(float value)
        {
            slider = GetComponent<Slider>();
            slider.value = value * slider.maxValue;
        }
        #endregion

    }
}
