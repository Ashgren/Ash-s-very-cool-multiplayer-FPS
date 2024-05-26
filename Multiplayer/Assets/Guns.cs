using UnityEngine;

public class Guns : MonoBehaviour
{
    public GameObject ak47;
    public GameObject m4a1;
    public GameObject AWP;
    public GameObject UZI;

    private int previousGun = 1; // Assume AK47 is initially selected

    void Start()
    {
        UpdateGunModel();
    }

    void Update()
    {
        // Check if guns variable has changed
        if (GunSelector.guns != previousGun)
        {
            UpdateGunModel();
            previousGun = GunSelector.guns;
        }
    }

    void UpdateGunModel()
    {
        // Enable the model of the selected gun and disable others
        switch (GunSelector.guns)
        {
            case 1:
                ak47.SetActive(true);
                m4a1.SetActive(false);
                AWP.SetActive(false);
                UZI.SetActive(false);
                break;
            case 2:
                ak47.SetActive(false);
                m4a1.SetActive(true);
                AWP.SetActive(false);
                UZI.SetActive(false);
                break;
            case 3:
                ak47.SetActive(false);
                m4a1.SetActive(false);
                AWP.SetActive(true);
                UZI.SetActive(false);
                break;
            case 4:
                ak47.SetActive(false);
                m4a1.SetActive(false);
                AWP.SetActive(false);
                UZI.SetActive(true);
                break;
            default:
                // Disable all models if the selected gun is unknown
                ak47.SetActive(false);
                m4a1.SetActive(false);
                AWP.SetActive(false);
                UZI.SetActive(false);
                break;
        }
    }
}
