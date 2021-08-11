using UnityEngine;

public class Bullet : MonoBehaviour
{
    private bool _move = false;
    private int _damage;

    public void StartMove(int damage)
    {
        _damage = damage;
        _move = true;
        Invoke("DestroyMe", 5);
    }

    private void Update()
    {
        if (!_move) return;
        transform.position += transform.forward * Time.deltaTime * 10;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Enemy")
        {
            Healt h = other.transform.GetComponentInChildren<Healt>();
            h.GetDamage(_damage);
        }
        DestroyMe();
    }

    private void DestroyMe() => Destroy(gameObject);


}
