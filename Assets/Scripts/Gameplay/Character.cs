﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Character : MonoBehaviour {

    public string Name;
    public Animator anim;

    [Header("Attack Colliders")]
    public Transform AttColl;
    protected BoxCollider2D UpAttColl;
    protected BoxCollider2D BottomAttColl;
    protected BoxCollider2D UpHardAttColl;
    protected BoxCollider2D BottomHardAttColl;

    [Header("Recieve Colliders")]
    public Transform RecvColl;
    protected BoxCollider2D UpRecvColl;
    protected BoxCollider2D BottomRecvColl;

    [Header("Blocking Colliders")]
    public Transform BlockColl;
    protected BoxCollider2D UpBlockColl;
    protected BoxCollider2D BottomBlockColl;

    [Header("Health")]
    public float CurrentHealth;
    public float MaxHealth;

    [Header("Stamina")]
    public float CurrentStamina;
    public float MaxStamina;
    public float CurrentMaxStamina;
    public float RecuperationPerSecond;

    [Header("Recuperation Times")]
    public float TiredRecuperationTime;
    public float PFailedRecuperationTime;


    #region AttReceptors
    protected ReceptorScript AttBottom;
    protected ReceptorScript AttUp;
    protected ReceptorScript HardAttBottom;
    protected ReceptorScript HardAttUp;
    #endregion
    public bool PunchEnded;

    #region Recievers
    protected RecieverScript RecvBottom;
    protected RecieverScript RecvUp;
    protected RecieverScript BlockBottom;
    protected RecieverScript BlockUp;
    #endregion
    public bool PunchRecieved;

    bool pause = false;

    public bool Punch;
    public PunchInfo info { get; set; }

    public float SD2Dodge = 15;

    [Header("States and Times")]
    public bool IsTired;
    public bool PunchFailed;
    public bool IsDefeated = true;

    [Header("Punch Infos")]
    public PunchInfo PIBottom;
    public PunchInfo PIUp;
    public PunchInfo PIHardBottom;
    public PunchInfo PIHardUp;

    public Stateinfos stateinfo;

    [Header("KnockOuts Manager")]
    public int MaxKnockouts;
    public int CurrentKnockouts = 0;
    public bool KnockedOut;

    [Header("Audio")]
    public AudioManager audioManager;

    [Header("Camera Shake")]
    public CameraManager cam;
    float Duration = 0.06f;
    float Magnitude = 0.1f;

    public void BlockedSound() {
        audioManager.PlaySound("Blocked");
    }

    public void PunchedSound() {
        audioManager.PlaySound("PunchHit");
    }

    #region FunctionsForAnimations

    protected Vector2 up = Vector2.up;
    protected Vector2 bottom = Vector2.down;
    protected Vector2 left = Vector2.left;
    protected Vector2 right = Vector2.right;
    

    public void BeginPunch()
    {
        //info = AuxInfo;
        AttBottom.Info = AttUp.Info = HardAttBottom.Info = HardAttUp.Info = info;
        StaminaDown(info.Stamina);
    }

    public void BeginDodge()
    {
        StaminaDown(SD2Dodge);
    }

    protected virtual bool Conditions2Fail() {
        return !PunchEnded;
    }

    public virtual void CheckFailPunch() {
        if (Conditions2Fail()) {
            FailingProcess();
        }
        PunchEnded = false;
    }

    protected virtual void FailingProcess() {
        anim.SetBool("PunchFailed", true);
        PunchFailed = true;
        print("i failed punch");
    }
    #endregion

    #region StateinfoClass
    [System.Serializable]
    public class Stateinfos {

        public AnimatorStateInfo StateInfo { get; private set; }


        [Header("Idle-Block")]
        public bool BlockIdle;
        public bool BlockedUp;
        public bool BlockedBottom;
        public bool Blocked;
        protected void GetBlocks() {
            BlockIdle = StateInfo.IsName("BlockV");
            BlockedUp = StateInfo.IsName("BlockedUp");
            BlockedBottom = StateInfo.IsName("BlockedBottom");
            Blocked = BlockedUp || BlockedBottom;
        }

        [Header("Dodge")]
        public bool DodgeLeft;
        public bool DodgeRight;
        public bool Dodging;
        protected void GetDodge() {
            DodgeLeft = StateInfo.IsName("DodgeLeft");
            DodgeRight = StateInfo.IsName("DodgeRight");
            Dodging = DodgeLeft || DodgeRight;
        }

        [Header("Normal Punch")]
        public bool PunchBottomLeft;
        public bool PunchBottomRight;
        public bool PunchUpLeft;
        public bool PunchUpRight;
        public bool Punching;
        protected void GetPunchs() {
            PunchBottomLeft = StateInfo.IsName("PunchBottomLeft");
            PunchBottomRight = StateInfo.IsName("PunchBottomRight");
            PunchUpLeft = StateInfo.IsName("PunchUpLeft");
            PunchUpRight = StateInfo.IsName("PunchUpRight");
            for (int i = 0; i < 10; i++)
            {
                PunchBottomLeft = PunchBottomLeft || StateInfo.IsName("PunchBottomLeft" + i);
                PunchBottomRight = PunchBottomRight || StateInfo.IsName("PunchBottomRight" + i);
                PunchUpLeft = PunchUpLeft || StateInfo.IsName("PunchUpLeft" + i);
                PunchUpRight = PunchUpRight|| StateInfo.IsName("PunchUpRight" + i);
            }
            

            Punching = PunchBottomLeft || PunchBottomRight || PunchUpLeft || PunchUpRight;
        }

        [Header("Hard Punch")]
        public bool HardPunchBottomLeft;
        public bool HardPunchBottomRight;
        public bool HardPunchUpLeft;
        public bool HardPunchUpRight;
        public bool HardPunching;
        protected void GetHardPunchs() {
            HardPunchBottomLeft = StateInfo.IsName("HardPunchBottomLeft");
            HardPunchBottomRight = StateInfo.IsName("HardPunchBottomRight");
            HardPunchUpLeft = StateInfo.IsName("HardPunchUpLeft");
            HardPunchUpRight = StateInfo.IsName("HardPunchUpRight");
            for (int i = 0; i < 10; i++)
            {
                HardPunchBottomLeft = HardPunchBottomLeft || StateInfo.IsName("HardPunchBottomLeft" + i);
                HardPunchBottomRight = HardPunchBottomRight || StateInfo.IsName("HardPunchBottomRight" + i);
                HardPunchUpLeft = HardPunchUpLeft || StateInfo.IsName("HardPunchUpLeft" + i);
                HardPunchUpRight = HardPunchUpRight || StateInfo.IsName("HardPunchUpRight" + i);
            }

            HardPunching = HardPunchBottomLeft || HardPunchBottomRight || HardPunchUpLeft || HardPunchUpRight;
        }

        [Header("Punch Failed")]
        public bool PunchFailedBottomLeft;
        public bool PunchFailedBottomRight;
        public bool PunchFailedUpLeft;
        public bool PunchFailedUpRight;
        public bool NormalPunchFailed;

        public bool PunchFailedHBottomLeft;
        public bool PunchFailedHBottomRight;
        public bool PunchFailedHUpLeft;
        public bool PunchFailedHUpRight;
        public bool HardpunchFailed;

        public bool PunchFailed;

        public bool FailingPunch;
        protected void GetPunchFailed() {
            PunchFailedBottomLeft = StateInfo.IsName("PunchFailedBottomLeft");
            PunchFailedBottomRight = StateInfo.IsName("PunchFailedBottomRight");
            PunchFailedUpLeft = StateInfo.IsName("PunchFailedUpLeft");
            PunchFailedUpRight = StateInfo.IsName("PunchFailedUpRight");
            NormalPunchFailed = PunchFailedBottomLeft || PunchFailedBottomRight || PunchFailedUpLeft || PunchFailedUpRight;

            PunchFailedHBottomLeft = StateInfo.IsName("PunchFailedHBottomLeft");
            PunchFailedHBottomRight = StateInfo.IsName("PunchFailedHBottomRight");
            PunchFailedHUpLeft = StateInfo.IsName("PunchFailedHUpLeft");
            PunchFailedHUpRight = StateInfo.IsName("PunchFailedHUpRight");
            HardpunchFailed = PunchFailedHBottomLeft || PunchFailedHBottomRight || PunchFailedHUpLeft || PunchFailedHUpRight;

            PunchFailed = StateInfo.IsName("PunchFailed");

            FailingPunch = NormalPunchFailed || HardpunchFailed || PunchFailed;
        }

        [Header("Recieve Punch")]
        public bool RecieveBottomLeft;
        public bool RecieveBottomRight;
        public bool RecieveUpLeft;
        public bool RecieveUpRight;
        public bool RecievePunch;
        protected void GetRecievePunch() {
            RecieveBottomLeft = StateInfo.IsName("RecieveBottomLeft");
            RecieveBottomRight = StateInfo.IsName("RecieveBottomRight");
            RecieveUpLeft = StateInfo.IsName("RecieveUpLeft");
            RecieveUpRight = StateInfo.IsName("RecieveUpRight");

            RecievePunch = RecieveBottomLeft || RecieveBottomRight || RecieveUpLeft || RecieveUpRight;
        }

        [Header("Recieve Hard Punch")]
        public bool RecieveHardBottomLeft;
        public bool RecieveHardBottomRight;
        public bool RecieveHardUpLeft;
        public bool RecieveHardUpRight;
        public bool RecieveHardPunch;
        protected void GetRecieveHardPunch() {
            RecieveHardBottomLeft = StateInfo.IsName("RecieveHardBottomLeft");
            RecieveHardBottomRight = StateInfo.IsName("RecieveHardBottomRight");
            RecieveHardUpLeft = StateInfo.IsName("RecieveHardUpLeft");
            RecieveHardUpRight = StateInfo.IsName("RecieveHardUpRight");

            RecieveHardPunch = RecieveHardBottomLeft || RecieveHardBottomRight || RecieveHardUpLeft || RecieveHardUpRight;
        }

        [Header("Tired")]
        public bool Tired;
        public bool Waiting;
        protected void GetTired() {
            Tired = StateInfo.IsName("Tired");
            Waiting = StateInfo.IsName("Waiting");
        }

        public virtual void GetStatesInfo(AnimatorStateInfo StateInf) {
            StateInfo = StateInf;

            GetBlocks();
            GetDodge();
            GetPunchs();
            GetHardPunchs();
            GetPunchFailed();
            GetRecievePunch();
            GetRecieveHardPunch();
            GetTired();
        }
    }

    #endregion


    public virtual void DoPunch() {
        Punch = true;
        if(!info.Hard)
            audioManager.PlaySound("NormalPunchAir");
        else
            audioManager.PlaySound("HardPunchAir");
    }

    public virtual void LoadData() {
        CurrentKnockouts = 0;
        anim = GetComponent<Animator>();
        audioManager = GetComponent<AudioManager>();

        //Attack Colliders
        UpAttColl = AttColl.GetChild(0).GetComponent<BoxCollider2D>();
        BottomAttColl = AttColl.GetChild(1).GetComponent<BoxCollider2D>();
        UpHardAttColl = AttColl.GetChild(2).GetComponent<BoxCollider2D>();
        BottomHardAttColl = AttColl.GetChild(3).GetComponent<BoxCollider2D>();

        //Recieve Colliders
        UpRecvColl = RecvColl.GetChild(0).GetComponent<BoxCollider2D>();
        BottomRecvColl = RecvColl.GetChild(1).GetComponent<BoxCollider2D>();
        
        //Blocking Colliders
        UpBlockColl = BlockColl.GetChild(0).GetComponent<BoxCollider2D>();
        BottomBlockColl = BlockColl.GetChild(1).GetComponent<BoxCollider2D>();

        //Receptors
        AttBottom = BottomAttColl.transform.GetComponent<ReceptorScript>();
        AttUp = UpAttColl.transform.GetComponent<ReceptorScript>();
        HardAttBottom = BottomHardAttColl.transform.GetComponent<ReceptorScript>();
        HardAttUp = UpHardAttColl.transform.GetComponent<ReceptorScript>();

        //Recievers
        RecvBottom = BottomRecvColl.transform.GetComponent<RecieverScript>();
        RecvUp = UpRecvColl.transform.GetComponent<RecieverScript>();
        BlockBottom = BottomBlockColl.transform.GetComponent<RecieverScript>();
        BlockUp = UpBlockColl.transform.GetComponent<RecieverScript>();

        //Bars
        CurrentHealth = MaxHealth;
        CurrentMaxStamina = MaxStamina;
        CurrentStamina = CurrentMaxStamina;
    }

    float timeStaminadown;
    [Header("STAMINA RECOVERING DEMO")]
    public float StaminaRecovering;
    public virtual void UpdateStamina() {
        bool CanIncStamina = Time.time - timeStaminadown > StaminaRecovering;
        if (CanIncStamina){

            CurrentStamina += RecuperationPerSecond * Time.deltaTime;

        }
    }

    public virtual void UpdateThis() {
        if (PauseManager.GameIsPaused)
            return;
        UpdateStamina();
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
        CurrentMaxStamina = Mathf.Clamp(CurrentMaxStamina, 0, MaxStamina);
        CurrentStamina = Mathf.Clamp(CurrentStamina, 0, CurrentMaxStamina);
    }

    public virtual void FixedUpdatethis() {
        if (PauseManager.GameIsPaused)
            return;
        stateinfo.GetStatesInfo(anim.GetCurrentAnimatorStateInfo(0));
        if (PunchFailed && (stateinfo.HardpunchFailed || stateinfo.NormalPunchFailed))
        {
            anim.SetBool("PunchFailed", false);
            PunchFailed = false;
        }
    }

    public virtual void Damaged(PunchInfo punchInfo) {
        CurrentHealth -= punchInfo.Damage;
        string Trigger = "Recieve";

        if (punchInfo.Hard)
            Trigger += "Hard";

        if (punchInfo.PunchRawLocal.y == -1)
            Trigger += "Bottom";
        else if (punchInfo.PunchRawLocal.y == 1)
            Trigger += "Up";
        else print("Error in Y input of PunchInfo");

        if (punchInfo.PunchRawLocal.x == -1)
            Trigger += "Left";
        else if (punchInfo.PunchRawLocal.x == 1)
            Trigger += "Right";
        else print("Error in X input of PunchInfo");

        if (!punchInfo.Hard)
            cam.CamShake(0.06f, 0.1f);
        else
            cam.CamShake(0.2f, 0.3f);

        if (CurrentHealth <= 0) {
            Defeated(punchInfo);
            return;
        }

        anim.SetTrigger(Trigger);

        if (stateinfo.Punching || stateinfo.HardPunching)
        {
            print("i get punch whlie i punch");
            StartCoroutine(FailedRecuperation(punchInfo));
        }
    }

    IEnumerator FailedRecuperation(PunchInfo punchInfo) {
        anim.SetBool("PunchFailed", true);
        PunchFailed = true;
        float rectime = PFailedRecuperationTime;
        if (punchInfo.Hard)
            rectime = rectime * 1.5f;


        yield return new WaitForSeconds(rectime);

        anim.SetBool("PunchFailed", false);
        PunchFailed = false;
    }

    public virtual void StaminaDown(float minusStamina) {
        CurrentStamina -= minusStamina;
        timeStaminadown = Time.time;
        if (CurrentStamina <= 0) {
            Tired();
            return;
        }
    }

    public virtual void Blocked(PunchInfo info) {
        anim.SetTrigger("Blocked");
    }

    public virtual void Tired() {
        StartCoroutine(TiredRecuperation());
    }
    IEnumerator TiredRecuperation()
    {
        anim.SetBool("Tired", true);
        IsTired = true;

        yield return new WaitForSeconds(TiredRecuperationTime);

        anim.SetBool("Tired", false);
        IsTired = false;
    }

    public virtual void Defeated(PunchInfo punchInfo) {
        string trigger = "Defeated";

        if (punchInfo.PunchRawLocal.x == -1)
            trigger += "Left";
        else if (punchInfo.PunchRawLocal.x == 1)
            trigger += "Right";
        else
            print("Error in X input of PunchInfo");

        anim.SetTrigger(trigger);
        CurrentKnockouts++;
        if (CurrentKnockouts < 3)
        {
            KnockedOut = true;
            anim.SetBool("Recovered", true);
        }
    }

    public void Defeat() {
        
        if(CurrentKnockouts >= 3) {
            IsDefeated = true;
        }
    }

    public void Knocking() {
        anim.SetTrigger("Inside");
        anim.SetBool("Recovered", true);
    }
    public void unwait() {
        anim.SetBool("Recovered", false);
    }
    public void RestoreHealth() {
        CurrentHealth = MaxHealth;
    }
    public void RestoreStamina() {
        CurrentStamina = CurrentMaxStamina;
    }

    public virtual void Win() {
        anim.SetTrigger("Win");
    }

    public void PlayClip(AudioSource A, AudioClip Clip)
    {
        A.clip = Clip;
        A.Play();
    }

    // Start is called before the first frame update
    void Start() {
        stateinfo = new Stateinfos();
        LoadData();
    }

    // Update is called once per frame
    void Update() {
        if (pause)
            return;
        UpdateThis();
    }

    void FixedUpdate() {
        FixedUpdatethis();
        //WatchAttackColliders();
    }
}

public class Enemy : Character {
    public PlayerScript Player;
}
