using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class torch_flicker : MonoBehaviour {

    public float flickerProbability = 22f;
    public float minIntensity = 12.5f;
    public float maxIntensity = 13.5f;
    public float minRange = 3.12f;
    public float maxRange = 3.16f;
    public float transitionTime = 0.05f;

    private bool intensityChanging;
    private bool rangeChanging;
    private new Light light;

    void Start () {
        light = this.GetComponent<Light>();
        // If the attached isn't a light, this is all moot so we'll just get rid of this script.
        if (light == null) Destroy(this);

        light.intensity = (minIntensity + maxIntensity) / 2;
        light.range = (minRange + maxRange) / 2;
    }
	
	void Update () {
        float likelihood = Random.Range(1, 100);
        if (likelihood < flickerProbability && !(intensityChanging || rangeChanging))
        {
            StartCoroutine(IntensityShift(Random.Range(minIntensity, maxIntensity)));
            StartCoroutine(RangeShift(Random.Range(minRange, maxRange)));
            intensityChanging = true;
            rangeChanging = true;
        }
    }

    private IEnumerator IntensityShift(float newIntensity)
    {
        float oldValue = light.intensity;
        for (float t = 0; t < transitionTime; t += Time.deltaTime)
        {
            light.intensity = Mathf.Lerp(oldValue, newIntensity, t / transitionTime);
            yield return new WaitForEndOfFrame();
        }
        intensityChanging = false;
    }

    private IEnumerator RangeShift(float newRange)
    {
        float oldValue = light.range;
        for (float t = 0; t < transitionTime; t += Time.deltaTime)
        {
            light.range = Mathf.Lerp(oldValue, newRange, t / transitionTime);
            yield return new WaitForEndOfFrame();
        }
        rangeChanging = false;
    }
}
