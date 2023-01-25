using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class Gun : MonoBehaviour
{
    private GunAnimations animations;
    private ObjectControls controls;
    private ObjectSounds sounds;
    private ObjectParticles particles;

    public Transform bulletSpawn;

    bool hasShot = false;

    public bool isSimulatorGun = false;

    private void Awake()
    {
        animations = GetComponent<GunAnimations>();
        controls = GetComponent<ObjectControls>();
        sounds = GetComponent<ObjectSounds>();
        particles = GetComponent<ObjectParticles>();
    }

    private void Update()
    {
        if (!controls.objectHeld)
            return;

        if(controls.GetTriggerValue() > 0.5f && !hasShot)
        {
            Shoot();
            hasShot = true;
        }
        else if (controls.GetTriggerValue() <= 0.5 && hasShot)
        {
            hasShot = false;
        }

    }

    private void Shoot()
    {
        sounds.PlaySound();
        particles.PlayParticle();
        controls.SetHapticOfDevice(1f, 0.2f);
        animations.BarrelAnimation();

        RaycastHit hit;

        if (Physics.Raycast(transform.position, bulletSpawn.forward, out hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag("Bot") && hit.collider.gameObject.GetComponent<BotBehaviour>().interuptFinished)
            {
                hit.collider.gameObject.GetComponent<BotBehaviour>().hasBeenShot = true;
            }
        }
    }

    public void GunPickedUp(SelectEnterEventArgs args)
    {
        if(isSimulatorGun)
        GameObject.Find("GameMode").GetComponent<WaveManager>().PlayerReady();
    }
}
