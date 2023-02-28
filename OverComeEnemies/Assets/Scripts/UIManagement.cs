using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagement : MonoBehaviour
{
    public Image healtFill;
    public Image ammoFill;
    private Attack playerAmmo;
    private Target playerHealt;
    public void Awake()
    {
        playerAmmo = GameObject.FindGameObjectWithTag("Player").GetComponent<Attack>();
        playerHealt = playerAmmo.GetComponent<Target>();
    }
    void Update()
    {
        UpdateHealtFill();
        UpdateAmmoFill();
    }
    public void UpdateAmmoFill()
    {
        ammoFill.fillAmount =(float) playerAmmo.GetAmmo / playerAmmo.MaxAmmoCount;
    }
    public void UpdateHealtFill()
    {
        healtFill.fillAmount =(float) playerHealt.GetHealt / playerHealt.MaxHealt;
    }
}
