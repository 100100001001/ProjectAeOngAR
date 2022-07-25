using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking; // ����Ƽ ���� ��Ʈ��ŷ ���̺귯��

public class RequestTest : MonoBehaviour
{
    void Start()
    {
        // ���� ���ø����̼� ���� URI�� �Լ��� ���� �Ű������� ����Ͽ� get ��û ȣ��
        StartCoroutine(GetRequest("http://127.0.0.1:5000"));
    }

    // ���ø����̼� ������ ��û�� ������,
    // ���� ����� �����ϱ� ���� ������ ���� ������ ��ٸ�
    IEnumerator GetRequest(string uri)
    {
        // ��û ����
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        { 
            // �������� ������ ���� ������ ��ٸ�
            yield return webRequest.SendWebRequest();

            // ��Ʈ��ũ ���� Ȯ���ϰ� �α� ���
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
