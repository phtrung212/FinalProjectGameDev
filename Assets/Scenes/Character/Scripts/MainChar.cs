using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading;

public class MainChar : MonoBehaviour {
    public Text lv;
    public Text HPText;
    public Text ManaText;
    public Text ExperenceText;
    public Text Note;
    public Text Note2;
    public LayerMask NPCLayer;
    public GameObject Skill2;
    public int skill2CD;
    public int skill1CD;
    public float skill2PhamVi;
    public GameObject Skill3;
    public int skill3CD;
    public float skill3PhamVi;
    public Transform Skill3CheckPoint;
    public GameObject Skill4;
    public int skill4CD;
    public float skill4PhamVi;
    public Transform Skill4CheckPoint;
    public GameObject Skill5;
    public float skill5PhamVi;
    public Transform Skill5CheckPoint;
    public string name;
    string filename = "dataplayer.gd";
    public int level;
    public int dame;
    public string whoIsUsing;
    public bool isAttacking;
    public Camera Camera;
    public int HPMax;
    public int ManaMax;
    public float speed = 5f;
    public float jumpSpeed = 10f;
    private float moment = 0f;
    private Rigidbody2D rigidBody;
    public Transform groundCheckPoint;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    private bool isTouchingGround;
    private Animator playerAnimation;
    int attack1;
    public LayerMask QuaiLayer;
    public Transform QuaiCheckPoint;
    public float QuaiCheckRadius;
    public Transform pfHealthBar;
    Transform healthBarTransform;
    HealthBar healthBar;
    public QuaiHPManager HP;
    public Transform pfManaBar;
    Transform ManaBarTransform;
    HealthBar ManaBar;
    public QuaiHPManager Mana;
    public Transform pfLVBar;
    Transform LVBarTransform;
    LevelBar LVBar;
    public int experenceCurence;
    public ExperenceManager Experence;
    public float speedCurrence;
    public GameObject HPBar;
    public GameObject manaBar;
    public GameObject expBar;
    /*int skill2Attack = 0;
    int skill1Attack = 0;
    int skill3Attack = 0;
    int skill4Attack = 0;*/
    bool skill2Attack = true;
    bool skill2AttackTime = false;
    bool skill2EndSkill = true;
    bool skill1Attack = true;
    bool skill1AttackTime = false;
    bool skill3Attack = true;
    bool skill3AttackTime = false;
    bool skill4Attack = true;
    bool skill4AttackTime = false;
    bool skill5Attack = true;
    bool skill5AttackTime = false;
    bool attacking = false;
    private Thread oThread1;
    private Thread oThread2;
    private Thread oThread3;
    private Thread oThread4;
    private Thread oThread5;
    // Use this for initialization
    void Start () {
        this.tag = "Player";
        speedCurrence = speed;
        database dataBase = readData();
        level = dataBase.lv;
        Experence = new ExperenceManager(level, experenceCurence, ref HP, ref Mana);
        name = dataBase.name;
        experenceCurence = dataBase.experence;
        lv.text = (level+1).ToString();
        Debug.Log(level);
        Debug.Log(experenceCurence);
        Debug.Log(dataBase.lvMap);
        isAttacking = false;
        whoIsUsing = "";
        rigidBody = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<Animator>();
        attack1 = -1;
        HP = new QuaiHPManager(ExperenceManager.getHealthMax(level));
        healthBar = HPBar.GetComponent<HealthBar>();
        healthBar.setup(HP);
        Mana = new QuaiHPManager(ExperenceManager.getManaMax(level));
        ManaBar = manaBar.GetComponent<HealthBar>();
        ManaBar.setup(Mana);
        LVBar = expBar.GetComponent<LevelBar>();
        LVBar.setup(Experence);
    }

