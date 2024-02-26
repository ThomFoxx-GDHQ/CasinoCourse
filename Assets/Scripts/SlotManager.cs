using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class SlotManager : MonoBehaviour
{

    [SerializeField] private PlayableDirector _director;
    [SerializeField] private ReelStrip[] _reelStrips;
    [SerializeField] private GameObject[] _reelParents;
    [SerializeField] private ReelStop _reelStops;
    [SerializeField] private float _winLoadDelay;
    private WaitForSeconds _loadDelay;

    // Start is called before the first frame update
    void Start()
    {
        if (_director == null)
            _director = GetComponent<PlayableDirector>();

        if (_reelStrips == null)
            Debug.LogError("Reels not Loaded");
        else
            for (int i = 0; i < _reelParents.Length; i++)
                LoadReel(_reelParents[i], _reelStrips[i]);

        if (_reelStops == null)
            Debug.LogError("ReelStops not Loaded");

        _loadDelay = new WaitForSeconds(_winLoadDelay);
    }

    void LoadReel(GameObject reel, ReelStrip strip)
    {
        Image[] reelA = reel.transform.GetChild(0).GetComponentsInChildren<Image>();
        Image[] reelB = reel.transform.GetChild(1).GetComponentsInChildren<Image>();
        Image[] reelC = reel.transform.GetChild(2).GetComponentsInChildren<Image>();

        for (int i = 0; i < reelA.Length; i++)
        {
            reelA[i].sprite = strip.images[i];
            reelB[i].sprite = strip.images[i];
        }
        for (int i = 0;i < reelC.Length; i++)
        {
            reelC[i].sprite = strip.images[i];
        }

    }

    IEnumerator LoadWinDelayed(GameObject reel, int reelPosition, int stopSet)
    {
        int stop = 0;

        switch (reelPosition)
        {
            case 1:
                stop = _reelStops.positions[stopSet].R1; 
                break;
            case 2:
                stop = _reelStops.positions[stopSet].R2;
                break;
            case 3:
                stop = _reelStops.positions[stopSet].R3;
                break;
            case 4:
                stop = _reelStops.positions[stopSet].R4;
                break;
            case 5:
                stop = _reelStops.positions[stopSet].R5;
                break;
        }
                
        yield return _loadDelay;

        Image[] reelC = reel.transform.GetChild(2).GetComponentsInChildren<Image>();

        for (int i = -1; i < 4; i++)
        {
            int stopImage = stop + i;

            if (stopImage >= _reelStrips[reelPosition - 1].images.Length)
                stopImage -= _reelStrips[reelPosition - 1].images.Length;
            if (stopImage < 0)
                stopImage += _reelStrips[reelPosition - 1].images.Length;

            reelC[i + 1].sprite = _reelStrips[reelPosition-1].images[stopImage];
        }        
    }


    [ContextMenu("Random Spin")]
    public void RandomSpin()
    {
        _director.Play();

        int randomStop = Random.Range(0, _reelStops.positions.Length);

        for (int i = 0; i < 5; i++)
        {
            StartCoroutine(LoadWinDelayed(_reelParents[i], i+1, randomStop));
        }
    }

    public void SelectedSpin(int StopSet)
    {
        _director.Play();

        for (int i=0; i<5; i++) 
        {
            StartCoroutine(LoadWinDelayed(_reelParents[i], i+1, StopSet));
        }
    }
}
