using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LifeBase : MonoBehaviour
{
    [SerializeField]    private TextMeshProUGUI _lifeText;

    public void SetLife(int life)
    {
        _lifeText.text = "LIFE:" + life;
    }


}
