using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FTGOverlay
{
    public class Score : MonoBehaviour
    {
        public TMPro.TMP_Text Text;
        private int _value;
        public int Value
        {
            get { return _value; }
            set
            {
                _value = value;
                Text.text = _value.ToString();
            }
        }

        public void Awake()
        {
            Text.text = _value.ToString();
        }

        public void OnAddScore()
        {
            Value++;
        }

        public void ResetScore()
        {
            Value = 0;
        }
    }
}