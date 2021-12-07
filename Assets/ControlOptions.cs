using Tzaik.General;
using Tzaik.Player;
using UnityEngine;
using UnityEngine.UI;
 
namespace Tzaik
{
public class ControlOptions : MonoBehaviour
{
        #region Fields
		[SerializeField] Slider sensitivityslider;
        #endregion

        #region Properties
        #endregion

        #region Unity Methods 
		void Start()
		{
			GameManager.Instance.Player.GetComponent<PlayerController>().MouseLook.SetSensitivity(PlayerPrefs.GetFloat("MouseSensitivity", 20));
			sensitivityslider.value = PlayerPrefs.GetFloat("MouseSensitivity", 20);
		} 
        #endregion

        #region Class Methods	
        public void SetSensitivity(float x) 
		{
			PlayerPrefs.SetFloat("MouseSensitivity", x);
			GameManager.Instance.Player.GetComponent<PlayerController>().MouseLook.SetSensitivity(x);
		} 
        #endregion

        #region Coroutines
        #endregion
    }
}
