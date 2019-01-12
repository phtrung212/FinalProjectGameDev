using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading;
using System;


public class HuongDan : MonoBehaviour
{
    public GameObject NPC1;
    public GameObject quai1;
    public GameObject quai2;
    public GameObject quai3;
    public GameObject quai4;
    public GameObject quai5;
    public GameObject quai6;
    public GameObject quai7;
    public GameObject quai8;
    public GameObject quai9;
    public GameObject quai10;
    public GameObject quai11;
    public GameObject quai12;
    public GameObject quai13;
    List<string> arrayNote;
    public int currentLevelMap;
    public GameObject iconSkill1;
    public GameObject iconSkill2;
    public GameObject iconSkill3;
    public GameObject iconSkill4;
    public GameObject iconSkill5;
    public Text lv;
    public Text Textskill1;
    public Text Textskill2;
    public Text Textskill3;
    public Text Textskill4;
    public Text Textskill5;
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
    public ulong experenceCurence;
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
    bool skill3EndSkill = true;
    bool skill3AttackTime = false;
    bool skill4Attack = true;
    bool skill4AttackTime = false;
    bool skill5Attack = true;
    bool skill5EndSkill = true;
    bool skill5AttackTime = false;
    bool attacking = false;
    private Thread oThread1;
    private Thread oThread2;
    private Thread oThread3;
    private Thread oThread4;
    private Thread oThread5;
    private Thread skill1Thread1;
    private Thread skill2Thread2;
    private Thread skill3Thread3;
    private Thread skill4Thread4;
    private Thread skill5Thread5;
    bool skill1Flag = true;
    bool skill2Flag = true;
    bool skill3Flag = true;
    bool skill4Flag = true;
    bool skill5Flag = true;
    int timeSkill1;
    int timeSkill2;
    int timeSkill3;
    int timeSkill4;
    int timeSkill5;
    public AudioSource soundSkill1;
    public AudioSource soundSkill2;
    public AudioSource soundSkill3;
    public AudioSource soundSkill4;
    public AudioSource soundSkill5;
    public AudioSource Chay;
    public AudioSource Nhay;
    public AudioSource Chet;
    bool isRunning;
    private Thread noteThread;
    bool canPrintNote = false;
    bool canPrintNote1 = false;
    bool autoRun = false;
    int index = 1;
    private Thread HuongDanThread;
    int step = 1;
    bool jumpcheck = false;
    bool autorun = false;
    // Use this for initialization
    void Start()
    {
        Application.targetFrameRate = 20;
        HuongDanThread = new Thread(new ThreadStart(printHuongDan));
        HuongDanThread.Start();
        arrayNote = new List<string>();
        noteThread = new Thread(new ThreadStart(countTimeNote));
        noteThread.Start();
        soundSkill5.Stop();
        soundSkill4.Stop();
        soundSkill3.Stop();
        soundSkill2.Stop();
        soundSkill1.Stop();
        Chay.Stop();
        Nhay.Stop();
        Chet.Stop();
        int[] arrayLv = new int[100];
        arrayLv[0] = 100;
        int[] arrayHealth = new int[100];
        arrayHealth[0] = 500;
        int[] arrayMana = new int[100];
        arrayMana[0] = 100;
        for (int i = 1; i < arrayLv.Length; i++)
        {
            arrayLv[i] = arrayLv[i - 1] * 2;
            arrayHealth[i] = (int)(arrayHealth[i - 1] * 1.1);
            arrayMana[i] = (int)(arrayMana[i - 1] * 1.1);
        }
        this.tag = "Player";
        speedCurrence = speed;
        database dataBase = readData();
        level = dataBase.lv;
        experenceCurence = dataBase.experence;
        HP = new QuaiHPManager(arrayHealth[level]);
        Mana = new QuaiHPManager(arrayMana[level]);
        Debug.Log(dataBase.lv);
        Debug.Log(dataBase.experence);
        Debug.Log(HP);
        Debug.Log(Mana);
        Experence = new ExperenceManager(level, experenceCurence, ref HP, ref Mana);
        Debug.Log(Experence.getLevel());
        name = dataBase.name;

        lv.text = (level + 1).ToString();
        Debug.Log(level);
        Debug.Log(experenceCurence);
        Debug.Log(dataBase.lvMap);
        isAttacking = false;
        whoIsUsing = "";
        rigidBody = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<Animator>();
        attack1 = -1;

        healthBar = HPBar.GetComponent<HealthBar>();
        healthBar.setup(HP);

        ManaBar = manaBar.GetComponent<HealthBar>();
        ManaBar.setup(Mana);

        LVBar = expBar.GetComponent<LevelBar>();
        LVBar.setup(Experence);
    }

