using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TransitionManager : MonoBehaviour
{
    [SerializeField] private Transform transitionPanel;
    [SerializeField] private float transitionSpeed = 1f;
    [SerializeField] private Color transitionColor = Color.black;

    [SerializeField] private Vector3 leftPosition = new Vector3(-1.5f, 0, 0);
    [SerializeField] private Vector3 centerPosition = new Vector3(0, 0, 0);
    [SerializeField] private Vector3 rightPosition = new Vector3(1.5f, 0, 0);

    private bool isTransitioning = false;
    private bool newSceneLoaded = false;

    private void Awake()
    {
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

    [ContextMenu("Teste")]
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

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                asyncLoad.allowSceneActivation = true;
                newSceneLoaded = true;
            }
            yield return null;
        }

        yield return MovePanelTo(rightPosition);

        transitionPanel.localPosition = leftPosition;
        isTransitioning = false;
        newSceneLoaded = false;
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
}
