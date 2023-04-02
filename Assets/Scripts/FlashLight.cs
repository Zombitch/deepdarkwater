using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashLight : MonoBehaviour
{

    public Image flashLight;
    public float lightFadeDuration = 0.02f;
    public float lightScreenDuration = 0.01f;
    
    public IEnumerator handleFlashLight()
    {
        //BLINK IN (fade from transparent to opaque)
        float alpha = 0;
        for (float i = 0; i <= lightFadeDuration; i += Time.deltaTime)
        {
            alpha = (i - 0) / (lightFadeDuration - 0); //Normalize value between 0 and 1
 
            // set color with i as alpha
            this.flashLight.color = new Color(this.flashLight.color.r, this.flashLight.color.g, this.flashLight.color.b, alpha * 0.3f);
            //print(m_BlinkImage.color);
            yield return null;
        }
 
        yield return new WaitForSeconds(lightScreenDuration);
 
        //BLINK OUT (fade from opaque to transparent)
        for (float i = lightFadeDuration; i >= 0; i -= Time.deltaTime)
        {
            alpha = (i - 0) / (lightFadeDuration - 0); //Normalize value between 0 and 1
 
            // set color with i as alpha
            this.flashLight.color = new Color(this.flashLight.color.r, this.flashLight.color.g, this.flashLight.color.b, alpha);
            yield return null;
        }
        this.flashLight.color = new Color(this.flashLight.color.r, this.flashLight.color.g, this.flashLight.color.b, 0); // fix residual values
        
    }
}
