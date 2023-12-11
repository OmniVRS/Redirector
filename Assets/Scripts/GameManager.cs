using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Image shieldImage;
    public Image capacitorImage;
    private float reflectCooldown;
    private float absorbCooldown;

    // Start is called before the first frame update
    void Start()
    {
        reflectCooldown = PlayerControl.shieldCooldown;
        absorbCooldown = PlayerControl.capacitorCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void shieldCooldown()
    {
        StartCoroutine(shieldCooldownCoroutine());
    }
    
    public void capacitorCooldown()
    {
        StartCoroutine(capacitorCooldownCoroutine());
    }

    IEnumerator shieldCooldownCoroutine()
    {
        shieldImage.fillAmount = 0;
        while (shieldImage.fillAmount < 1)
        {
            shieldImage.fillAmount += 1 / reflectCooldown * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator capacitorCooldownCoroutine()
    {
        capacitorImage.fillAmount = 0;
        while (capacitorImage.fillAmount < 1)
        {
            capacitorImage.fillAmount += 1 / absorbCooldown * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
