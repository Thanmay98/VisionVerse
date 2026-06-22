using UnityEngine;
using UnityEngine.Rendering;

public class VisionSwitcher : MonoBehaviour
{
    public Volume globalVolume;
    public VolumeProfile normalVisionProfile;
    public VolumeProfile myopiaProfile;
    public VolumeProfile hyperopiaProfile;

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
}