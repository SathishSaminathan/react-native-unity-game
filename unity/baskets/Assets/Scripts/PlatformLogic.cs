using UnityEngine;
using System.Collections;

public class PlatformLogic : MonoBehaviour
{
    private GameObject cylinder = null;
    private Vector3 throwStartPos;
    private Vector3 throwMovPos;
    private Vector3 touchReleasePos;
    private LineRenderer lineRenderer;

    private bool isMoving = false;
    private bool isLeftToRightMovement = false;
    private bool hasCreatedNext = false;
    private bool hasCollided = false;
    private Vector3 start;

    public static float MinHeight = -10;

    void Start()
    {
        cylinder = GameObject.FindGameObjectWithTag("Player");
        throwStartPos = transform.position;
        hasCollided = false;
        lineRenderer = this.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasCollided)
            Move();
        if (NextNeedToBeCreated())
            PlatformGeneration.GeneratePlatform(this.transform.position.x, this.transform.position.y);
    }


    private bool NextNeedToBeCreated()
    {
        if (!hasCreatedNext)
        {
            wasHere = true;
            float blockPosition = transform.position.y;
            if (blockPosition < Camera.main.transform.position.y + 10)
            {
                hasCreatedNext = true;
                return true;
            }
        }
        return false;
    }

    bool wasHere = false;

    void Move()
    {
        if (Input.touchCount > 0)
        {
            switch (Input.GetTouch(0).phase)
            {
                case TouchPhase.Began:
                    cylinder.transform.position = throwStartPos + Vector3.up;
                    cylinder.GetComponent<Rigidbody2D>().isKinematic = true;
                    break;
                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    throwMovPos = GetPosition();

                    if (throwMovPos.y > throwStartPos.y)
                        throwMovPos.y = throwStartPos.y;

                    float temp = Mathf.Clamp(Vector3.Distance(throwMovPos, throwStartPos), -2f, 2f);
                    differenceVector = (Vector3.Normalize(throwStartPos - throwMovPos)) * temp;
                    transform.position = throwStartPos - differenceVector;
                    cylinder.transform.position = transform.position + Vector3.up;
                    throwMovPos.y = transform.localPosition.y;
                    throwMovPos.x = transform.localPosition.x;

                    lineRenderer.SetPosition(0, new Vector3(throwStartPos.x, throwStartPos.y + 1f, 0.1f));
                    lineRenderer.SetPosition(1, new Vector3(throwMovPos.x - .5f, throwMovPos.y + .5f, 0.1f));
                    lineRenderer.SetPosition(2, new Vector3(throwStartPos.x, throwStartPos.y + 1f, 0.1f));
                    lineRenderer.SetPosition(3, new Vector3(throwMovPos.x + .5f, throwMovPos.y + .5f, 0.1f));

                    //lineRenderer.SetPosition(0, new Vector3(throwMovPos.x -.5f, throwMovPos.y + .5f, 0.1f));
                    //lineRenderer.SetPosition(1, new Vector3(throwStartPos.x, throwStartPos.y + 1f, 0.1f));
                    //lineRenderer.SetPosition(2, new Vector3(throwMovPos.x +.5f, throwMovPos.y + .5f, 0.1f));

                    //float angle = Mathf.Atan2(differenceVector.x, differenceVector.y) * Mathf.Rad2Deg;                    
                    //transform.rotation = Quaternion.Euler(new Vector3(0, 0, - angle));

                    break;
                case TouchPhase.Canceled:
                case TouchPhase.Ended:
                    Rigidbody2D rigid2D = cylinder.GetComponent<Rigidbody2D>();
                    rigid2D.isKinematic = false;
                    rigid2D.AddForce(differenceVector * 700.0f);

                    lineRenderer.SetPosition(0, Vector3.zero);
                    lineRenderer.SetPosition(1, Vector3.zero);
                    lineRenderer.SetPosition(2, Vector3.zero);
                    lineRenderer.SetPosition(3, Vector3.zero);

                    //transform.rotation = new Quaternion();
                    start = transform.position;
                    isMoving = true;
                    isLeftToRightMovement = throwStartPos.x > transform.position.x;
                    break;
                default:
                    break;
            }
        }
    }

    void FixedUpdate()
    {
        if (isMoving)

            if (transform.position.y < throwStartPos.y)
                transform.position = cylinder.transform.position + Vector3.down;
            else if (isLeftToRightMovement ? transform.position.x < throwStartPos.x : transform.position.x > throwStartPos.x)
            {
                Vector3 position = cylinder.transform.position;
                position.y = transform.position.y;
                transform.position = position;
            }
            else
            {
                hasCollided = false;
                isMoving = !isMoving;
            }
    }

    Vector3 differenceVector;

    private Vector3 GetPosition()
    {
        // create ray from the camera and passing through the touch position:
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        // create a logical plane at this object's position
        // and perpendicular to world Z:
        Plane plane = new Plane(Vector3.forward, transform.position);
        float distance = 0; // this will return the distance from the camera
        if (plane.Raycast(ray, out distance))
        { // if plane hit...
            Vector3 pos = ray.GetPoint(distance); // get the point
            // pos has the position in the plane you've touched
            return pos;
        }
        return new Vector3();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasCollided && other.tag == "Player")
        {
            var currentCamPosition = Camera.main.transform.position;
            currentCamPosition.y = transform.position.y + 3;
            Camera.main.transform.position = currentCamPosition;
            hasCollided = true;
            wasHere = true;
            cylinder.transform.position = transform.position + Vector3.up;
            cylinder.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (!hasCollided && other.tag == "Player")
            hasCollided = true;
    }
}