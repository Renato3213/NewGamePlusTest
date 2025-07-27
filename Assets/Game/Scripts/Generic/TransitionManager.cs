using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;

public class TransitionManager : MonoBehaviour
{
    [SerializeField] private Transform transitionPanel;
    [SerializeField] private float transitionSpeed = 1f;

    [SerializeField] private Vector3 leftPosition = new Vector3(-1.5f, 0, 0);
    [SerializeField] private Vector3 centerPosition = new Vector3(0, 0, 0);
    [SerializeField] private Vector3 rightPosition = new Vector3(1.5f, 0, 0);

    private bool isTransitioning = false;
    private bool newSceneLoaded = false;

    [SerializeField] private GameObject[] _mapPrefabs;
    [SerializeField] private GameObject _currentMap;
    [SerializeField] private int _currentMapIndex;

    [SerializeField] private Transform _cameraConfiner;

    private void Awake()
    {
        GameManager.Instance.TransitionManager = this;
        transitionPanel.gameObject.SetActive(true);
        transitionPanel.localPosition = centerPosition;
        StartCoroutine(MovePanelTo(rightPosition));
    }

    private void OnEnable()
    {
        Door.OnDoorOpened += StartSceneTransition;
    }

    private void OnDisable()
    {
        Door.OnDoorOpened -= StartSceneTransition;
    }

    public void StartSceneTransition()
    {
        if (isTransitioning) return;

        transitionPanel.localPosition = leftPosition;
        isTransitioning = true;
        StartCoroutine(TransitionRoutine());
    }

    private IEnumerator TransitionRoutine()
    {
        yield return MovePanelTo(centerPosition);

        yield return new WaitForSeconds(0.5f);

        GameObject oldMap = _currentMap;
        Destroy(oldMap);
        _currentMap = Instantiate(_mapPrefabs[_currentMapIndex + 1], Vector2.zero, Quaternion.identity);
        _currentMapIndex++;
        _cameraConfiner.position = _currentMap.transform.position;
        GameManager.Instance.Player.transform.position = _currentMap.GetComponent<Map>().SpawnPoint.position;


        yield return new WaitForSeconds(0.5f);

        yield return MovePanelTo(rightPosition);

        transitionPanel.localPosition = leftPosition;
        isTransitioning = false;
    }

    private IEnumerator LoadMapRoutine(MapSaveData data)
    {
        yield return MovePanelTo(centerPosition);

        yield return new WaitForSeconds(0.5f);

        GameObject oldMap = _currentMap;
        Destroy(oldMap);
        _currentMap = Instantiate(_mapPrefabs[data.MapIndex], Vector2.zero, Quaternion.identity);
        _cameraConfiner.position = _currentMap.transform.position;
        GameManager.Instance.Player.transform.position = _currentMap.GetComponent<Map>().SpawnPoint.position;
        _currentMapIndex = data.MapIndex;

        yield return new WaitForSeconds(0.5f);

        yield return MovePanelTo(rightPosition);

        transitionPanel.localPosition = leftPosition;
        isTransitioning = false;
    }

    private IEnumerator MovePanelTo(Vector3 targetPosition)
    {
        Vector3 startPosition = transitionPanel.localPosition;
        float distance = Vector3.Distance(startPosition, targetPosition);
        float duration = distance / transitionSpeed;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transitionPanel.localPosition = Vector3.Lerp(
                startPosition,
                targetPosition,
                elapsedTime / duration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transitionPanel.localPosition = targetPosition;
    }

    public void Save(ref MapSaveData data)
    {
        data.MapIndex = _currentMapIndex;
    }

    public void Load(MapSaveData data)
    {
        if (isTransitioning) return;

        transitionPanel.localPosition = leftPosition;
        isTransitioning = true;
        StartCoroutine(LoadMapRoutine(data));
    }


}

[System.Serializable]
public struct MapSaveData
{
    public int MapIndex;
}
