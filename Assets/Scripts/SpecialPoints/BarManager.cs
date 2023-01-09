using System.Collections;
using UnityEngine;
using Slider = UnityEngine.UI.Slider;

public class BarManager : MonoBehaviour
{
    Vector2 originalPosition;
    RectTransform rect;
    [SerializeField] private int shakeIntensity;

    private float newSpecialPoints;
    private float currentSpecialPoints;

    [SerializeField] private int numberOfIterations = 100;
    [SerializeField] private float UpdateSpeed = 1.0f;

    private Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
        rect = GetComponent<RectTransform>();

        float startingValue = SpecialPointsManager.Instance.GetSpecialPoints();
        newSpecialPoints = startingValue;
        currentSpecialPoints = startingValue;
        slider.value = startingValue;

        originalPosition = rect.anchoredPosition;
    }

    // Update is called once per frame
    void Update()
    {
        newSpecialPoints = SpecialPointsManager.Instance.GetSpecialPoints();
        if (newSpecialPoints != currentSpecialPoints)
        {
            StartCoroutine(UpdateBar());
            currentSpecialPoints = newSpecialPoints;
        }
    }

    private IEnumerator UpdateBar()
    {
        float iterationLength = UpdateSpeed / numberOfIterations;
        float delta = (newSpecialPoints - currentSpecialPoints);
        float iterationDelta = (2 * delta) / (numberOfIterations * (numberOfIterations + 1));

        for (int i = numberOfIterations; i > 0; i--)
        {
            float currentIterationDelta = i * iterationDelta;

            slider.value += currentIterationDelta;
            BarShake(currentIterationDelta, i);
            yield return new WaitForSeconds(iterationLength);
            rect.anchoredPosition = originalPosition;
        }
    }

    private void BarShake(float delta, int i)
    {
        float rand = Random.Range(-1, 1);
        float Shake = Mathf.Sin(i + rand);
        rect.anchoredPosition = new Vector2(originalPosition.x + Shake * (delta * i) * shakeIntensity, originalPosition.y + rand);
    }
}
