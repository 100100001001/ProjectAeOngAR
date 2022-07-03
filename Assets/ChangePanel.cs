using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ȭ��ǥ�� ������ �� �÷��̾�(ĳ����)â�� �ٲٴ� ��ũ��Ʈ
public class ChangePanel : MonoBehaviour
{
    public GameObject[] Arrows;       // ȭ��ǥ�� �����״��ϱ� ���� ȭ��ǥ���� �޴� ����
    public GameObject[] playerPanels; // �÷��̾�(ĳ����)â���� �޴� ����
    private int count = 0;            // �÷��̾�(ĳ����)â �迭�� �ε����� �����ϱ� ���� ���� ����

    private void Update()
    {
        // �÷��̾�(ĳ����)â�� Ȱ��ȭ ���ο� ���� ȭ��ǥ�� Ȱ��ȭ/��Ȱ��ȭ ����
        if (playerPanels[0].activeSelf == true) Arrows[0].SetActive(false);
        else if (playerPanels[2].activeSelf == true) Arrows[1].SetActive(false);
        else
        {
            Arrows[0].SetActive(true);
            Arrows[1].SetActive(true);
        }
    }

    // ������ ȭ��ǥ�� ������ ��
    public void Right()
    {
        if (count + 1 >= playerPanels.Length) return;

        playerPanels[count].SetActive(false);
        count++;
        playerPanels[count].SetActive(true);
    }

    // ���� ȭ��ǥ�� ������ ��
    public void Left()
    {
        if (count - 1 < 0) return;

        playerPanels[count].SetActive(false);
        count--;
        playerPanels[count].SetActive(true);
    }
}
