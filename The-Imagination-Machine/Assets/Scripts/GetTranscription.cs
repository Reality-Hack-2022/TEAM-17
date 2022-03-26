using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTranscription : MonoBehaviour
{
    public Oculus.Voice.AppVoiceExperience VoiceExperience;
    public GPT3Interface GPT3Interface;
    public void ShowLastTranscription(string lastTranscription) {
        gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = lastTranscription;
    }
    public void ShowAndSendLastTranscription(string lastTranscription) {
        ShowLastTranscription(lastTranscription);
        GPT3Interface.SendInput(lastTranscription + "\n");
    }
}
