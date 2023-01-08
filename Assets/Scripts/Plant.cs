using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Serializable]
    struct StageData
    {
        public float stageDuration;
        public Sprite sprite;
    }

    [SerializeField] private List<StageData> stages;
    public int CurrentStageIndex { get; private set; }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetStage(0);
    }

    private void SetStage(int stageIndex)
    {
        var stage = stages[stageIndex];
        spriteRenderer.sprite = stage.sprite;
        
        CurrentStageIndex = stageIndex;
        StartCoroutine(WaitStageDurationThenAdvance());
    }

    private IEnumerator WaitStageDurationThenAdvance()
    {
        yield return new WaitForSeconds(stages[CurrentStageIndex].stageDuration);
        CurrentStageIndex++;
        
        if (CurrentStageIndex < stages.Count)
            SetStage(CurrentStageIndex);
    }
}