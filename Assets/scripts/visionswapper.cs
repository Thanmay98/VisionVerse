using UnityEngine;
using UnityEngine.Rendering;

public class VisionSwitcher : MonoBehaviour
{
    public Volume globalVolume;
    public VolumeProfile normalVisionProfile;
    public VolumeProfile myopiaProfile;
    public VolumeProfile hyperopiaProfile;
    public VolumeProfile presbyopiaProfile;
    public VolumeProfile colorBlindGrayscaleProfile;
    public VolumeProfile colorBlindRedGreenProfile;

    public void SetNormalVision()
    {
        globalVolume.profile = normalVisionProfile;
    }

    public void SetMyopia()
    {
        globalVolume.profile = myopiaProfile;
    }

    public void SetHyperopia()
    {
        globalVolume.profile = hyperopiaProfile;
    }
    public void SetPresbyopia()
    {
        globalVolume.profile = presbyopiaProfile;
    }

    public void SetColorBlindGrayscale()
    {
        globalVolume.profile = colorBlindGrayscaleProfile;
    }

    public void SetColorBlindRedGreen()
    {
        globalVolume.profile = colorBlindRedGreenProfile;
    }
}