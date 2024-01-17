using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HighlightIndividual : MonoBehaviour
{
    public void OnClickText()
    {
        TextMeshProUGUI scrollContent = GameObject.Find("ScrollSuspectsNames/Viewport/Content").GetComponent<TextMeshProUGUI>();
        Camera arCamera = GameObject.Find("AR Camera").GetComponent<Camera>();
        var lineIndex = TMP_TextUtilities.FindIntersectingLine(scrollContent, Input.mousePosition, arCamera);
        if (lineIndex != -1) {
            string fullName = scrollContent.text.Split('\n')[lineIndex];
            scrollContent.text = scrollContent.text.Replace("<color=red>", "");
            scrollContent.text = scrollContent.text.Replace("</color>", "");
            scrollContent.text = scrollContent.text.Replace(fullName, "<color=red>" + fullName + "</color>");
            GameObject map = GameObject.Find("MapMenu");
            foreach (Transform child in map.transform) {
                if(child.gameObject.name != "Image"){
                    if(child.gameObject.name == fullName){
                        child.gameObject.GetComponent<RawImage>().color = Color.red;
                    } else {
                        child.gameObject.GetComponent<RawImage>().color = Color.cyan;                    }
                }
            }
        }
    }
}
