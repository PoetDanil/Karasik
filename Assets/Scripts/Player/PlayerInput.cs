using System;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class PlayerInput : Sounds
{
    public static Action OnJump;

    public float MoveInput { get; private set; }
    public float RotationInput { get; private set; }
    
    [SerializeField] private Text coinsText;

    [SerializeField] private Joystick _joystick;
    
    [SerializeField] private ParticleSystem _particle;
    
    public static float coins;
    
    private void Update()
    {
        coinsText.text = coins.ToString();
        
        if (YandexGame.EnvironmentData.isMobile)
        {
            if (_joystick.Vertical > 0)
            {
                MoveInput = 1;

            }
            else if (_joystick.Vertical < 0)
            {
                MoveInput = -1;
            }
            else
            {
                MoveInput = 0;
            }
            RotationInput = _joystick.Horizontal;
           

        }
        else
        {
            MoveInput = Input.GetAxis("Vertical");
            RotationInput = Input.GetAxis("Horizontal");


        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    public void Jump() // ������
    {
        OnJump?.Invoke();
        PlaySound(sounds[0], 0.7f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin")) // �������
        {
            coins++;
            PlaySound(sounds[1], volume : 0.32f);
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("wall")) {
            _particle.Play();
            PlaySound(sounds[2], volume: 0.6f);
        }
    }
}
