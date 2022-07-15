using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    // Inventory 컴포넌트가 담길 변수
    private Inventory inventory;

    // 사용자 인벤토리의 슬롯을 담을 배열 선언
    public Slot[] slots;
    // slot들을 품고 있는 부모 오브젝트(=Content) 지정 변수 선언
    public Transform slotHolder;


    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemDesText;



    void Start()
    {
        // slotHolder의 자식 오브젝트들에서 Slot 컴포넌트를 한번에 배열로 가져오기
        slots = GetComponentsInChildren<Slot>();

        // inventory 변수 초기화
        inventory = Inventory.instance;

        // inventory > onSlotCountChange에 SlotChange 메서드 등록(구독)
        inventory.onSlotCountChange += SlotChange;

    }

    // inventory의 SlotCount의 값만큼 Slot을 활성화시키는 메서드
    private void SlotChange(int value)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].GetComponent<Button>().interactable = i < inventory.SlotCount ? true : false;
        }
    }

    // inventory의 SlotCount의 값을 증가시키는 메서드
    public void AddSlot()
    {
        inventory.SlotCount++;
    }

    void Update()
    {
        if (inventory.SlotCount == 0)
        {
            itemNameText.text = "아이템이 없어요!";
            itemDesText.text = "또바기와 미니 게임을 해보세요.\n또바기에게 선물을 줄 수 있을지도?";
        }


    }
}