    // Update is called once per frame
    void Update()
    {
        if (autoRun)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
            }
            if (isRunning == false)
            {
                isRunning = true;
                Chay.Play();
            }
            playerAnimation.SetFloat("Speed", 1);

            if (transform.localScale.x >= 0)
            {
                Debug.Log(speedCurrence);
                rigidBody.velocity = new Vector2(speedCurrence, rigidBody.velocity.y);
            }
            else
            {
                Debug.Log(speedCurrence);
                rigidBody.velocity = new Vector2(-speedCurrence, rigidBody.velocity.y);
            }
        }
        if (canPrintNote1 == true)
        {
            canPrintNote1 = false;
            if (index == 1)
            {
                addNote("Bấm phím ← → để di chuyển qua lại.");
            }
            else if (index == 2)
            {
                addNote("Bấm phím 《 1 》 để tấn công mục tiêu. Đây là kỹ năng cơ bản của đại hiệp. Không tiêu hao mana. Gây một lượng sát thương nhỏ lên 1 mục tiêu. Không có giản cách xuất kỹ năng.");
            }
            else if (index == 3)
            {
                addNote("Bấm phím 《 2 》 để tấn công mục tiêu. Đây là kỹ năng cơ bản của đại hiệp. Tiêu hao 1 mana. Gây sát thương lượng vừa phải lên 1 mục tiêu. Không có giản cách xuất kỹ năng.");
            }
            else if (index == 4)
            {
                addNote("Bấm phím 《 3 》 để tấn công mục tiêu. Đây là kỹ năng cơ bản của đại hiệp. Tiêu hao 2 mana. Gây sát thương lượng vừa phải lên nhiều mục tiêu xung quanh. Giãn cách xuất kỹ năng là 5 giây");
            }
            else if (index == 5)
            {
                addNote("Bấm phím 《 4 》 để tấn công mục tiêu. Đây là kỹ năng trấn phái của đại hiệp. Tiêu hao 20 mana. Gây sát thương lượng lớn lên 1 mục tiêu. Giãn cách xuất kỹ năng là 3 phút");
            }
            else if (index == 6)
            {
                addNote("Bấm phím 《 5 》 để tấn công mục tiêu. Đây là kỹ năng trấn phái của đại hiệp. Tiêu hao 30 mana. Gây sát thương lượng lớn lên nhiều mục tiêu xung quanh. Giãn cách xuất kỹ năng là 5 phút.");
            }
            else if(index == 7)
            {
                addNote("Bấm phím ↑ để nhảy lên.");
            }
            else if(index == 8)
            {
                addNote("Bấm phím 《 G 》 để tự động di chuyển.");
            }
        }
        if (canPrintNote == true)
        {
            canPrintNote = false;
            printNote();
        }
        if (skill2Attack == true)
        {
            iconSkill2.GetComponent<Animator>().SetBool("CD", false);
            Textskill2.text = "";
        }
        else
        {
            Textskill2.text = ConvertTime(timeSkill2);
        }
        if (skill2EndSkill == true)
        {
            soundSkill2.Stop();
            Skill2.GetComponent<Animator>().SetBool("skill2", false);
            playerAnimation.SetBool("skill2", false);
        }
        if (skill1Attack == true)
        {
            soundSkill1.Stop();
            playerAnimation.SetBool("attack1", false);
            iconSkill1.GetComponent<Animator>().SetBool("CD", false);
            Textskill1.text = "";
        }
        else
        {
            Textskill1.text = ConvertTime(timeSkill1);
        }
        if (skill3Attack == true)
        {
            iconSkill3.GetComponent<Animator>().SetBool("CD", false);
            Textskill3.text = "";
        }
        else
        {
            Textskill3.text = ConvertTime(timeSkill3);
        }
        if (skill3EndSkill == true)
        {
            soundSkill3.Stop();
            Skill3.GetComponent<Animator>().SetBool("skill3", false);
            playerAnimation.SetBool("skill3", false);
        }
        /*if (skill3Attack == true)
        {
            Skill3.GetComponent<Animator>().SetBool("skill3", false);
            playerAnimation.SetBool("skill3", false);
            iconSkill3.GetComponent<Animator>().SetBool("CD", false);
            Textskill3.text = "";
        }
        else
        {
            Textskill3.text = ConvertTime(timeSkill3);
        }*/
        if (skill4Attack == true)
        {
            soundSkill4.Stop();
            Skill4.GetComponent<Animator>().SetBool("skill4", false);
            playerAnimation.SetBool("skill4", false);
            iconSkill4.GetComponent<Animator>().SetBool("CD", false);
            Textskill4.text = "";
        }
        else
        {
            Textskill4.text = ConvertTime(timeSkill4);
        }
        if (skill5Attack == true)
        {
            iconSkill5.GetComponent<Animator>().SetBool("CD", false);
            Textskill5.text = "";
        }
        else
        {
            Textskill5.text = ConvertTime(timeSkill5);
        }
        if (skill5EndSkill == true)
        {
            soundSkill5.Stop();
            Skill5.GetComponent<Animator>().SetBool("skill5", false);
            playerAnimation.SetBool("skill5", false);
        }
        if (isAttacking == true)
        {
            Note2.text = "*** Đang chiến đấu ***";
        }
        else
        {
            Note2.text = "";
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            writeData(currentLevelMap);
            SceneManager.LoadScene(0, LoadSceneMode.Single);
            SceneManager.UnloadSceneAsync(currentLevelMap + 1);
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
        ExperenceText.text = string.Format("{0:0}", Experence.getExperence()) + "/" + string.Format("{0:0}", Experence.getExperenceNextLV());
        /*Collider2D[] NPC = Physics2D.OverlapCircleAll(QuaiCheckPoint.position, QuaiCheckRadius, NPCLayer);
        if (NPC.Length > 0)
        {
            writeData(2);
        }
        else
        {
            Debug.Log("---");
            writeData(currentLevelMap);
        }*/

        if (HP.getHP() <= 0)
        {
            Chet.Play();
            HP.returnHP();
            transform.position = new Vector3(0.09f, -3.170004f, transform.position.z);
        }
        isTouchingGround = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer) || Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, QuaiLayer);
        moment = Input.GetAxis("Horizontal");
        if (step == 1)
        {
            if (Mathf.Abs(Mathf.Sqrt(Mathf.Pow(transform.position.x - quai1.transform.position.x, 2) + Mathf.Pow(transform.position.y - quai1.transform.position.y, 2))) > 5)
            {
                if (moment > 0f && autoRun == false)
                {
                    if (isRunning == false)
                    {
                        isRunning = true;
                        Chay.Play();
                    }
                    playerAnimation.SetFloat("Speed", 1);
                    rigidBody.velocity = new Vector2(moment * speedCurrence, rigidBody.velocity.y);
                    transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
                }
                else if (moment < 0f && autoRun == false)
                {
                    if (isRunning == false)
                    {
                        isRunning = true;
                        Chay.Play();
                    }
                    playerAnimation.SetFloat("Speed", 1);
                    Debug.Log(moment);
                    rigidBody.velocity = new Vector2(moment * speedCurrence, rigidBody.velocity.y);
                    transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
                }
                else if (autoRun == false)
                {
                    if (isRunning == true)
                    {
                        isRunning = false;
                        Chay.Stop();
                    }
                    playerAnimation.SetFloat("Speed", 0);
                    rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
                }
            }
            else
            {
                step = 2;
            }
        }
        else if (step == 2)
        {
            if (isRunning == true)
            {
                isRunning = false;
                Chay.Stop();
            }
            playerAnimation.SetFloat("Speed", 0);
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
            index = 2;
            if (quai1.GetComponent<QuaiHuongDan>().HP.getHP() > 0)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1) && skill1Attack == true && attacking == false)
                {
                    soundSkill1.Play();
                    skill1Flag = false;
                    timeSkill1 = 2;
                    oThread1 = new Thread(new ThreadStart(countdownSkill1));
                    oThread1.Start();
                    iconSkill1.GetComponent<Animator>().SetBool("CD", true);
                    attacking = true;
                    skill1Attack = false;
                    skill1AttackTime = true;
                    playerAnimation.SetBool("attack1", true);
                    oThread1 = new Thread(new ThreadStart(Skill1Func));
                    oThread1.Start();
                }
                if (skill1AttackTime == true)
                {
                    skill1AttackTime = false;
                    Collider2D[] enemiesToDamege = Physics2D.OverlapCircleAll(QuaiCheckPoint.position, QuaiCheckRadius, QuaiLayer);
                    if (enemiesToDamege.Length > 0)
                    {
                        float damg = dame + (Experence.getLevel() - enemiesToDamege[0].GetComponent<QuaiHuongDan>().Level) * 0.1f * dame;
                        int damgBlood = (int)damg;
                        damgBlood = (int)(damgBlood * 0.2f);
                        if (damgBlood > 0)
                        {
                            enemiesToDamege[0].GetComponent<QuaiHuongDan>().HP.Damage(damgBlood);
                            Debug.Log(enemiesToDamege[0].GetComponent<QuaiHuongDan>().HP.getHP());
                        }
                    }
                }
            }
            else
            {
                Instantiate(quai1.GetComponent<QuaiHuongDan>().enemy);
                Destroy(quai1);
                step = 3;
                index = 1;
            }
        }
        else if (step == 3)
        {
            if (Mathf.Abs(Mathf.Sqrt(Mathf.Pow(transform.position.x - quai2.transform.position.x, 2) + Mathf.Pow(transform.position.y - quai2.transform.position.y, 2))) > 5)
            {
                if (moment > 0f && autoRun == false)
                {
                    if (isRunning == false)
                    {
                        isRunning = true;
                        Chay.Play();
                    }
                    playerAnimation.SetFloat("Speed", 1);
                    rigidBody.velocity = new Vector2(moment * speedCurrence, rigidBody.velocity.y);
                    transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
                }
                else if (moment < 0f && autoRun == false)
                {
                    if (isRunning == false)
                    {
                        isRunning = true;
                        Chay.Play();
                    }
                    playerAnimation.SetFloat("Speed", 1);
                    Debug.Log(moment);
                    rigidBody.velocity = new Vector2(moment * speedCurrence, rigidBody.velocity.y);
                    transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
                }
                else if (autoRun == false)
                {
                    if (isRunning == true)
                    {
                        isRunning = false;
                        Chay.Stop();
                    }
                    playerAnimation.SetFloat("Speed", 0);
                    rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
                }
            }
            else
            {
                step = 4;
            }
        }
        else if (step == 4)
        {
            if (isRunning == true)
            {
                isRunning = false;
                Chay.Stop();
            }
            playerAnimation.SetFloat("Speed", 0);
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
            index = 3;
            if (quai2.GetComponent<QuaiHuongDan>().HP.getHP() > 0)
            {
                if (Input.GetKeyDown(KeyCode.Alpha2) && Mana.getHP() >= 5 && skill4Attack == true && attacking == false)
                {
                    soundSkill4.Play();
                    skill4Flag = false;
                    timeSkill4 = 1;
                    oThread4 = new Thread(new ThreadStart(countdownSkill4));
                    oThread4.Start();
                    iconSkill4.GetComponent<Animator>().SetBool("CD", true);
                    attacking = true;
                    skill4Attack = false;
                    skill4AttackTime = true;
                    Mana.Damage(1);
                    playerAnimation.SetBool("skill4", true);
                    Skill4.GetComponent<Animator>().SetBool("skill4", true);
                    oThread4 = new Thread(new ThreadStart(Skill4Func));
                    oThread4.Start();

                }
                if (skill4AttackTime == true)
                {
                    skill4AttackTime = false;
                    Collider2D[] enemiesToDamege = Physics2D.OverlapCircleAll(Skill4CheckPoint.transform.position, skill4PhamVi, QuaiLayer);
                    if (enemiesToDamege.Length > 0)
                    {
                        enemiesToDamege[0].GetComponent<QuaiHuongDan>().HP.Damage(dame);
                        float damg = dame + (Experence.getLevel() - enemiesToDamege[0].GetComponent<QuaiHuongDan>().Level) * 0.1f * dame;
                        int damgBlood = (int)damg;
                        if (damgBlood > 0)
                            enemiesToDamege[0].GetComponent<QuaiHuongDan>().HP.Damage((int)(damgBlood * 1.2));
                    }
                }
            }
            else
            {
                Instantiate(quai2.GetComponent<QuaiHuongDan>().enemy);
                Destroy(quai2);
                step = 5;
                index = 1;
            }
        }
        else if (step == 5)
        {
            if (Mathf.Abs(Mathf.Sqrt(Mathf.Pow(transform.position.x - quai7.transform.position.x, 2) + Mathf.Pow(transform.position.y - quai7.transform.position.y, 2))) > 5)
            {
                if (moment > 0f && autoRun == false)
                {
                    if (isRunning == false)
                    {
                        isRunning = true;
                        Chay.Play();
                    }
                    playerAnimation.SetFloat("Speed", 1);
                    rigidBody.velocity = new Vector2(moment * speedCurrence, rigidBody.velocity.y);
                    transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
                }
                else if (moment < 0f && autoRun == false)
                {
                    if (isRunning == false)
                    {
                        isRunning = true;
                        Chay.Play();
                    }
                    playerAnimation.SetFloat("Speed", 1);
                    Debug.Log(moment);
                    rigidBody.velocity = new Vector2(moment * speedCurrence, rigidBody.velocity.y);
                    transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
                }
                else if (autoRun == false)
                {
                    if (isRunning == true)
                    {
                        isRunning = false;
                        Chay.Stop();
                    }
                    playerAnimation.SetFloat("Speed", 0);
                    rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
                }
            }
            else
            {
                step = 6;
            }
        }
        else if (step == 6)
        {
            if (isRunning == true)
            {
                isRunning = false;
                Chay.Stop();
            }
            playerAnimation.SetFloat("Speed", 0);
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
            index = 4;
            if (quai3.GetComponent<QuaiHuongDan>().HP.getHP() > 0 || quai4.GetComponent<QuaiHuongDan>().HP.getHP() > 0 || quai5.GetComponent<QuaiHuongDan>().HP.getHP() > 0 || quai6.GetComponent<QuaiHuongDan>().HP.getHP() > 0 || quai7.GetComponent<QuaiHuongDan>().HP.getHP() > 0)
            {
                Instantiate(quai3.GetComponent<QuaiHuongDan>().enemy);
                if (Input.GetKeyDown(KeyCode.Alpha3) && Mana.getHP() >= 5 && skill5Attack == true && attacking == false)
                {
                    soundSkill5.Play();
                    skill5Flag = false;
                    timeSkill5 = 5;
                    oThread5 = new Thread(new ThreadStart(countdownSkill5));
                    oThread5.Start();
                    iconSkill5.GetComponent<Animator>().SetBool("CD", true);
                    attacking = true;
                    skill5Attack = false;
                    skill5EndSkill = false;
                    skill5AttackTime = true;
                    Mana.Damage(4);
                    playerAnimation.SetBool("skill5", true);
                    Skill5.GetComponent<Animator>().SetBool("skill5", true);
                    oThread5 = new Thread(new ThreadStart(Skill5Func));
                    oThread5.Start();
                }
                if (skill5AttackTime == true)
                {
                    skill5AttackTime = false;
                    Collider2D[] enemiesToDamege = Physics2D.OverlapCircleAll(transform.position, skill5PhamVi, QuaiLayer);
                    for (int i = 0; i < enemiesToDamege.Length; i++)
                    {
                        float damg = dame + (Experence.getLevel() - enemiesToDamege[i].GetComponent<QuaiHuongDan>().Level) * 0.1f * dame;
                        int damgBlood = (int)damg;
                        if (damgBlood > 0)
                            enemiesToDamege[i].GetComponent<QuaiHuongDan>().HP.Damage((int)(damgBlood * 0.5));
                    }
                }
            }
            else
            {
                Instantiate(quai3.GetComponent<QuaiHuongDan>().enemy);
                Instantiate(quai4.GetComponent<QuaiHuongDan>().enemy);
                Instantiate(quai5.GetComponent<QuaiHuongDan>().enemy);
                Instantiate(quai6.GetComponent<QuaiHuongDan>().enemy);
                Instantiate(quai7.GetComponent<QuaiHuongDan>().enemy);
                Destroy(quai3);
                Destroy(quai4);
                Destroy(quai5);
                Destroy(quai6);
                Destroy(quai7);
                step = 7;
                index = 1;
            }
        }
        else if (step == 7)
        {
            if (Mathf.Abs(Mathf.Sqrt(Mathf.Pow(transform.position.x - quai8.transform.position.x, 2) + Mathf.Pow(transform.position.y - quai8.transform.position.y, 2))) > 5)
            {
                if (moment > 0f && autoRun == false)
                {
                    if (isRunning == false)
                    {
                        isRunning = true;
                        Chay.Play();
                    }
                    playerAnimation.SetFloat("Speed", 1);
                    rigidBody.velocity = new Vector2(moment * speedCurrence, rigidBody.velocity.y);
                    transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
                }
                else if (moment < 0f && autoRun == false)
                {
                    if (isRunning == false)
                    {
                        isRunning = true;
                        Chay.Play();
                    }
                    playerAnimation.SetFloat("Speed", 1);
                    Debug.Log(moment);
                    rigidBody.velocity = new Vector2(moment * speedCurrence, rigidBody.velocity.y);
                    transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
                }
                else if (autoRun == false)
                {
                    if (isRunning == true)
                    {
                        isRunning = false;
                        Chay.Stop();
                    }
                    playerAnimation.SetFloat("Speed", 0);
                    rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
                }
            }
            else
            {
                step = 8;
            }
        }
        else if (step == 8)
        {
            if (isRunning == true)
            {
                isRunning = false;
                Chay.Stop();
            }
            playerAnimation.SetFloat("Speed", 0);
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
            index = 5;
            if (quai8.GetComponent<QuaiHuongDan>().HP.getHP() > 0)
            {
                if (Input.GetKeyDown(KeyCode.Alpha4) && Mana.getHP() >= 5 && skill3Attack == true && attacking == false)
                {
                    soundSkill3.Play();
                    skill3Flag = false;
                    timeSkill3 = 180;
                    oThread3 = new Thread(new ThreadStart(countdownSkill3));
                    oThread3.Start();
                    iconSkill3.GetComponent<Animator>().SetBool("CD", true);
                    attacking = true;
                    skill3Attack = false;
                    skill3EndSkill = false;
                    skill3AttackTime = true;
                    Mana.Damage(20);
                    playerAnimation.SetBool("skill3", true);
                    Skill3.GetComponent<Animator>().SetBool("skill3", true);
                    oThread3 = new Thread(new ThreadStart(Skill3Func));
                    oThread3.Start();
                    /*skill3Flag = false;
                    timeSkill3 = 180;
                    oThread3 = new Thread(new ThreadStart(countdownSkill3));
                    oThread3.Start();
                    iconSkill3.GetComponent<Animator>().SetBool("CD", true);
                    attacking = true;
                    skill3Attack = false;
                    skill3AttackTime = true;
                    Mana.Damage(5);
                    playerAnimation.SetBool("skill3", true);
                    Skill3.GetComponent<Animator>().SetBool("skill3", true);
                    oThread3 = new Thread(new ThreadStart(Skill3Func));
                    oThread3.Start();*/
                }
                if (skill3AttackTime == true)
                {
                    skill3AttackTime = false;
                    Collider2D[] enemiesToDamege = Physics2D.OverlapCircleAll(Skill3CheckPoint.transform.position, skill3PhamVi, QuaiLayer);
                    if (enemiesToDamege.Length > 0)
                    {
                        float damg = dame + (Experence.getLevel() - enemiesToDamege[0].GetComponent<QuaiHuongDan>().Level) * 0.1f * dame;
                        int damgBlood = (int)damg;
                        if (damgBlood > 0)
                            enemiesToDamege[0].GetComponent<QuaiHuongDan>().HP.Damage(damgBlood * 6);
                    }
                }
            }
            else
            {
                Instantiate(quai8.GetComponent<QuaiHuongDan>().enemy);
                Destroy(quai8);
                step = 9;
                index = 1;
            }
        }
        else if (step == 9)
        {
            if (Mathf.Abs(Mathf.Sqrt(Mathf.Pow(transform.position.x - quai13.transform.position.x, 2) + Mathf.Pow(transform.position.y - quai13.transform.position.y, 2))) > 5)
            {
                if (moment > 0f && autoRun == false)
                {
                    if (isRunning == false)
                    {
                        isRunning = true;
                        Chay.Play();
                    }
                    playerAnimation.SetFloat("Speed", 1);
                    rigidBody.velocity = new Vector2(moment * speedCurrence, rigidBody.velocity.y);
                    transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
                }
                else if (moment < 0f && autoRun == false)
                {
                    if (isRunning == false)
                    {
                        isRunning = true;
                        Chay.Play();
                    }
                    playerAnimation.SetFloat("Speed", 1);
                    Debug.Log(moment);
                    rigidBody.velocity = new Vector2(moment * speedCurrence, rigidBody.velocity.y);
                    transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
                }
                else if (autoRun == false)
                {
                    if (isRunning == true)
                    {
                        isRunning = false;
                        Chay.Stop();
                    }
                    playerAnimation.SetFloat("Speed", 0);
                    rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
                }
            }
            else
            {
                step = 10;
            }
        }
        else if (step == 10)
        {
            if (isRunning == true)
            {
                isRunning = false;
                Chay.Stop();
            }
            playerAnimation.SetFloat("Speed", 0);
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
            index = 6;
            if (quai9.GetComponent<QuaiHuongDan>().HP.getHP() > 0 || quai10.GetComponent<QuaiHuongDan>().HP.getHP() > 0 || quai11.GetComponent<QuaiHuongDan>().HP.getHP() > 0 || quai12.GetComponent<QuaiHuongDan>().HP.getHP() > 0 || quai13.GetComponent<QuaiHuongDan>().HP.getHP() > 0)
            {
                Debug.Log((quai9.GetComponent<QuaiHuongDan>().HP.getHP()));
                Debug.Log((quai10.GetComponent<QuaiHuongDan>().HP.getHP()));
                Debug.Log((quai11.GetComponent<QuaiHuongDan>().HP.getHP()));
                Debug.Log((quai12.GetComponent<QuaiHuongDan>().HP.getHP()));
                Debug.Log((quai13.GetComponent<QuaiHuongDan>().HP.getHP()));
                if (Input.GetKeyDown(KeyCode.Alpha5) && Mana.getHP() >= 30 && skill2Attack == true && attacking == false)
                {
                    soundSkill2.Play();
                    skill2Flag = false;
                    timeSkill2 = 300;
                    oThread2 = new Thread(new ThreadStart(countdownSkill2));
                    oThread2.Start();
                    iconSkill2.GetComponent<Animator>().SetBool("CD", true);
                    attacking = true;
                    skill2Attack = false;
                    skill2EndSkill = false;
                    skill2AttackTime = true;
                    Mana.Damage(30);
                    playerAnimation.SetBool("skill2", true);
                    Skill2.GetComponent<Animator>().SetBool("skill2", true);
                    oThread2 = new Thread(new ThreadStart(Skill2Func));
                    oThread2.Start();
                }
                if (skill2AttackTime == true)
                {
                    Debug.Log("----------");
                    skill2AttackTime = false;
                    Collider2D[] enemiesToDamege = Physics2D.OverlapCircleAll(transform.position, skill2PhamVi, QuaiLayer);
                    for (int i = 0; i < enemiesToDamege.Length; i++)
                    {
                        enemiesToDamege[i].GetComponent<QuaiHuongDan>().HP.Damage(dame);
                        float damg = dame + (Experence.getLevel() - enemiesToDamege[i].GetComponent<QuaiHuongDan>().Level) * 0.1f * dame;
                        int damgBlood = (int)damg;
                        if (damgBlood > 0)
                        {
                            enemiesToDamege[i].GetComponent<QuaiHuongDan>().HP.Damage((int)((int)(damgBlood * 1.5)));
                        }
                    }
                }
            }
            else
            {
                Instantiate(quai9.GetComponent<QuaiHuongDan>().enemy);
                Instantiate(quai10.GetComponent<QuaiHuongDan>().enemy);
                Instantiate(quai11.GetComponent<QuaiHuongDan>().enemy);
                Instantiate(quai12.GetComponent<QuaiHuongDan>().enemy);
                Instantiate(quai13.GetComponent<QuaiHuongDan>().enemy);
                Destroy(quai9);
                Destroy(quai10);
                Destroy(quai11);
                Destroy(quai12);
                Destroy(quai13);
                step = 11;
                index = 7;
            }

        }
        else if(step == 11)
        {
            Debug.Log(isTouchingGround);
            Debug.Log(isAttacking);
            if (jumpcheck == false)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow) && isTouchingGround && isAttacking == false)
                {
                    Nhay.Play();
                    rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeed);
                    jumpcheck = true;
                }
            }
            else
            {
                step = 12;
                index = 8;
            }
        }
        else if (step == 12)
        {
            if (Mathf.Abs(Mathf.Sqrt(Mathf.Pow(transform.position.x - NPC1.transform.position.x, 2) + Mathf.Pow(transform.position.y - NPC1.transform.position.y, 2))) > 1)
            {
                if (Input.GetKeyDown(KeyCode.G))
                {
                    autoRun = !autoRun;
                    if (autoRun == false)
                    {
                        isRunning = false;
                        Chay.Stop();
                    }
                }
                if (moment > 0f && autoRun == false)
                {
                    if (isRunning == false)
                    {
                        isRunning = true;
                        Chay.Play();
                    }
                    playerAnimation.SetFloat("Speed", 1);
                    rigidBody.velocity = new Vector2(moment * speedCurrence, rigidBody.velocity.y);
                    transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
                }
                else if (moment < 0f && autoRun == false)
                {
                    if (isRunning == false)
                    {
                        isRunning = true;
                        Chay.Play();
                    }
                    playerAnimation.SetFloat("Speed", 1);
                    Debug.Log(moment);
                    rigidBody.velocity = new Vector2(moment * speedCurrence, rigidBody.velocity.y);
                    transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
                }
                else if (autoRun == false)
                {
                    if (isRunning == true)
                    {
                        isRunning = false;
                        Chay.Stop();
                    }
                    playerAnimation.SetFloat("Speed", 0);
                    rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
                }
            }
        }
    }

    void printHuongDan()
    {
        while (true)
        {
            canPrintNote1 = true;
            Thread.Sleep(4000);
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
        database dataBase = new database(name, Experence.getLevel() - 1, Experence.getExperence(), lvMap);
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
        database dataBase = new database(name, 0, 0, 1);
        if (File.Exists(Application.persistentDataPath + "/" + filename))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + filename, FileMode.Open);
            dataBase = (database)bf.Deserialize(file);
            file.Close();
        }
        else
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/" + filename);
            bf.Serialize(file, dataBase);
            file.Close();
            /*BinaryFormatter bff = new BinaryFormatter();
            FileStream filef = File.Create(Application.persistentDataPath + "/" + filename);
            bff.Serialize(filef, dataBase);
            filef.Close();*/
        }
        return dataBase;
    }

    void Skill2Func()
    {
        for (int i = 0; i < 6; i++)
        {
            Thread.Sleep(500);
            skill2AttackTime = true;
        }
        Thread.Sleep(1000);
        skill2EndSkill = true;
        skill2AttackTime = false;
        attacking = false;
        Thread.Sleep(296000);
        skill2Attack = true;

    }

    void Skill3Func()
    {
        for (int i = 0; i < 1; i++)
        {
            Thread.Sleep(500);
            skill3AttackTime = true;
        }
        Thread.Sleep(300);
        skill3EndSkill = true;
        skill3AttackTime = false;
        attacking = false;
        Thread.Sleep(179000);
        skill3Attack = true;

    }
    /*void Skill3Func()
    {
        Thread.Sleep(180000);
        skill3Attack = true;
        skill3AttackTime = false;
        attacking = false;
        
    }*/

    void Skill5Func()
    {
        for (int i = 0; i < 3; i++)
        {
            Thread.Sleep(500);
            skill5AttackTime = true;
        }
        Thread.Sleep(500);
        skill5EndSkill = true;
        skill5AttackTime = false;
        attacking = false;
        Thread.Sleep(3000);
        skill5Attack = true;

    }

    /*void Skill5Func()
    {
        Thread.Sleep(5000);
        skill5Attack = true;
        skill5AttackTime = false;
        attacking = false;
        
    }*/

    void Skill4Func()
    {
        Thread.Sleep(1000);
        skill4Attack = true;
        skill4AttackTime = false;
        attacking = false;

    }

    void Skill1Func()
    {
        for (int i = 0; i < 3; i++)
        {
            Thread.Sleep(500);
            skill1AttackTime = true;
        }
        Thread.Sleep(500);
        skill1Attack = true;
        skill1AttackTime = false;
        attacking = false;

    }

    void countdownSkill2()
    {
        for (int i = 300; i >= 0; i--)
        {
            Thread.Sleep(1000);
            timeSkill2--;
        }
    }

    void countdownSkill5()
    {
        for (int i = 5; i >= 0; i--)
        {
            Thread.Sleep(1000);
            timeSkill5--;
        }
    }

    void countdownSkill4()
    {
        for (int i = 1; i >= 0; i--)
        {
            Thread.Sleep(1000);
            timeSkill4--;
        }
    }

    void countdownSkill1()
    {
        for (int i = 2; i >= 0; i--)
        {
            Thread.Sleep(1000);
            timeSkill1--;
        }
    }

    void countdownSkill3()
    {
        for (int i = 180; i >= 0; i--)
        {
            Thread.Sleep(1000);
            timeSkill3--;
        }
    }

    private string ConvertTime(int second)
    {
        TimeSpan t = TimeSpan.FromSeconds(second);
        // Converts the total miliseconds to the human readable time format
        if (t.Minutes != 0)
        {
            return t.Minutes.ToString() + "m";
        }
        else
        {
            return t.Seconds.ToString();
        }
    }

    public void addNote(string note)
    {
        if (arrayNote.Count > 5)
            arrayNote.RemoveAt(0);
        arrayNote.Add(note);
        printNote();
    }

    void countTimeNote()
    {
        while (true)
        {
            if (arrayNote.Count > 0)
            {
                Thread.Sleep(3000);
                arrayNote.RemoveAt(0);
                canPrintNote = true;
            }
        }
    }

    void printNote()
    {
        Note.text = "";
        for (int i = 0; i < arrayNote.Count; i++)
        {
            Note.text = Note.text + "\n" + arrayNote[i];
        }

    }
}
