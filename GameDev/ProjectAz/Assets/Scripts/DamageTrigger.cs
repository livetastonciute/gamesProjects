using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UI))]
public class DamageTrigger : MonoBehaviour
{

	[SerializeField]
    private string dmgTag = "Dmg";
	[SerializeField, Min(0)]
    private int dmgValue = 10;
    [SerializeField]
    private int force = 200;

    [SerializeField]
    private string dmgPendulumTag = "DmgPendulum";
    [SerializeField, Min(0)]
    private int dmgPendulumValue = 15;
    [SerializeField]
    private int Pendulumforce = 50;

    [SerializeField]
    private string dmgSpinnerTag = "DmgSpinner";
    [SerializeField, Min(0)]
    private int dmgSpinnerValue = 5;
    [SerializeField]
    private int Spinnerforce = 90;

    [SerializeField]
    private string dmgSpinBlockTag = "DmgSpinBlock";
    [SerializeField, Min(0)]
    private int dmgSpinBlockValue = 30;
    [SerializeField]
    private int SpinBlockforce = 200;

    [SerializeField]
    private string dmgLogTag = "DmgLog";
    [SerializeField, Min(0)]
    private int dmgLogValue = 100;
    [SerializeField]
    private int SpinLogforce = 500;

    private UI t;

    public AudioSource[] sounds;
    public AudioSource hitSound;

    private void Awake()
    {
        sounds = GetComponents<AudioSource>();
        hitSound = sounds[1];
        t = GetComponent<UI>();
    }

    private bool IsDmg(GameObject obj)
    {
        return obj.CompareTag(dmgTag);
    }
    private bool IsDmgPendulum(GameObject obj)
    {
        return obj.CompareTag(dmgPendulumTag);
    }
    private bool IsDmgSpinner(GameObject obj)
    {
        return obj.CompareTag(dmgSpinnerTag);
    }
    private bool IsDmgSpinBlock(GameObject obj)
    {
        return obj.CompareTag(dmgSpinBlockTag);
    }

    private bool IsDmgLog(GameObject obj)
    {
        return obj.CompareTag(dmgLogTag);
    }

    private void OnTriggerEnter(Collider other)
    {
        var otherGameObject = other.gameObject;

        if (IsDmg(otherGameObject)) {
            hitSound.Play();
            t.RemoveHp(dmgValue);
            var dir = this.gameObject.transform.position - otherGameObject.transform.position;
            ImpactReceiver.AddImpact(dir, force);
        }

        if (IsDmgPendulum(otherGameObject))
        {
            hitSound.Play();
            t.RemoveHp(dmgPendulumValue);
            var dir = this.gameObject.transform.position - otherGameObject.transform.position;
            ImpactReceiver.AddImpact(dir, Pendulumforce);
        }

        if (IsDmgSpinner(otherGameObject))
        {
            hitSound.Play();
            t.RemoveHp(dmgSpinnerValue);
            var dir = this.gameObject.transform.position - otherGameObject.transform.position;
            ImpactReceiver.AddImpact(dir, Spinnerforce);
        }

        if (IsDmgSpinBlock(otherGameObject))
        {
            hitSound.Play();
            t.RemoveHp(dmgSpinBlockValue);
            var dir = this.gameObject.transform.position - otherGameObject.transform.position;
            ImpactReceiver.AddImpact(dir, SpinBlockforce);
        }
        if (IsDmgLog(otherGameObject))
        {
            t.RemoveHp(dmgLogValue);
            var dir = this.gameObject.transform.position - otherGameObject.transform.position;
            ImpactReceiver.AddImpact(dir, SpinLogforce);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //hitAudio = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
