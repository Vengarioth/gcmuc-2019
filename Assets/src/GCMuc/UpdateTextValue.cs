using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace GCMuc
{
    [RequireComponent(typeof(TMP_Text))]
    public class UpdateTextValue : MonoBehaviour
    {
        private TMP_Text _text;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }

        public void SetTextValue(float value)
        {
            var intValue = Mathf.RoundToInt(value);
            _text.text = intValue.ToString();
        }
    }
}
