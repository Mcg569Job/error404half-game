using UnityEngine;

public enum ItemType{Bullet,RepairBox}
public class Item : MonoBehaviour
{
    public ItemType type;
    public int amount;

    public void CollectMe()
    {
        AudioManager.Instance.PlaySound(AudioType.Collect);
       gameObject.SetActive(false);
    }
}
