using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class DialogueTrackMixer : PlayableBehaviour
{
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        TextMeshProUGUI text = playerData as TextMeshProUGUI;
        string currentText = "";
        float currentAlpha = 0f;

        if(!text){return;}
        int inputCount=playable.GetInputCount();
        for(int i=0;i<inputCount;i++){
            float inputWeight= playable.GetInputWeight(i);
            if(inputWeight>0f){
                ScriptPlayable<DialogueBehaviour> inputPlayable = (ScriptPlayable<DialogueBehaviour>)playable.GetInput(i);
                DialogueBehaviour input = inputPlayable.GetBehaviour();
                currentText=input.dialogueText;
                currentAlpha = inputWeight;
            }

        }


        text.text = currentText;
        text.color = new Color(1,1,1,currentAlpha);
    }
}
