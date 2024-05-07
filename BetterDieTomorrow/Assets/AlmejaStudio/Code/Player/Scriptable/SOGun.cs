using System.Collections;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Pool;

[CreateAssetMenu(fileName = "Gun Config", menuName = "AlmejaStudio/Guns/Guns", order = 0)]
public class SOGun : ScriptableObject
{
    #region Variables

    public WeaponType Type;
    public string Name;
    public GameObject ModelPrefab;
    public GameObject BulletPrefab;
    public Vector3 SpawnPoint;
    public Vector3 SpawnRotation;

    public SOShootConfig ShootConfig;
    public SOTrailConfig TrailConfig;

    private MonoBehaviour _activeMonoBehaviour;
    private GameObject _model;
    private float _lastShootTime;
    private GameObject _bulletSystem;
    private ObjectPool<TrailRenderer> _trailPool;

    #endregion

    public void Spawn(Transform parent, MonoBehaviour activeMonoBehaviour)
    {
        this._activeMonoBehaviour = activeMonoBehaviour;
        _lastShootTime = 0;
        _trailPool = new ObjectPool<TrailRenderer>(CreateTrail);

        _model = Instantiate(ModelPrefab);
        _model.transform.SetParent(parent, false);
        _model.transform.localPosition = SpawnPoint;
        ModelPrefab.transform.localRotation = Quaternion.Euler(SpawnRotation);

        _bulletSystem = _model;
    }

    public void Shoot()
    {
        if (Time.time > ShootConfig.FireRate + _lastShootTime)
        {
            GameObject bullet = Instantiate(BulletPrefab, _bulletSystem.transform.position, _bulletSystem.transform.rotation);
            _bulletSystem.gameObject.SetActive(true);

            #region RandomShootSpreadVariables

            float randomShootSpreedX = UnityEngine.Random.Range(-ShootConfig.Spread.x, ShootConfig.Spread.x);
            float randomShootSpreedY = UnityEngine.Random.Range(-ShootConfig.Spread.y, ShootConfig.Spread.y);
            float randomShootSpreedZ = UnityEngine.Random.Range(-ShootConfig.Spread.z, ShootConfig.Spread.z);

            #endregion
            Vector3 shootDirection = _bulletSystem.transform.forward + new Vector3(randomShootSpreedX, randomShootSpreedY, randomShootSpreedZ);
            shootDirection.Normalize();

            if (Physics.Raycast(_bulletSystem.transform.position, shootDirection, out RaycastHit hit, float.MaxValue, ShootConfig.HitMask))
            {
                _activeMonoBehaviour.StartCoroutine(PlayTrail(_bulletSystem.transform.position, hit.point, hit));
            }
            else
            {
                _activeMonoBehaviour.StartCoroutine(PlayTrail(_bulletSystem.transform.position, (_bulletSystem.transform.position * TrailConfig.MissDistance) , new RaycastHit()));
            }
        }
    }

    private IEnumerator PlayTrail(Vector3 startPoint, Vector3 endPoint, RaycastHit hit)
    {
        TrailRenderer instance = _trailPool.Get();
        instance.gameObject.SetActive(true);
        instance.transform.position = startPoint;
        yield return null;

        instance.emitting = true;

        float distance = Vector3.Distance(startPoint, endPoint);
        float remainingDistance = distance;
        while (remainingDistance > 0)
        {
            instance.transform.position =
                Vector3.Lerp(startPoint, endPoint, Mathf.Clamp01(1 - (remainingDistance / distance)));
            remainingDistance -= TrailConfig.SimulationSpeed * Time.deltaTime;

            yield return null;
        }

        instance.transform.position = endPoint;

        yield return new WaitForSeconds(TrailConfig.Duration);
        yield return null;
        instance.emitting = false;
        instance.gameObject.SetActive(false);
        _trailPool.Release(instance);
    }
    private TrailRenderer CreateTrail()
    {
        GameObject instance = new GameObject("Bullet Trail");
        TrailRenderer trail = instance.AddComponent<TrailRenderer>();
        trail.colorGradient = TrailConfig.Color;
        trail.material = TrailConfig.Material;
        trail.widthCurve = TrailConfig.WidthCure;
        trail.time = TrailConfig.Duration;
        
        trail.emitting = false;
        trail.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        return trail;
    }
    
}
