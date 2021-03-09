using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResources : MonoBehaviour
{
    [SerializeField] int _maxLightPoints;
    private int _currentLightPoints;
    private int _skillPoints = 2;

    private LightPower _pointLight;
    private LightBar _lightBar;

    private Camera _mainCamera;

    private bool _haungsMode = false;
    private PlayerControls input;

    private PlayerShield _shield;

    private int _minLightPowerBrightness = 35;

   // Start is called before the first frame update
   void Start()
    {
        _mainCamera = Camera.main;

        _lightBar = FindObjectOfType<LightBar>();
        _pointLight = FindObjectOfType<LightPower>();

        _currentLightPoints = _maxLightPoints;
        _currentLightPoints = _maxLightPoints;
        if (_lightBar != null) {
            _lightBar.SetMaxLightPoints(_maxLightPoints);
            _lightBar.SetLightPoints(_maxLightPoints);
        }
        _pointLight.SetMaxLightPoints(_currentLightPoints);
        _pointLight.SetLightPoints(_maxLightPoints);

        _mainCamera.GetComponent<GlowComposite>().Intensity = (float)_currentLightPoints / (float)_maxLightPoints;

        _shield = GetComponent<PlayerShield>();
    }

    private void UpdateLightPoints(int damage) {
        _currentLightPoints -= damage;
        _lightBar.SetLightPoints(_currentLightPoints);
        int displayLightPoints = Math.Max(_minLightPowerBrightness, _currentLightPoints);
        _pointLight.GetComponent<LightPower>().SetLightPoints(displayLightPoints);
        _mainCamera.GetComponent<GlowComposite>().Intensity = (float)displayLightPoints / (float)_maxLightPoints;
   }

    public void TakeDamage(int damage)
    {

        if(_shield != null && _shield._shieldPressed)
        {
            _shield.ShieldImpact();
            UpdateLightPoints(Mathf.RoundToInt(damage * 0.25f));
            return;
        }

        if (!_haungsMode) {
            UpdateLightPoints(damage);
        }
    }

    public void TakeSelfDamage(int damage)
    {
        if (!_haungsMode)
        {
            UpdateLightPoints(damage);
        }
    }

    public bool ResourcesAvailable(int spendRequest)
    {
        if (_currentLightPoints - spendRequest > 0)
            return true;
        else
            return false;
    }

    public void Regenerate()
    {
        if (_currentLightPoints != _maxLightPoints)
        {
            _currentLightPoints = Mathf.CeilToInt(Mathf.Lerp(_currentLightPoints, _maxLightPoints, 0.05f));
            _lightBar.SetLightPoints(_currentLightPoints);
            _pointLight.GetComponent<LightPower>().SetLightPoints(_currentLightPoints);
            _mainCamera.GetComponent<GlowComposite>().Intensity = (float)_currentLightPoints / (float)_maxLightPoints;

        }
    }

    public int getMaxLightPoints() { return _maxLightPoints; }
    public int getCurrentLightPoints() { return _currentLightPoints; }
    public int getSkillPoints() {return _skillPoints; }
    public void setSkillPoints(int points) {_skillPoints = points;}

    private void Awake() {
        input = new PlayerControls();
        input.CharacterControls.HaungsMode.performed += ctx => _haungsMode = ctx.ReadValueAsButton();
        input.CharacterControls.HaungsMode.canceled += ctx => { };
    }

    private void OnEnable() {
        input.CharacterControls.Enable();
    }

    private void OnDisable() {
        input.CharacterControls.Disable();
    }
}