    // Update is called once per frame
    void Update() {
        if(skill2EndSkill == true)
        {
            Skill2.GetComponent<Animator>().SetBool("skill2", false);
            playerAnimation.SetBool("skill2", false);
        }
        if (skill1Attack == true)
        {
            playerAnimation.SetBool("attack1", false);
        }
        if (skill3Attack == true)
        {
            Skill3.GetComponent<Animator>().SetBool("skill3", false);
            playerAnimation.SetBool("skill3", false);
        }
        if (skill4Attack == true)
        {
            Skill4.GetComponent<Animator>().SetBool("skill4", false);
            playerAnimation.SetBool("skill4", false);
        }
        if (skill5Attack == true)
        {
            Skill5.GetComponent<Animator>().SetBool("skill5", false);
            playerAnimation.SetBool("skill5", false);
        }
        if (isAttacking == true)
        {
            Note.text = "Tiến nhập chiến đấu trung.";
        }
        else
        {
            Note.text = "";
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
        //database dataBase = readData();
        //level = dataBase.lv;
        lv.text = Experence.getLevel().ToString();
        int HPCurrenceTemp = (int)HP.getHP();
        int HPMaxTemp = (int)HP.getHPMax();
        HPText.text = HPCurrenceTemp.ToString() + "/" + HPMaxTemp.ToString();
        int ManaCurrenceTemp = (int)Mana.getHP();
        int ManaMaxTemp = (int)Mana.getHPMax();
        ManaText.text = ManaCurrenceTemp.ToString() + "/" + ManaMaxTemp.ToString();
        ExperenceText.text = Experence.getExperence().ToString() + "/" + Experence.getExperenceNextLV().ToString();
        Collider2D[] NPC = Physics2D.OverlapCircleAll(QuaiCheckPoint.position, QuaiCheckRadius, NPCLayer);
        if (NPC.Length > 0)
        {
            writeData(2);
        }
        else
        {
            Debug.Log("---");
            writeData();
        } 
       
        if (HP.getHP() <= 0)
        {
            HP.returnHP();
            transform.position = new Vector3(0.09f, -3.170004f, transform.position.z);
        }
        isTouchingGround = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer) || Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, QuaiLayer);
        moment = Input.GetAxis("Horizontal");
        if (moment > 0f)
        {
            playerAnimation.SetFloat("Speed", 1);
            rigidBody.velocity = new Vector2(moment * speedCurrence, rigidBody.velocity.y);
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
        else if (moment < 0f)
        {
            playerAnimation.SetFloat("Speed", 1);
            rigidBody.velocity = new Vector2(moment * speedCurrence, rigidBody.velocity.y);
            transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
        else
        {
            playerAnimation.SetFloat("Speed", 0);
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && isTouchingGround && isAttacking == false)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeed);
        }


