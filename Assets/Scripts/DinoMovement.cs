using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class DinoMovement : MonoBehaviour
{
   [SerializeField] private float _forwardVelocity = 20;
    private Rigidbody _rb;
    private Vector3 _velocity;
    //Swerving
    private DinoInputScript _dinoInput;
    [SerializeField] private float xMovementSpeed = 0.3f;
    [SerializeField] private float maxSwerveRange = 1f;

    public Animator _anim;

    private bool _isAllMovementDisabled = false;

    private int _maxScale = 0;

    [SerializeField] private float _runAnimSpeed = 1.2f;

    private flyingPowerupFollow testingFollo;

    private cineCamScript camFollow;

    private int flightDirection;

    //Conditions Related
    private bool _isGrounded = true;

    private bool _inRage = false;

    private bool _goUp = false;

    //Audio Stuff
    private AudioSource[] sound;

    private AudioSource mageSound;
    private AudioSource bite;
    private AudioSource powerUp;
    private AudioSource trexRoar;
    private AudioSource hit;

    private AudioSource hardBite;

    private AudioSource explosionSound;
    private AudioSource deathSound;
    
    public static AudioSource bossMusic;

    //UI RELATED
    public bool hasStarted = false;
    private bool triggeredRun = false;

    public bool paused = false;

    public float rageTime= 10f;
    
    //obs Collision
    public GameObject explosion;
    public GameObject powerUpParticles;
    public GameObject flypowerUpParticles;


    //HungerStuff
    public int hungerValue = 100;
    public int currentHealth;

    public HealthBar healthBar;
    public GameObject healthBarObj;
    

    //Boss Stuff
    public bool inBossFight = false;
    public GameObject bossRef;

    public Level_Loader ui;

    public GameObject wings;
    
    void Start()
    {
        //Movement Stuff
        _rb = GetComponent<Rigidbody>();
        _velocity = Vector3.zero;
        _dinoInput = GetComponent<DinoInputScript>();
        //Animation Stuff
        _anim =  GetComponent<Animator>();
        _anim.SetFloat("runSpeed", _runAnimSpeed);
        //PowerUps Stuff
        testingFollo = FindObjectOfType<flyingPowerupFollow>();
        //Cam
        camFollow = FindObjectOfType<cineCamScript>();
        //Sound Stuff
        sound = GetComponents<AudioSource>();
        mageSound = sound[0];
        bite = sound[1];
        powerUp = sound[2];
        trexRoar = sound[3];
        hit = sound[4];
        hardBite = sound[5];
        explosionSound = sound[6];
        deathSound = sound[7];
        bossMusic = sound[8];
        //bossfightStuff
        ui = FindObjectOfType<Level_Loader>();

        //hunger Bar
        currentHealth = hungerValue;
        healthBar = healthBarObj.GetComponent<HealthBar>();
        healthBar.SetMaxHealth(hungerValue);
        
        wings.SetActive(false);
    }
    private void Update() {
        mageSound.volume = SetVolume.musicVolume;
        bite.volume = SetVolume.musicVolume;
        powerUp.volume = SetVolume.musicVolume;
        trexRoar.volume = SetVolume.musicVolume;
        hit.volume = SetVolume.musicVolume;
        hardBite.volume = SetVolume.musicVolume;
        explosionSound.volume = SetVolume.musicVolume;
        deathSound.volume = SetVolume.musicVolume;
        bossMusic.volume = SetVolume.musicVolume;
    }
    private void FixedUpdate() 
    {
        mageSound.volume = SetVolume.musicVolume;
        bite.volume = SetVolume.musicVolume;
        powerUp.volume = SetVolume.musicVolume;
        trexRoar.volume = SetVolume.musicVolume;
        hit.volume = SetVolume.musicVolume;
        hardBite.volume = SetVolume.musicVolume;
        explosionSound.volume = SetVolume.musicVolume;
        deathSound.volume = SetVolume.musicVolume;
        bossMusic.volume = SetVolume.musicVolume;
        if(!inBossFight)
        {
            if(hasStarted)
            {
                triggeredRun = true;
                camFollow.StartCam = false;
                if(paused)
                {
                    paused = false;
                }
                if(triggeredRun)
                {
                    _anim.SetTrigger("GameStarted");
                }
                if(_isAllMovementDisabled == false)
                {
                    forwardMovement();
                    Movement();
                    
                }
                if(_isGrounded == false)
                    {
                        GetComponent<Rigidbody>().useGravity = true;
                        Vector3 flightPos = transform.position;
                        flightPos.y = 0f;
                        transform.position = Vector3.Lerp(transform.position, flightPos, 0.05f);

                    }
                if(_goUp == true)
                {
                    flight();
                }
            }
            if(paused)
            {
                _anim.SetTrigger("paused");
                triggeredRun = false;
                if(hasStarted)
                {
                    hasStarted =false;
                }
            }
        //Debug.Log(hungerValue);
        }
        
    }

    // Update is called once per frame
    void flight()
    {
        
        Vector3 flightPos = transform.position;
        flightPos.y += 8f;
        GetComponent<Rigidbody>().useGravity = false;
        transform.position = Vector3.MoveTowards(transform.position, flightPos, 0.2f);
        _isGrounded = false;
        
    }
    //Functions
    void forwardMovement()
    {
        _velocity.z = _forwardVelocity;
        _rb.velocity = _velocity;
        

    }
    void Movement()
    {
        if(!_inRage)
        {
            float swerveXFactor = Time.deltaTime * xMovementSpeed * _dinoInput.movementX;
            swerveXFactor = Mathf.Clamp(swerveXFactor,-maxSwerveRange, maxSwerveRange);
            transform.Translate(x:swerveXFactor, y:0, z:0);
        }
    }
    private void OnCollisionEnter(Collision other) 
    {
        if(!_inRage)
        {
            if(other.gameObject.tag == "obstacle")
            {
                if(currentHealth > 0)
                {
                    currentHealth -= 10;
                    healthBar.SetHealth(currentHealth);
                }
                Debug.Log(currentHealth);
                bite.Play();
                StartCoroutine(ObstacleCollision(0.8f ,other.gameObject, 0.2f));
            }
            else if(other.gameObject.tag == "worldObs")
            {
                if(currentHealth > 0)
                {
                    currentHealth -= 5;
                    healthBar.SetHealth(currentHealth);
                }
                Debug.Log(currentHealth);
                GameObject clone = (GameObject)Instantiate (explosion, other.transform.position, Quaternion.identity);
                Destroy(clone,1f);
                StartCoroutine(ObstacleCollision(0.8f ,other.gameObject, 0f));
                Destroy(other.gameObject);
            }
            else if(other.gameObject.tag == "powerup")
            {
                _anim.SetTrigger("Roar");
                StartCoroutine(SpeedScalePowaUp(1.8f ,other.gameObject));
            }
            else if(other.gameObject.tag == "flyingPowerUp")
            {
                if(currentHealth > 0)
                {
                    currentHealth -= 5;
                    healthBar.SetHealth(currentHealth);
                }
                _anim.SetTrigger("lookBack");
                StartCoroutine(flyingPower(other.gameObject,7f));
            }
            else if(other.gameObject.tag == "death")
            {
                _anim.SetTrigger("death");
                Level_Loader.levelIndex = SceneManager.GetActiveScene().buildIndex;
                deathSound.Play();
                Invoke("retryScreen",0.9f);
            }
            else if(other.gameObject.tag == "food")
            {
                float sizeFactor = 0.05f;
                Vector3 size = new Vector3(transform.localScale.x + sizeFactor ,transform.localScale.y+ sizeFactor , transform.localScale.z+ sizeFactor );
                transform.DOScale(size,7);
                if(currentHealth < 100)
                {
                    currentHealth+=5;
                    healthBar.SetHealth(currentHealth);
                }
                Debug.Log(currentHealth);
                _anim.SetTrigger("Bite");
                hardBite.Play();
                Destroy(other.gameObject,0.8f);
            }
            else if(other.gameObject.tag == "rage")
            {
                if(currentHealth > 0)
                {
                    currentHealth -= 25;
                    healthBar.SetHealth(currentHealth);
                }
                trexRoar.Play();
                _anim.SetTrigger("Rage");
                Destroy(other.gameObject,0.8f);
                StartCoroutine(Rage(6.2f));
            }
            else if(other.gameObject.tag == "boss")
            {
                
                bossRef = other.gameObject;
                inBossFight = true;
                Level_Loader.levelIndex = SceneManager.GetActiveScene().buildIndex;
                _anim.SetTrigger("infight");
                ui.inFight();
            }

        }
        else
        {
            if(other.gameObject.tag!="ground")
            {
                GameObject clone = (GameObject)Instantiate (explosion, other.transform.position, Quaternion.identity);
                Destroy(clone,1f);
                Destroy(other.gameObject);
                explosionSound.volume = 0.3f;
                explosionSound.Play();
            }
        }
        
    }
    void retryScreen()
    {
        SceneManager.LoadScene("Retry");
    }
    IEnumerator Rage(float time)
    {
        //Setting Up rage Properties i.e scaling and camera and controls disabling
        _inRage = true;
        float temp = _forwardVelocity;
        _forwardVelocity = 0f;
        Vector3 ogScale = transform.localScale;
        transform.DOScale(3f,7);
        camFollow._normalCam = !camFollow._normalCam;

        yield return new WaitForSeconds(time);
        // Setting up speedUp properties
        _forwardVelocity = temp * 1.5f;
        _runAnimSpeed += 0.08f;
        _anim.SetFloat("runSpeed", _runAnimSpeed);
        yield return new WaitForSeconds(rageTime);
        // re-setting the rage properties
        transform.DOScale(ogScale,7);
        _forwardVelocity = temp;
        _runAnimSpeed -=0.07f;
        _anim.SetFloat("runSpeed", _runAnimSpeed);
        _inRage = false;
        camFollow._normalCam = !camFollow._normalCam;
    }
    IEnumerator ObstacleCollision(float time, GameObject obj, float animDelay)
    {
        _isAllMovementDisabled = true;
        float velocity = _forwardVelocity;
        _forwardVelocity = 0f;
        hit.Play();
        yield return new WaitForSeconds(animDelay);
        _anim.SetTrigger("Hit");
        yield return new WaitForSeconds(time);
        _isAllMovementDisabled = false;
        _forwardVelocity = velocity;

        //Destroy(obj);
        
    }
    //For obstacles(Enemies) and Size+Speed Powerups
    IEnumerator SpeedScalePowaUp(float time, GameObject obj)
    {
        _isAllMovementDisabled = true;
        powerUp.Play();
        GameObject clone = (GameObject)Instantiate (powerUpParticles, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(time);
        Destroy(clone);
        //scaling
        Vector3 oldScale = transform.localScale;
        if(_maxScale < 3)
        {
            Vector3 size = new Vector3(transform.localScale.x + 0.18f ,transform.localScale.y+ 0.18f , transform.localScale.z+ 0.18f );
            transform.DOScale(size,4);
            //testingFollo.offset = testingFollo.offset + 0.60f *Time.deltaTime;
            _maxScale +=1;
            _runAnimSpeed += 0.07f;
            _anim.SetFloat("runSpeed", _runAnimSpeed);
            _forwardVelocity += 0.4f;
        }
        _isAllMovementDisabled = false;

        Destroy(obj);
    }
    //For flying powerup
    IEnumerator flyingPower(GameObject obj, float time){
        
        //testingFollo.poweredUp = true;
        Destroy(obj);
        _isAllMovementDisabled = true;
        mageSound.Play();
        Vector3 pos = transform.position;
        pos.y += 0.5f;
        GameObject clone = (GameObject)Instantiate (flypowerUpParticles, pos, Quaternion.identity);
        yield return new WaitForSeconds(1.6f);
        //Destroy(clone);
        wings.SetActive(true);
        _isAllMovementDisabled = false;
        _goUp = true;
      // _anim.SetTrigger("fly");
        float dinoRun = _anim.GetFloat("runSpeed");
        _anim.SetFloat("runSpeed", 0f); 
        yield return new WaitForSeconds(time);

        _anim.SetFloat("runSpeed",dinoRun);
        _anim.SetTrigger("fall");
        //testingFollo.poweredUp = false;
        _goUp = false;

        wings.SetActive(false);
        yield return new WaitForSeconds(1.2f);
        Destroy(clone);
        _isGrounded = true;
    }
    
}
