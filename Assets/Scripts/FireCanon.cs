using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FireCanon : StateMachine
{

    private ShootingState _state;

    public GameObject cannonball;
    public float cannonballSpeed = 20;
    public Transform pof;
    public Transform barrel;
    public float scrollIncrements = 100;

    //public float minRotate = -10f;
    //public float maxRotate = 10f;

    public float shootSpeed;

    public Transform cursor;
    public LayerMask layer;

    public Vector3 target;

    public float desireDuration = 3f;
    private float elapsedTime;


    Quaternion rotGoal;
    Vector3 direction;


    public LineRenderer lineVisual;
    public float resolution = 50;
    public float timeOfTheFlight = 10f;
    public float yPoistion;

    public bool shootAtTarget = false;


    private Camera cam;

    public float h = 25f;
    float gravity = -9.81f;

    public Vector3 V0;

    ShootingSound sound;
    public ParticleSystem muzzleFlash;

    private ShootingMode shootingMode;

    public float radius = 10f;

    public float lerpThreshold;

    [SerializeField]
    private AnimationCurve curve;

    public ParticleSystem exp1;
    public ParticleSystem exp2;



    public GameObject cube;
    public Material mt;
    public Color32[] colors;

    void Start()
    {
        cam = Camera.main;
        lineVisual = GetComponent<LineRenderer>();
        sound = GetComponent<ShootingSound>();
        _state = ShootingState.Idle;
        SetState(new Begin(this));
    }
    void Update()
    {
        CanonMovement();
        CursorMovement();
        LaunchProjectile();
        Visiualize();
        ShootingModesSelection();
        
        CurrentState.OnStateUpdate(this);
        
    }
    void CanonMovement()
    {
        if (target == null)
            return;

        elapsedTime += Time.deltaTime;
        float percantageComplete = elapsedTime / desireDuration;

        if (percantageComplete <= 0.05f)
        {

            direction = (target - transform.position).normalized;
            Vector3 canonTargetDirection = direction;
            direction.y = 0;
            canonTargetDirection.x = 0;
            rotGoal = Quaternion.LookRotation(direction.normalized, Vector3.up);
            Quaternion barrelrot = Quaternion.LookRotation(canonTargetDirection);

            transform.rotation = Quaternion.Slerp(transform.rotation, rotGoal, curve.Evaluate(percantageComplete));
            barrel.localRotation = Quaternion.Slerp(barrel.localRotation, barrelrot, curve.Evaluate(percantageComplete));

            float angle = Quaternion.Angle(barrel.localRotation, barrelrot);
            if (angle <= lerpThreshold)
            {
                lineVisual.enabled = true;
                shootAtTarget = true;
                //Debug.Log(curve.Evaluate(percantageComplete));
            }
            else
            {

                lineVisual.enabled = false;
                shootAtTarget = false;

            }
        }
        else
        {
            elapsedTime = 0f;

        }
    }
    void CursorMovement()
    {
        var screenPoint = Input.mousePosition;
        screenPoint.z = 10.0f; //distance of the plane from the camera
        Vector3 pos = Input.mousePosition/*Camera.main.ScreenToWorldPoint(screenPoint)*/;
        cursor.transform.position = pos;
    }
    void LaunchProjectile()
    {
        Ray camRay = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Debug.DrawLine(camRay.origin, camRay.origin + camRay.direction * 1000f, Color.red);
        if (Physics.Raycast(camRay.origin, camRay.direction, out hit, 1000f))
        {
            target = hit.point;
        }
    }
    void Visiualize()
    {
        LaunchData launchdata = CalculateVelocity();
        Vector3 previousDrawPoint = pof.transform.position;
        var pointList = new List<Vector3>();

        for (int i = 0; i <= resolution; i++)
        {
            float simulateTime = i / (float)resolution * launchdata.timeToTarget;
            Vector3 distplacement1 = launchdata.initialVelocity * simulateTime + Vector3.up * gravity * simulateTime * simulateTime / 2f;
            Vector3 drawPoint = pof.position + distplacement1;
            pointList.Add(drawPoint);
            //Debug.DrawLine(previousDrawPoint, drawPoint, Color.red);

            //Debug.Log(i + " " + drawPoint);
            // previousDrawPoint = drawPoint;
        }
        lineVisual.positionCount = pointList.Count;
        lineVisual.SetPositions(pointList.ToArray());
    }
    LaunchData CalculateVelocity()
    {
        float displacementY = target.y - pof.position.y;
        Vector3 displacementXZ = new Vector3(target.x - pof.position.x, 0, target.z - pof.position.z);

        h = displacementY * 1.5f;
        float time;
        Vector3 velocityY;
        Vector3 velocityXZ;

        if (h >= 0)
        {
            time = (Mathf.Sqrt(Mathf.Abs(-2 * h / gravity)) + Mathf.Sqrt(Mathf.Abs(2 * (displacementY - h) / gravity)));
            velocityY = Vector3.up * Mathf.Sqrt(Mathf.Abs(-2 * gravity * h));
            velocityXZ = displacementXZ / time;
            return new LaunchData(velocityY + velocityXZ, time);

        }
        else
        {
            time = 1f;
            float vY = displacementY / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;
            float vXZ = (displacementXZ.magnitude) / time;
            Vector3 result = displacementXZ.normalized;
            result *= vXZ;
            result.y = vY;
            return new LaunchData(result, time);
        }
    }
    struct LaunchData
    {
        public readonly Vector3 initialVelocity;
        public readonly float timeToTarget;

        public LaunchData(Vector3 initialVelocity, float timeToTarget)
        {
            this.initialVelocity = initialVelocity;
            this.timeToTarget = timeToTarget;
        }
    }
    public void Shooting()
    {
        V0 = CalculateVelocity().initialVelocity;
        StopAllCoroutines();
        cube = ObjectPooling.instance.GetObjectFromPool();
        cube.GetComponent<DestoryOnBoundry>().blast = exp2;
        //explosion ;
        if (cube != null)
        {
            cube.transform.localScale = new Vector3(1, 1, 1);
            mt = cube.GetComponent<MeshRenderer>().material;
            mt.color = new Color(0, 0, 0, 255);
            cube.transform.position = pof.transform.position;
            cube.transform.rotation = pof.transform.rotation;
            if (shootAtTarget)
            {
                cube.SetActive(true);
                var rb = cube.GetComponent<Rigidbody>();
                rb.velocity = V0;
                muzzleFlash.Play();
            }

        }
    }
    private void ShootingModesSelection()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("shoot 1");
            //_state = ShootingState.RegularShooting;
            SetState(new ShootModeOne(this));
            //CurrentState.RegularShooting();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetState(new ShootModeTwo(this));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetState(new ShootModeThree(this));
            Debug.Log("shoot 3");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetState(new ShootModeFour(this));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SetState(new ShootModeFive(this));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SetState(new ShootModeSix(this));
        }
    }
    public void CallCoroutine(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }
}

