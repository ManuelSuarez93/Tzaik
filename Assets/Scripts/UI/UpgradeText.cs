using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tzaik
{
    public class UpgradeText : MonoBehaviour
    {
        [SerializeField] TextMesh text; 

        public string UpgradeText1 { get; set; }
        public string UpgradeText2 { get; set; }
        public string SpecialUpgradeText { get; set; }



        public void SetText1() => text.text = UpgradeText1;
        public void SetText2() => text.text = UpgradeText2; 
        public void SetTextSpecial() => text.text = SpecialUpgradeText;
        public void SetTextEmpty() => text.text = "";
    }
}
