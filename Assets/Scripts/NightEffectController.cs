using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class NightEffectController : MonoBehaviour
{
    [SerializeField] private PostProcessVolume ppVolume;
    [SerializeField] private Transform endPoint;
    [SerializeField] private Transform player;
    Vector3 startPoint;

    private ColorGrading colorGrading;
    void Start()
    {
        ppVolume.profile.TryGetSettings<ColorGrading>(out colorGrading);
        startPoint = player.position;
        colorGrading.ldrLutContribution.value = 0f;

    }

    void Update()
    {
        if (endPoint == null || player == null) return;

        var progress = Mathf.InverseLerp(endPoint.position.x, startPoint.x, player.position.x);

        colorGrading.ldrLutContribution.value = progress;
    }
}