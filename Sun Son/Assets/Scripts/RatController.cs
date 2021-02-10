using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatController : MonoBehaviour
{
    [SerializeField] int _maxLightPoints;
    [SerializeField] int _damageCost = 2;
    [SerializeField] LightPower _pointLight;
    [SerializeField] LightBar _lightBar;

    private bool goingLeft = false;
    private float walkTimer = 2.0f;
    private float damageTimer = 1.0f;
    private int _currentLightPoints;

    // Start is called before the first frame update
    void Start()
    {
        _currentLightPoints = _maxLightPoints;
        _lightBar.SetLightPoints(_currentLightPoints);
        _pointLight.GetComponent<LightPower>().SetLightPoints(_currentLightPoints);
    }

    // Update is called once per frame
    void Update()
    {
        if (walkTimer <= 0)
        {
            walkTimer = 2.0f;
            goingLeft = !goingLeft;
        }
        else
        {
            walkTimer -= Time.deltaTime;

            if (goingLeft)
            {
                transform.position -= new Vector3(0.02f, 0, 0);
            }
            else
            {
                transform.position += new Vector3(0.02f, 0, 0);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name == "SunCharacter")
        {
            if (damageTimer <= 0)
            {
                _currentLightPoints = _lightBar.GetLightPoints();
                _currentLightPoints -= _damageCost;
                _lightBar.SetLightPoints(_currentLightPoints);
                _pointLight.GetComponent<LightPower>().SetLightPoints(_currentLightPoints);
                damageTimer = 1.0f;
            }
            else
            {
                damageTimer -= Time.deltaTime;
            }
        }
    }
}
