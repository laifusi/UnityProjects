using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballLauncher : MonoBehaviour
{
    [SerializeField] Fireball fireballPrefab;
    [SerializeField] float fireRate = 0.5f;
    
    string fireButton;
    string horizontalAxis;
    float nextFireTime;

    void Start()
    {
        int playerNumber = GetComponent<Player>().PlayerNumber;
        fireButton = $"P{playerNumber}Fire";
        horizontalAxis = $"P{playerNumber}Horizontal";
    }

    void Update()
    {
        if (Input.GetButtonDown(fireButton) && Time.time >= nextFireTime)
        {
            var horizontal = Input.GetAxis(horizontalAxis);
            Fireball fireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity);
            fireball.Direction = horizontal >= 0 ? 1 : -1;
            nextFireTime = Time.time + fireRate;
        }
    }
}