        playerAnimation.SetBool("onGround", isTouchingGround);
        if (Input.GetKeyDown(KeyCode.Alpha5) && Mana.getHP() >= 5 && skill5Attack == true && attacking == false)
        {
            attacking = true;
            skill5Attack = false;
            skill5AttackTime = true;
            Mana.Damage(5);
            playerAnimation.SetBool("skill5", true);
            Skill5.GetComponent<Animator>().SetBool("skill5", true);
            oThread5 = new Thread(new ThreadStart(Skill5Func));
            oThread5.Start();

        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && Mana.getHP() >= 5 && skill4Attack == true && attacking == false)
        {
            attacking = true;
            skill4Attack = false;
            skill4AttackTime = true;
            Mana.Damage(5);
            playerAnimation.SetBool("skill4", true);
            Skill4.GetComponent<Animator>().SetBool("skill4", true);
            oThread4 = new Thread(new ThreadStart(Skill4Func));
            oThread4.Start();

        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && Mana.getHP() >= 5 && skill3Attack == true && attacking == false)
        {
            attacking = true;
            skill3Attack = false;
            skill3AttackTime = true;
            Mana.Damage(5);
            playerAnimation.SetBool("skill3", true);
            Skill3.GetComponent<Animator>().SetBool("skill3", true);
            oThread3 = new Thread(new ThreadStart(Skill3Func));
            oThread3.Start();
            
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && Mana.getHP() >= 20 && skill2Attack == true && attacking == false)
        {
            attacking = true;
            skill2Attack = false;
            skill2EndSkill = false;
            skill2AttackTime = true;
            Mana.Damage(5);
            playerAnimation.SetBool("skill2", true);
            Skill2.GetComponent<Animator>().SetBool("skill2", true);
            oThread2 = new Thread(new ThreadStart(Skill2Func));
            oThread2.Start();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) && skill1Attack == true && attacking == false)
        {
            attacking = true;
            skill1Attack = false;
            skill1AttackTime = true;
            Mana.Damage(2);
            playerAnimation.SetBool("attack1", true);
            oThread1 = new Thread(new ThreadStart(Skill1Func));
            oThread1.Start();
        }
        if (skill3AttackTime == true)
        {
            skill3AttackTime = false;
            Collider2D[] enemiesToDamege = Physics2D.OverlapCircleAll(Skill3CheckPoint.transform.position, skill3PhamVi, QuaiLayer);
            if (enemiesToDamege.Length > 0)
            {
                enemiesToDamege[0].GetComponent<AutoAttack>().HP.Damage(dame);
                float damg = dame + (Experence.getLevel() - enemiesToDamege[0].GetComponent<AutoAttack>().Level) * 0.1f * dame;
                int damgBlood = (int)damg;
                if (damgBlood > 0)
                    enemiesToDamege[0].GetComponent<AutoAttack>().HP.Damage(damgBlood);
            }
        }
        if(skill4AttackTime == true)
        {
            skill4AttackTime = false;
            Collider2D[] enemiesToDamege = Physics2D.OverlapCircleAll(Skill4CheckPoint.transform.position, skill4PhamVi, QuaiLayer);
            if (enemiesToDamege.Length > 0)
            {
                enemiesToDamege[0].GetComponent<AutoAttack>().HP.Damage(dame);
                float damg = dame + (Experence.getLevel() - enemiesToDamege[0].GetComponent<AutoAttack>().Level) * 0.1f * dame;
                int damgBlood = (int)damg;
                if (damgBlood > 0)
                    enemiesToDamege[0].GetComponent<AutoAttack>().HP.Damage(damgBlood);
            }
        }
        if (skill5AttackTime == true)
        {
            skill5AttackTime = false;
            Collider2D[] enemiesToDamege = Physics2D.OverlapCircleAll(transform.position, skill5PhamVi, QuaiLayer);
            for (int i = 0; i < enemiesToDamege.Length; i++)
            {
                enemiesToDamege[i].GetComponent<AutoAttack>().HP.Damage(dame);
                float damg = dame + (Experence.getLevel() - enemiesToDamege[0].GetComponent<AutoAttack>().Level) * 0.1f * dame;
                int damgBlood = (int)damg;
                if (damgBlood > 0)
                    enemiesToDamege[0].GetComponent<AutoAttack>().HP.Damage(damgBlood);
            }
        }
        if (skill2AttackTime == true)
        {
            skill2AttackTime = false;
            Collider2D[] enemiesToDamege = Physics2D.OverlapCircleAll(transform.position, skill2PhamVi, QuaiLayer);
            for (int i = 0; i < enemiesToDamege.Length; i++)
            {
                enemiesToDamege[i].GetComponent<AutoAttack>().HP.Damage(dame);
                float damg = dame + (Experence.getLevel() - enemiesToDamege[0].GetComponent<AutoAttack>().Level) * 0.1f * dame;
                int damgBlood = (int)damg;
                if (damgBlood > 0)
                    enemiesToDamege[0].GetComponent<AutoAttack>().HP.Damage(damgBlood);
            }
        }
        if (skill1AttackTime == true)
        {
            skill1AttackTime = false;
            Collider2D[] enemiesToDamege = Physics2D.OverlapCircleAll(QuaiCheckPoint.position, QuaiCheckRadius, QuaiLayer);
            if (enemiesToDamege.Length > 0)
            {
                enemiesToDamege[0].GetComponent<AutoAttack>().HP.Damage(dame);
                float damg = dame + (Experence.getLevel() - enemiesToDamege[0].GetComponent<AutoAttack>().Level) * 0.1f * dame;
                int damgBlood = (int)damg;
                if (damgBlood > 0)
                    enemiesToDamege[0].GetComponent<AutoAttack>().HP.Damage(damgBlood);
            }
        }

        


    }

    

    public void setSpeedCurrence(float cre)
    {
        this.speedCurrence = speed * cre;
    }

    public void setAttacking(string name)
    {
        if (whoIsUsing == "")
        {
            this.whoIsUsing = name;
            isAttacking = true;
        }
    }

    public void cancalAttacking(string name)
    {
        if (whoIsUsing == name)
        {
            this.whoIsUsing = "";
            this.speedCurrence = this.speed;
            isAttacking = false;
        }
    }

    public void writeData(int lvMap)
    {
        database dataBase = new database(name, Experence.getLevel()-1, Experence.getExperence(), lvMap);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + filename);
        bf.Serialize(file, dataBase);
        file.Close();
    }

    public void writeData()
    {
        database dataBase = new database(name, Experence.getLevel() - 1, Experence.getExperence());
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + filename);
        bf.Serialize(file, dataBase);
        file.Close();
    }

    database readData()
    {
        Debug.Log(Application.persistentDataPath + "/" + filename);
        database dataBase = new database(name, 1, 0, 1);
        if(File.Exists(Application.persistentDataPath + "/" + filename))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + filename, FileMode.Open);
            dataBase = (database)bf.Deserialize(file);
            file.Close();
        }
        else
        {
            dataBase = new database(name, 0, 0, 1);
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/" + filename);
            bf.Serialize(file, dataBase);
            file.Close();
        }
        return dataBase;
    }

    void Skill2Func()
    {
        for(int i = 0; i < 3; i++)
        {
            Thread.Sleep(1000);
            skill2AttackTime = true;
        }
        Thread.Sleep(1000);
        skill2EndSkill = true;
        skill2AttackTime = false;
        attacking = false;
        Thread.Sleep(10000);
        skill2Attack = true;
    }

    void Skill3Func()
    {
        Thread.Sleep(800);
        skill3Attack = true;
        skill3AttackTime = false;
        attacking = false;
    }

    void Skill5Func()
    {
        Thread.Sleep(700);
        skill5Attack = true;
        skill5AttackTime = false;
        attacking = false;
    }

    void Skill4Func()
    {
        Thread.Sleep(1000);
        skill4Attack = true;
        skill4AttackTime = false;
        attacking = false;
    }

    void Skill1Func()
    {
        for (int i = 0; i < 2; i++)
        {
            Thread.Sleep(500);
            skill1AttackTime = true;
        }
        Thread.Sleep(500);
        skill1Attack = true;
        skill1AttackTime = false;
        attacking = false;
    }
}
