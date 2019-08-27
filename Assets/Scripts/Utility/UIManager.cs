using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Singleton
    public static UIManager Instance = null;
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public GameObject healthBarPrefab;
    public Transform healthBarParent;

    public static Slider CreateHealthBarUI()
    {
        GameObject healthBarUI = Instantiate(Instance.healthBarPrefab, 
                                             Instance.healthBarParent);
        return healthBarUI.GetComponent<Slider>();
    }
}
