using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking; // 유니티 엔진 네트워킹 라이브러리

public class RequestTest : MonoBehaviour
{
    void Start()
    {
        // 로컬 애플리케이션 서버 URI를 함수에 대한 매개변수로 사용하여 get 요청 호출
        StartCoroutine(GetRequest("http://127.0.0.1:5000"));
    }

    // 애플리케이션 서버에 요청을 보내고,
    // 다음 명령을 실행하기 위한 응답을 받을 때까지 기다림
    IEnumerator GetRequest(string uri)
    {
        // 요청 생성
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        { 
            // 서버에서 응답을 받을 때까지 기다림
            yield return webRequest.SendWebRequest();

            // 네트워크 오류 확인하고 로그 기록
            if (webRequest.isNetworkError)
            {
                Debug.Log("Error : " + webRequest.error);
            }
            else
            {
                Debug.Log(webRequest.downloadHandler.text);
            }
        }
    }
}
