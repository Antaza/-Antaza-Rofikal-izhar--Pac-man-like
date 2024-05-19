using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Action OnpowerupStart;
    public Action OnpowerupStop;

    [SerializeField]
    float _speed;
    [SerializeField]
    Camera _camera;
    [SerializeField]
    float _powerupDuration;
    [SerializeField]
    int Health;
    [SerializeField]
    TMP_Text _healthText;
    [SerializeField]
    Transform _respawnPoint;

    Rigidbody _rigidbody;
    Coroutine _powerupCoroutine;
    public bool _isPowerupActive = false;
    
    public void Dead()
    {
        Health -= 1;
        UpdateUI();
        if (Health > 0)
        {
            transform.position = _respawnPoint.position;
        }
        else
        {
            Health = 0;
            UpdateUI();
            Debug.Log("YOU LOSE");
            SceneManager.LoadScene("LOSE Scene");
        }
    }
    public void PickPowerUp()
    {
        if (_powerupCoroutine != null)
        {
            StopCoroutine(_powerupCoroutine);
        }
        
        _powerupCoroutine = StartCoroutine(StartPowerUp());
    }

    IEnumerator StartPowerUp()
    {
        _isPowerupActive = true;
        if (OnpowerupStart != null)
        {
            OnpowerupStart();
            Debug.Log("Start Power Up");
        }
        
        yield return new WaitForSeconds(_powerupDuration);

        _isPowerupActive = false;
        if (OnpowerupStop != null)
        {
            OnpowerupStop();
            Debug.Log("Stop Power Up");
        }
        
    }
    void Awake()
    {
        UpdateUI();
        _rigidbody = GetComponent<Rigidbody>();
        HideAndLockCursor();
    }

    void HideAndLockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    void Update()
    {
        
        // Horizontal = a or left (-) / d or right (+)
        float horizontal = Input.GetAxis("Horizontal");
        // Vertical = s or down (-) / w or up (+)
        float vertical = Input.GetAxis("Vertical");

        Vector3 horizontalDirection = horizontal * _camera.transform.right;
        Vector3 verticalDirection = vertical * _camera.transform.forward;
        verticalDirection.y = 0;
        horizontalDirection.y = 0;

        Vector3 movementDirection = horizontalDirection + verticalDirection;

        _rigidbody.velocity = movementDirection * _speed * Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (_isPowerupActive)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                collision.gameObject.GetComponent<Enemy>().Dead();
            }
        }
    }

    void UpdateUI()
    {
        _healthText.text = "Health : " + Health;
    }
}
