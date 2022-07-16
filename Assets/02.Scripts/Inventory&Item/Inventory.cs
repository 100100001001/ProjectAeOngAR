using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // 싱글톤패턴 적용
    // 1. 누구나 손쉽게 접근
    // 2. 단, 하나만 존재
    public static Inventory instance;


    // slotCount의 변화를 알려줄 델리게이트(대리자) 정의
    public delegate void OnSlotCountChange(int value);

    // slotCount의 변화를 알려줄 델리게이트(대리자) 인스턴스화
    public OnSlotCountChange onSlotCountChange;


    // 아이템 추가시 슬롯UI에도 알려줄 델리게이트(대리자) 정의
    public delegate void OnChangeItem();
    // 아이템 추가시 슬롯UI에도 알려줄 델리게이트(대리자) 인스턴스화
    public OnChangeItem onChangeItem;


    // Slot의 개수를 지정할 변수
    private int slotCount;

    // slotCount 캡슐화 프로퍼티
    public int SlotCount
    {
        // 외부에서 값을 읽어올 경우 slotCount의 값을 넘겨줌
        get => slotCount;
        // 외부에서 값을 적용할 경우
        set
        {
            // slotCount에 입력값을 적용
            slotCount = value;

            // slotCount의 변화를 알려줄 델리게이트(대리자)를 호출
            onSlotCountChange.Invoke(slotCount);
            //onSlotCountChange(slotCount); // 위 아래 둘 중 하나로 실행

        }

    }


    // 획득한 아이템을 보관할(담을) List
    public List<Item> items = new List<Item>();


    private void Awake()
    {
        // 만약에 instance가 비어있지 않다면,
        if (instance != null)
        {
            // 현재 오브젝트를 파괴
            Destroy(gameObject);
            // 아래로 더 이상 내려가지 말고 돌아가
            return;
        }

        // instance에 현재 내 자신을 넣어줘
        instance = this;
    }

    void Start()
    {
        // SlotCount를 0으로 초기화
        SlotCount = 0;
    }


    // items 리스트에 아이템을 추가할 수 있는 메서드
    public bool AddItem(Item item)
    {
        // 만약에 items(리스트)의 개수가 실제 사용가능한 slotCount(현재 활성된 슬롯)보다 작다면
        if (items.Count < SlotCount)
        {
            // items 리스트에 아이템 추가
            items.Add(item);
            // 만약에 onChangeItem이 비어있지 않다면,
            if (onChangeItem != null) onChangeItem.Invoke();

            return true; // 아이템 추가 성공 반환
        }

        return false;
    }

}
