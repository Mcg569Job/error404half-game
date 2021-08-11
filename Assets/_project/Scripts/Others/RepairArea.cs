using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairArea : MonoBehaviour
{

    private bool _activate = false;
    private Healt _healt;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            AudioManager.Instance.PlaySound(AudioType.Fixed);
            _healt = other.GetComponentInChildren<Healt>();
            Invoke("Enter", 1f);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Exit();
        }
    }
    private void Update()
    {
        if (_activate)
            _healt.GetDamage((-10 * Time.deltaTime));
    }


    private void Enter()
    {
        ErrorManager.Instance.FixErrors();
        _activate = true;
        AudioManager.Instance.PlaySound(AudioType.EnterRepaiArea);
    }
    private void Exit()
    {
        AudioManager.Instance.PlaySound(AudioType.ExitRepairArea);
        ErrorManager.Instance.CreateNewError();
        _activate = false;
    }
}
