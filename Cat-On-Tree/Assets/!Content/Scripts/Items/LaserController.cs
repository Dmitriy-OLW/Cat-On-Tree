using UnityEngine;

public class LaserController : MonoBehaviour
{
    [SerializeField] private VRHandInteractor VRHandScript_R;
    [SerializeField] private VRHandInteractor VRHandScript_L;
    [SerializeField] private LineRenderer laserLineRenderer;
    [SerializeField] private Transform laserOrigin; // Точка, из которой выстреливает лазер

    private bool isLaserActive = false;
    private const float maxLaserDistance = 100f; // Максимальная дальность лазера
    private const string glassTag = "Glass"; // Тег для стеклянных объектов
    private bool _isActive = false;

    void Update()
    {
        // Проверяем активацию лазера от любого контроллера
        if (VRHandScript_L != null && VRHandScript_R != null && 
            (VRHandScript_L.activateLaser || VRHandScript_R.activateLaser))
        {
            if (!_isActive)
            {
                ToggleLaser();
            }
            
        }

        if (!VRHandScript_L.activateLaser && !VRHandScript_R.activateLaser)
        {
            _isActive = false;
        }

        if (isLaserActive)
        {
            UpdateLaser();
        }
        else if (laserLineRenderer.enabled)
        {
            laserLineRenderer.enabled = false;
        }
    }

    private void ToggleLaser()
    {
        // Переключаем состояние лазера
        isLaserActive = !isLaserActive;
        laserLineRenderer.enabled = isLaserActive;
        _isActive = true;
    }

    private void UpdateLaser()
    {
        if (laserOrigin == null)
        {
            return;
        }

        Vector3 laserStart = laserOrigin.position;
        Vector3 laserDirection = laserOrigin.forward;
        RaycastHit hit;

        // Начинаем с начальной позиции лазера
        Vector3 endPosition = laserStart + laserDirection * maxLaserDistance;

        // Создаем луч для проверки столкновений
        Ray ray = new Ray(laserStart, laserDirection);
        bool hasHit = Physics.Raycast(ray, out hit, maxLaserDistance);

        // Проверяем все пересечения для обработки стеклянных объектов
        if (hasHit)
        {
            bool foundNonGlass = false;
            RaycastHit[] hits = Physics.RaycastAll(ray, maxLaserDistance);
            
            foreach (RaycastHit currentHit in hits)
            {
                if (!currentHit.collider.CompareTag(glassTag))
                {
                    // Нашли непрозрачный объект
                    endPosition = currentHit.point;
                    foundNonGlass = true;
                    break;
                }
            }

            // Если все объекты стеклянные, луч проходит на максимальную дистанцию
            if (!foundNonGlass)
            {
                //endPosition = laserStart + laserDirection * maxLaserDistance;
            }
        }

        // Обновляем LineRenderer
        laserLineRenderer.SetPosition(0, laserStart);
        laserLineRenderer.SetPosition(1, endPosition);
    }
}