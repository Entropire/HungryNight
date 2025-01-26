using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeoDeposit : MonoBehaviour
{
    int Health = 4;
    [SerializeField] SpriteRenderer ChildSprite;
    [SerializeField] Sprite BrokenRockSprite;
    [SerializeField] List<GameObject> DepositList;
    [SerializeField] GameObject[] Food;
    [SerializeField] AudioSource HitSound;
    private AudioSource Deposit;
    private bool StartUpdate;

    private void Start()
    {
        Deposit = GetComponent<AudioSource>();

        ChildSprite = gameObject.GetComponentInChildren<SpriteRenderer>();

        AttackCollider.OnPlayerAttacked += GoRock;
    }


    void GoRock(GameObject GameObj)
    {
        if (GameObj.layer == LayerMask.NameToLayer("Geo"))
        {
            Health--;
            for (int i = 0; i < 3; i++)
            {
                DepositList.Add(Instantiate(Food[Random.Range(0, 3)], transform.position, Quaternion.identity));
            }

            Deposit.Play();
            HitSound.Play();

            if (Health == 0)
            {
                ChildSprite.sprite = BrokenRockSprite;
                ChildSprite.transform.position -= new Vector3(0, .5f, 0);
                for (int i = 0; i < 11; i++)
                {
                    DepositList.Add(Instantiate(Food[Random.Range(0, 3)], transform.position, Quaternion.identity));
                }
                Destroy(this);
                AttackCollider.OnPlayerAttacked -= GoRock;
            }
        }

    }

}




