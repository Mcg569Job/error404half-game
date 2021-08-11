using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FXType { Dead }
public class FXManager : MonoBehaviour
{
    public static FXManager Instance = null;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    [SerializeField] private Pool[] _effects;

    private void Start()
    {
        for (int i = 0; i < _effects.Length; i++)
            _effects[i].CreatePool(transform);
    }

    public void SpawnFX(FXType type, Vector3 position)
    {
        GameObject fx = _effects[(int)type].GetObject();
        fx.transform.position = position;
        StartCoroutine(HideFx(fx));
    }

    private IEnumerator HideFx(GameObject fx)
    {
        yield return new WaitForSeconds(1.5f);
        fx.SetActive(false);
    }
}
