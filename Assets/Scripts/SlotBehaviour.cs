using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TreeEditor;
using UnityEditor.ShaderKeywordFilter;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class SlotBehaviour : MonoBehaviour
{
    [SerializeField] private float leverTargetDistance;
    [SerializeField] private float leverMovingSpeed;

    [SerializeField] Transform lever;
    [SerializeField] Renderer screen;

    [SerializeField] List<Material> screenColors;
    [SerializeField] private float changeInterval=3f;

    public bool isAvailable;

    private void Start()
    {
        isAvailable = true;
    }


    public IEnumerator PullTheLever()
    {
        Vector3 tempTarget = lever.position - new Vector3(0, leverTargetDistance, 0);
        float elapsedTime = 0f;
        Vector3 startLeverPos = lever.position;

        while (elapsedTime < 1f)
        {
            lever.transform.position = Vector3.Lerp(startLeverPos, tempTarget, elapsedTime);
            elapsedTime += Time.deltaTime * leverMovingSpeed;
            yield return null;
        }

        isAvailable = false;

        yield return new WaitForSeconds(0.5f);
        StartCoroutine(MoveDevultLeverPosition(tempTarget));
    }

    private IEnumerator MoveDevultLeverPosition(Vector3 aStartingPosition)
    {
        Vector3 defaultPosition = lever.position + new Vector3(0, leverTargetDistance, 0);
        float elapsedTime = 0f;
        Vector3 startLeverPos = lever.position;
        while (elapsedTime < 1f)
        {
            lever.transform.position = Vector3.Lerp(startLeverPos, defaultPosition, elapsedTime);
            elapsedTime += Time.deltaTime * leverMovingSpeed;
            yield return null;
        }

        yield return new WaitForSeconds(1);
        isAvailable = true;
    }

    public IEnumerator ColorTheScreen()
    {
        while (true)
        {
            Material randomMaterial = screenColors[Random.Range(0, screenColors.Count)];
            screen.material = randomMaterial;
            yield return new WaitForSeconds(changeInterval);

        }
    }

}


