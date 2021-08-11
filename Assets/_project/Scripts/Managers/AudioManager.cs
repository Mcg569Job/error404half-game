using UnityEngine;

public enum AudioType { Error, Finding, Fixing, Fixed,Fire,NoMagazine,EnterRepaiArea,ExitRepairArea,Collect }

public class AudioManager : MonoBehaviour
{
    #region Singleton
    public static AudioManager Instance = null;
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    #endregion

    [SerializeField] private AudioSource source;

    [SerializeField] private AudioClip Error;
    [SerializeField] private AudioClip Finding;
    [SerializeField] private AudioClip Fixing;
    [SerializeField] private AudioClip Fixed;
    [SerializeField] private AudioClip Fire;
    [SerializeField] private AudioClip NoMagazine;
    [SerializeField] private AudioClip EnterRepairArea;
    [SerializeField] private AudioClip ExitRepairArea;
    [SerializeField] private AudioClip Collect;


    public void PlaySound(AudioType audioType, bool loop = false)
    {
        // if (PlayerPrefs.GetInt("sound") != 0) return;

        AudioClip clip = null;
        switch (audioType)
        {
            case AudioType.Error: clip = Error; break;
            case AudioType.Finding: clip = Finding; break;
            case AudioType.Fixing: clip = Fixing; break;
            case AudioType.Fixed: clip = Fixed; break;
            case AudioType.Fire: clip = Fire; break;
            case AudioType.NoMagazine: clip = NoMagazine; break;
            case AudioType.EnterRepaiArea: clip = EnterRepairArea; break;
            case AudioType.ExitRepairArea: clip = ExitRepairArea; break;
            case AudioType.Collect: clip = Collect; break;
        }

        if (!loop)
        {

            if (source.clip != null)
            {
                source.clip = null;
                source.loop = false;
            }

            if (clip != null)
                source.PlayOneShot(clip);
        }
        else
        {
            if (clip != null)
                source.clip = clip;

            source.loop = true;
            source.Play();
        }



    }
}
