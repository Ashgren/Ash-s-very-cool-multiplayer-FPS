using UnityEngine;
using UnityEngine.UI;

public class GunSelector : MonoBehaviour
{
    public Text gunText;
    public static int guns = 1;
    void Start()
    {
        UpdateGunText();
    }

    void Update()
    {
        // Check for left button press
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            DecreaseGun();
        }
        // Check for right button press
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            IncreaseGun();
        }
    }

    public void DecreaseGun()
    {
        guns--;
        if (guns < 1)
            guns = 4; // Cycle to the last gun when reaching the beginning
        UpdateGunText();
    }

    public void IncreaseGun()
    {
        guns++;
        if (guns > 4)
            guns = 1; // Cycle to the first gun when reaching the end
        UpdateGunText();
    }

    void UpdateGunText()
    {
        switch (guns)
        {
            case 1:
                gunText.text = "AK47";
                break;
            case 2:
                gunText.text = "M4A1";
                break;
            case 3:
                gunText.text = "AWP";
                break;
            case 4:
                gunText.text = "UZI";
                break;
            default:
                gunText.text = "Unknown Gun";
                break;
        }
    }
}
