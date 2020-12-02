using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    // Common Parts
    public float distance = 1.5f;

    [TextArea]
    public string descriptionOff;
    [TextArea]
    public string descriptionOn;

    private GameObject player;
    private TMPro.TextMeshProUGUI textArea;

    private bool active = false;
    private bool aiming = false;

    // Microphone Parts
    public float sensitivity = 100;
    public float soundDistance = 10;
    public float soundPlayInterval = 3f;
    private float loudness = 0;
    private AudioSource _audio;
    protected bool hasRecorded = false;

    public bool isRepeatable = false;

    public enum InteractionType
    {
        Microphone,
        Animation
    }

    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Transform canvas = GameObject.Find("Canvas").transform;

        GameObject textField = canvas.Find("InteractTextArea").gameObject;
        GameObject duplicate = Instantiate(textField, canvas);

        textArea = duplicate.GetComponent<TMPro.TextMeshProUGUI>();

        CanInteract();

        if (this.interactionType == InteractionType.Microphone)
        {
            if (Microphone.devices.Length <= 0)
            {
                Debug.LogWarning("No Mic");
            }
            else
            {
                _audio = gameObject.AddComponent<AudioSource>();
                _audio.Stop();
                //_audio.clip = Microphone.Start(null, true, 10, 44100);
                _audio.minDistance = soundDistance;
                _audio.maxDistance = soundDistance * 1.5f;
                _audio.spatialBlend = 1f;
                _audio.rolloffMode = AudioRolloffMode.Linear;
                _audio.loop = false;
                //_audio.mute = true;
                //while (!(Microphone.GetPosition(null) > 0)) { }
                //_audio.Play();
            }
        }
    }

    protected virtual void Update()
    {
        if (aiming == false)
        {
            CanInteract();
        }

        if (hasRecorded && _audio)
        {
            if (Time.time % soundPlayInterval > (soundPlayInterval - 0.1f))
                _audio.Play();
        }
    }

    public InteractionType interactionType;

    public string GetDescription() {
        return (!hasRecorded) ? descriptionOff : descriptionOn;
    }
    public abstract void Interact();
    public void CanInteract() {
        float dist = Vector3.Distance(transform.position, player.transform.position);

        active = !(dist > distance);

        if (active)
        {
            if (!hasRecorded || (hasRecorded && isRepeatable))
            {
                KeyCode key = KeyCode.E;

                if (this.interactionType == InteractionType.Animation)
                {
                    textArea.text = this.GetDescription();

                    if (Input.GetKeyDown(key))
                    {
                        this.Interact();
                        hasRecorded = true;
                    }
                }
                else
                {
                    PlayerController playerController = player.GetComponent<PlayerController>();

                    if (playerController.hasAmmo())
                    {
                        SoundRecord(key);
                    }
                    else
                    {
                        textArea.text = "Plus de munitions";
                    }
                }
            } else
            {
                textArea.text = "";
            }
        }
        else
        {
            textArea.text = "";
        }
    }

    public void aimingStatus(bool rayHit = false)
    {
        aiming = rayHit;

        if (aiming)
        {
            if (!hasRecorded || (hasRecorded && isRepeatable))
            {
                PlayerController playerController = player.GetComponent<PlayerController>();

                if (playerController.hasAmmo())
                {
                    KeyCode key = KeyCode.E;

                    SoundRecord(key);
                }
                else
                {
                    textArea.text = "Plus de munitions";
                }
            } else
            {
                textArea.text = "";
            }
        }
        else
        {
            textArea.text = "";
        }
    }

    // SoundRecord : Only Mic Part
    private void SoundRecord(KeyCode key)
    {
        loudness = GetAveragedVolume() * sensitivity;
        PlayerController playerController = player.GetComponent<PlayerController>();

        if (!Microphone.IsRecording(null))
        {
            textArea.text = this.GetDescription();

            if (Input.GetKeyDown(key))
            {
                playerController.Shoot();
                Debug.LogWarning("RECORD");
                textArea.text = "RECORD";
                _audio.clip = Microphone.Start(null, true, 20, 44100);
            }
        }
        else
        {
            if (Input.GetKeyDown(key))
            {
                Debug.LogWarning("STOP RECORD && PLAY RECORDED SOUND");
                Microphone.End(null);
                hasRecorded = true;
                playerController.setRecording(false);
                playerController.removeAmmo();
                this.Interact();
            }
            else
            {
                Debug.LogWarning("RECORDING");
                playerController.setRecording(true);
                textArea.text = "RECORDING";
            }
        }
    }

    // GetAveragedVolume : Only Mic Part
    float GetAveragedVolume()
    {
        float[] data = new float[256];
        float a = 0;
        _audio.GetOutputData(data, 0);

        foreach (float s in data)
        {
            a += Mathf.Abs(s);
        }

        return a / 256;
    }
}
